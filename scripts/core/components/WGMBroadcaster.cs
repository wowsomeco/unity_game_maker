using System.Collections.Generic;
using UnityEngine;

namespace Wowsome.GameMaker {
  /// <summary>
  /// Component that can emit events.
  /// </summary>
  public class WGMBroadcaster : WGMComponent {
    /// <summary>
    /// The events that will be emit on init component.
    /// </summary>
    [SerializeField] List<string> _initEvs = new List<string>();
    /// <summary>
    /// The list of triggers. 
    /// </summary>
    [SerializeField] List<ReceiverEv> _triggers = new List<ReceiverEv>();

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      Object.Observable.Subscribe(ev => {
        ReceiverEv r;
        if (ev.Matches(this, _triggers, out r)) {
          TryBroadcastEvent(r.data);
        }
      });
    }

    public override void StartComponent() {
      TryBroadcastEvent(_initEvs);
    }
  }
}

