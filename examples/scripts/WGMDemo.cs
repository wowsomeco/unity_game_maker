using System.Collections.Generic;
using UnityEngine;
using Wowsome.Core;

namespace Wowsome.GameMaker {
  public class WGMDemo : MonoBehaviour {
    List<WGMObject> _objects;

    void Start() {
      _objects = new List<WGMObject>(GetComponentsInChildren<WGMObject>(true));
      _objects.ForEach(obj => obj.InitObject(CavEngine.Instance));
    }

    void Update() {
      foreach (WGMObject o in _objects) {
        o.UpdateObject(Time.deltaTime);
      }
    }
  }
}

