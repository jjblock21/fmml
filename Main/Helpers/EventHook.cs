using Doozy.Engine;
using FireworksMania.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class EventHook
    {
        public static void AddEvent<T>(string eventName, Action callback) where T : Message
        {
            Message.AddListener(eventName, delegate (T c) { callback?.Invoke(); });
        }

        public static void CallGameEvent(string gameEvent)
        {
            GameEventMessage.SendEvent(gameEvent);
        }

        public static string OpenInventoryEvent { get => PlayerUIManager.EventIds.ShowInventory; }
        public static string CloseInventoryEvent { get => PlayerUIManager.EventIds.HideInventory; }
    }
}
