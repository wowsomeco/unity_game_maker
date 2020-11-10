using System.Collections.Generic;
using UnityEngine;

namespace Wowsome.GameMaker {
  /// <summary>
  /// Activator Component.  
  /// it reacts according to the first index of the list data.
  /// the data can be either show, hide or toggle.
  /// once done, it will automagically broadcast the data again.
  /// </summary>
  public class WGMActivator : WGMComponent {
    readonly string Show = "show";
    readonly string Hide = "hide";
    readonly string Toggle = "toggle";

    [SerializeField] List<ReceiverEv> _triggers = new List<ReceiverEv>();

    Dictionary<string, Delegate<bool, GameObject>> _handlers = new Dictionary<string, Delegate<bool, GameObject>>();

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      _handlers[Show] = g => true;
      _handlers[Hide] = g => false;
      _handlers[Toggle] = g => !g.activeSelf;

      obj.Observable.Subscribe(ev => {
        ReceiverEv ae = null;
        if (ev.Matches(this, _triggers, out ae)) {
          bool visible;
          if (ProcessData(ae.data, out visible)) {
            gameObject.SetActive(visible);
            // broadcast the visibility state.
            // indicating that the event given has been triggered
            TryBroadcastEvent(visible ? Show : Hide);
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

