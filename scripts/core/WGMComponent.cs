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

    public string Id {
      get {
        if (Model.id.IsEmpty()) Model.id = name;

        return Model.id;
      }
    }

    public string ComponentType {
      get { return GetType().ToString().LastSplit('.').Replace("WGM", ""); }
    }

    public string Info {
      get {
        return string.Format("{0}.{1}.{2}", Object.name, ComponentType, Id);
      }
    }

    public WGMObject Object { get; private set; }

    public virtual void InitComponent(WGMObject obj) {
      Object = obj;
    }

    public virtual void StartComponent() { }

    public virtual void UpdateComponent(float dt) { }

    public void TryBroadcastEvent(List<string> ev) {
      if (!ev.IsEmpty()) {
        Object.Observable.Next(new SenderEv(Id, ev));

        Print.Log(string.Format("{0}|SENDS|{1}", Info, ev.Flatten()), "orange");
      }
    }

    public void TryBroadcastEvent(string ev) {
      TryBroadcastEvent(new List<string> { ev });
    }
  }
}

