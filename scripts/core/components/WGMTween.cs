using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.Audio;
using Wowsome.Tween;

namespace Wowsome.GameMaker {
  /// <summary>
  /// Tween Component that plays tween on any _triggers accordingly.
  /// Ideally, one WGMObject can have one or many WGMTween.  
  /// On Init, WGMTween will trace down the children and get the reference of each ITween in the children gameobjects.
  /// 
  /// BEST PRACTICE : dont nest WGMTween inside another since it might be completely unnecessary and might behave unexpectedly,
  /// since the parent will grab all the ITween(s) inside the children, where the children also have the references for the same ITween(s), 
  /// which might create a black hole.
  /// 
  /// REQUIRED: It needs to have AudioSystem in CavEngine with SfxManager attached to it.  
  /// 
  /// Look into TweenEv below for more details.    
  /// </summary>
  [DisallowMultipleComponent]
  public class WGMTween : WGMComponent {
    [Serializable]
    public class TweenEv : ReceiverEv {
      /// <summary>
      /// The event that will be sent on start play tween
      /// </summary>      
      public List<string> startEv = new List<string>();
      /// <summary>
      /// The sound fx that will be played on start play tween
      /// </summary>      
      public List<SfxData> startSound = new List<SfxData>();
      /// <summary>
      /// The event that will be sent on done play tween
      /// </summary>      
      public List<string> doneEv = new List<string>();
      /// <summary>
      /// The sound fx that will be played on done play tween
      /// </summary>      
      public List<SfxData> doneSound = new List<SfxData>();
      /// <summary>
      /// True = stop the tween, False = play the tween
      /// </summary>
      public bool stop;
    }

    [SerializeField] List<TweenEv> _triggers = new List<TweenEv>();
    SfxManager _sfx;
    CTweenChainer _tweener;

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      // iterate over the children...
      // dont include the children that have another WGMTween too.
      List<GameObject> children = new List<GameObject>();
      gameObject.IterateSelfAndChildren(go => {
        bool stopIteration = !go.Same(gameObject) && go.HasComponent<WGMTween>();
        if (stopIteration) return false;

        children.Add(go);
        return true;
      });
      // add the children object that has tweens to our container
      _tweener = new CTweenChainer();
      _tweener.Add(children.ToArray());
      // cache the sfx manager
      _sfx = obj.Engine.GetSystem<AudioSystem>().GetManager<SfxManager>();

      obj.Observable.Subscribe(ev => {
        TweenEv te;
        if (ev.Matches(this, _triggers, out te)) {
          TryPlaySound(te.startSound);
          TryBroadcastEvent(te.startEv);

          if (te.stop) {
            // when data is empty, stop all
            // otherwise iterate over the list data and stop them
            if (te.data.IsEmpty()) {
              _tweener.Stop();
            } else {
              te.data.ForEach(d => _tweener.StopTween(d));
            }
          } else {
            // stop first if still playing;
            if (_tweener.IsPlaying) {
              _tweener.Stop();
            }

            _tweener.PlayExistingTween(
              te.data,
              () => {
                TryPlaySound(te.doneSound);
                TryBroadcastEvent(te.doneEv);
              }
            );
          }
        }
      });
    }

    public override void UpdateComponent(float dt) {
      _tweener.Update(dt);
    }

    void TryPlaySound(List<SfxData> sfxs) {
      if (!sfxs.IsEmpty()) _sfx.PlaySound(sfxs.ToArray());
    }
  }
}

