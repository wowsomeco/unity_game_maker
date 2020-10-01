using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Wowsome.Node;

namespace Wowsome {
  namespace GameMaker {
    using EU = EditorUtils;

    public class WGMObjectEditor : WowNode {
      Dropdown<string> _listComponent = new Dropdown<string>();
      List<string> _components = new List<string> {
        "draggable",
        "tappable"
      };

      public WGMObjectEditor(INodeEditor controller, Vector2 position) : base(controller, position) {
      }

      public override void DrawContent() {
        EditorGUILayout.LabelField("testis");
        EditorGUI.Popup(new Rect(1, 30, rect.width - 2, 10), 0, _components.ToArray());
        EditorGUI.TextField(new Rect(1, 50, rect.width - 2, 30), "testis");
      }
    }
  }
}

