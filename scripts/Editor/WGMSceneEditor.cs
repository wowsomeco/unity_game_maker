using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wowsome.Node;

namespace Wowsome {
  namespace GameMaker {
    public class WGMSceneEditor : WowNodeEditor<WGMObjectEditor> {
      WGMSceneController _sceneController = null;

      [MenuItem("Wowsome/Game Maker")]
      private static void OpenWindow() {
        WGMSceneEditor window = GetWindow<WGMSceneEditor>();
        window.titleContent = new GUIContent("Game Maker");
      }

      protected override void OnEnable() {
        base.OnEnable();
        Refresh();
        OnAddNode += pos => {
          return new WGMObjectEditor(this, pos);
        };
      }

      void OnFocus() {
        Refresh();
      }

      void Refresh() {
        Scene curScene = EditorSceneManager.GetActiveScene();
        foreach (GameObject go in curScene.GetRootGameObjects()) {
          WGMSceneController controller = go.GetComponent<WGMSceneController>();
          if (null != controller) {
            _sceneController = controller;
            break;
          }
        }

        if (null != _sceneController) {

        }
      }

      void AddGameObject() {

      }
    }
  }
}

