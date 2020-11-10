using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.UI;

namespace Wowsome.GameMaker {
  /// <summary>
  /// The gesture component.
	/// - Right now it can react to tap and drag only.
	/// - Only works for Unity UI.
	/// - Will broadcast the event according to whichever gesture events that get triggered.
	/// 	
  /// TODO: more info, might also need to make the props more customizable e.g. flag for sending dragging ev, etc.
  /// </summary>
  [RequireComponent(typeof(RectTransform))]
  public class WGMGesturer : WGMComponent {
    public class Util {
      public RectTransform Rt { get; private set; }
      public RectTransform Parent { get; private set; }

      public Util(GameObject g) {
        Rt = g.GetComponent<RectTransform>();
        var parent = Rt.parent as RectTransform;
        Parent = parent != null ? parent : Rt;
      }

      public Vector2 LocalFromScreenPos(Vector2 screenPos) {
        Vector2 p;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Parent, screenPos, null, out p);
        return p;
      }

      public void SetFromScreenPos(Vector2 screenPos) {
        var localPos = LocalFromScreenPos(screenPos);
        Rt.SetPos(localPos);
      }
    }

    public enum GType { Tap, Drag }

    [SerializeField] List<GType> _gestureTypes = new List<GType>();
    CGestureHandler _gesture;
    Dictionary<GType, Action> _handlers = new Dictionary<GType, Action>();
    Util _util;

    readonly string _evTap = "tap";
    readonly string _evStartDrag = "startdrag";
    readonly string _evDragging = "dragging";
    readonly string _evEndDrag = "enddrag";

    public override void InitComponent(WGMObject obj) {
      base.InitComponent(obj);

      _util = new Util(gameObject);
      // tap handler
      _handlers[GType.Tap] = () => {
        _gesture.SetTappable();
        _gesture.OnTapListeners += pos => TryBroadcastEvent(_evTap);
      };
      // drag handler
      // TODO: might want to revamp the events e.g. send the cur pos too ?
      _handlers[GType.Drag] = () => {
        _gesture.SetDraggable();
        _gesture.OnStartSwipeListeners += ed => TryBroadcastEvent(_evStartDrag);
        _gesture.OnSwipingListeners += ed => {
          _util.SetFromScreenPos(ed.Pos);
          TryBroadcastEvent(_evDragging);
        };
        _gesture.OnEndSwipeListeners += ed => TryBroadcastEvent(_evEndDrag);
      };
      // init the gesture handler
      _gesture = new CGestureHandler(gameObject);
      // iterate over the given types and attach the gesture handler accordingly.
      _gestureTypes.ForEach(g => {
        if (_handlers.ContainsKey(g)) {
          _handlers[g]();
          // make sure gesture type only be added once for each of them.
          _handlers.Remove(g);
        }
      });
    }
  }
}

