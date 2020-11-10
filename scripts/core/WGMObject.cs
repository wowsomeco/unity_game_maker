using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.Core;
using Wowsome.Generic;

namespace Wowsome {
  namespace GameMaker {
    /// <summary>
    /// The Game Object that can have one or more WGMComponent (s).
    /// - On Init, it will find all the WGMComponent in its root.
    /// - Right now it cant have nested WGMObject in it, might eventually be able to do so but dont do it for now.
    /// - Observable is the event hub where every WGMComponent can both send and receive events.
    /// </summary>
    public class WGMObject : MonoBehaviour {
      /// <summary>
      /// The Object Model.
      /// The idea is to be able to save the model as json so that we can eventually generate the object(s) from the json files.
      /// 
      /// - right now, if the id is undefined, it will auto add from the gameobject name on init.
      /// - not really usable for now as we dont have node editor yet, it's still very much WIP too.
      /// </summary>
      [Serializable]
      public class Model {
        public string id;
        [HideInInspector]
        public int x;
        [HideInInspector]
        public int y;
      }

      /// <summary>
      /// when it's true, it will show all the received and sent events from this component
      /// </summary>
      public bool DebugMode;

      [SerializeField] Model _model;
      Dictionary<string, WGMComponent> _components = new Dictionary<string, WGMComponent>();

      public string Id {
        get {
          if (_model.id.IsEmpty()) _model.id = name;
          return _model.id;
        }
      }

      public CavEngine Engine { get; private set; }

      public WObservable<SenderEv> Observable { get; private set; }

      public void InitObject(CavEngine engine) {
        Engine = engine;
        Observable = new WObservable<SenderEv>(null);

        var components = GetComponentsInChildren<WGMComponent>(true);
        // init them all
        foreach (WGMComponent c in components) {
          c.InitComponent(this);
          _components[c.Id] = c;
        }
        // start
        foreach (var kv in _components) {
          kv.Value.StartComponent();
        }
      }

      public void UpdateObject(float dt) {
        foreach (var c in _components) {
          c.Value.UpdateComponent(dt);
        }
      }
    }
  }
}
