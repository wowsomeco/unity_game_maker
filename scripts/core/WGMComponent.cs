using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowsome.GameMaker {
  public abstract class WGMComponent : MonoBehaviour {
    [Serializable]
    public class ComponentModel {
      public string id;
    }

    public ComponentModel Model;

    public string Id { get { return Model.id; } }

    public WGMObject Object { get; private set; }

    public virtual void InitComponent(WGMObject obj) {
      Object = obj;
    }

    public virtual void UpdateComponent(float dt) { }

    public void TryBroadcastEvent(List<string> ev) {
      if (!ev.IsEmpty()) Object.Observable.Next(new SenderEv(Id, ev));
    }
  }
}

