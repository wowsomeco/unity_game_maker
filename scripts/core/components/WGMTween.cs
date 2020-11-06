using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.Audio;
using Wowsome.Tween;

namespace Wowsome.GameMaker {
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
    }

    [SerializeField] List<TweenEv> _triggers = new List<TweenEv>();
    SfxManager _sfx;
    CTweenChainer _tweener;

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      _tweener = new CTweenChainer();
      _tweener.Add(gameObject, true);
      // cache the sfx manager
      _sfx = obj.Engine.GetSystem<AudioSystem>().GetManager<SfxManager>();

      obj.Observable.Subscribe(ev => {
        TweenEv te;
        if (ev.Matches<TweenEv>(_triggers, out te)) {
          TryPlaySound(te.startSound);
          TryBroadcastEvent(te.startEv);

          _tweener.PlayExistingTween(
            te.data,
            () => {
              TryPlaySound(te.doneSound);
              TryBroadcastEvent(te.doneEv);
            }
          );
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

