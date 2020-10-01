using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowsome {
  namespace GameMaker {
    public class WGMObject : MonoBehaviour {
      [Serializable]
      public class Model {
        public int x;
        public int y;
      }

      public List<WGMComponent> Components = new List<WGMComponent>();

      public RectTransform RootTransform { get; private set; }

      Dictionary<string, WGMComponent> _components = new Dictionary<string, WGMComponent>();

      public void AddObjectComponent(WGMComponent c) {

      }
    }
  }
}
