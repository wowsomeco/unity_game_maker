using UnityEngine;
using Wowsome.Core;

namespace Wowsome.GameMaker {
  public class WGMDemo : MonoBehaviour {
    WGMObject _object;

    void Start() {
      _object = GetComponentInChildren<WGMObject>();
      _object.InitObject(CavEngine.Instance);
    }

    void Update() {
      _object.UpdateObject(Time.deltaTime);
    }
  }
}

