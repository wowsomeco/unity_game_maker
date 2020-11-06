using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.Core;
using Wowsome.Generic;

namespace Wowsome {
  namespace GameMaker {
    public class WGMObject : MonoBehaviour {
      [Serializable]
      public class Model {
        public int x;
        public int y;
      }

      Dictionary<string, WGMComponent> _components = new Dictionary<string, WGMComponent>();

      public CavEngine Engine { get; private set; }

      public WObservable<SenderEv> Observable { get; private set; }

      public void InitObject(CavEngine engine) {
        Engine = engine;
        Observable = new WObservable<SenderEv>(null);

        var components = GetComponentsInChildren<WGMComponent>(true);
        foreach (WGMComponent c in components) {
          c.InitComponent(this);
          _components[c.Model.id] = c;
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
