using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowsome.GameMaker {
  public class WGMActivator : WGMComponent {
    /// <summary>
    /// Activator Event.
    /// when it gets triggered, it will process the list data.
    /// for now it will get the first index of the string data.
    /// the data can be either show, hide or toggle.
    /// </summary>
    [Serializable]
    public class ActivatorEv : ReceiverEv {
      public List<string> activatedEv;
    }

    [SerializeField] List<ActivatorEv> _triggers = new List<ActivatorEv>();

    Dictionary<string, Delegate<bool, GameObject>> _handlers = new Dictionary<string, Delegate<bool, GameObject>>();

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      _handlers["show"] = g => true;
      _handlers["hide"] = g => false;
      _handlers["toggle"] = g => !g.activeSelf;

      obj.Observable.Subscribe(ev => {
        ActivatorEv ae = null;
        if (ev.Matches(this, _triggers, out ae)) {
          bool visible;
          if (ProcessData(ae.data, out visible)) {
            gameObject.SetActive(visible);
            TryBroadcastEvent(ae.activatedEv);
          }
        }
      });
    }

    bool ProcessData(List<string> data, out bool flag) {
      flag = false;
      // return if no data given
      if (data.IsEmpty()) return false;
      // get the first index of the string data
      string v = data[0];
      // dont go further if no handlers defined
      if (!_handlers.ContainsKey(v)) return false;
      // finally.
      flag = _handlers[v](gameObject);
      return true;
    }
  }
}

