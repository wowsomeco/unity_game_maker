using System.Collections.Generic;
using UnityEngine;

namespace Wowsome {
  namespace GameMaker {
    public class WGMSceneController : MonoBehaviour, IRootController {
      public RectTransform Root;
      public List<WGMObject> Objects = new List<WGMObject>();

      public RectTransform MainRoot { get { return Root; } }

      public void AddObject() {

      }
    }
  }
}

