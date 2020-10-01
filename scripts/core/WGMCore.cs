using UnityEngine;
using Wowsome.Core;

namespace Wowsome {
  namespace GameMaker {
    public interface IRootController {
      RectTransform MainRoot { get; }
      //   void RetainComponent(IComponent comp);
      //   void OnComponentProcessed(IComponentController processor, IComponent comp);
      //   void BeginComponentInteraction(PtdObject obj);
      //   void ProcessComponentInteraction(PtdObject obj);
      //   void BroadcastEvent(PtdObject obj, BaseSenderEv ev);
      //   void ToScene(string sceneId);
      //   PtdObject GetObjectById(string id);
    }

    public interface IComponent {
      string ComponentId { get; set; }
      WGMObject Owner { get; set; }
      void InitComponent(ISceneStarter sceneStarter, IRootController gameController);
      void OnAwakeGame();
      void OnStartGame();
      void Reinit();
      void OnLoaded();
    }

    public interface IActiveComponent : IComponent {
      bool IsActive { get; set; }
      //   bool OnReceiveEvent(BaseSenderEv ev, EventScope scope);
      //   void BroadcastEvent(BaseSenderEv ev);
    }

    public interface IUpdateableComponent : IComponent {
      void UpdateComponent(float dt);
    }
  }
}