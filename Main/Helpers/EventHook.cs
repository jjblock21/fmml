using Doozy.Engine;
using FireworksMania.UI;
using System;
using FireworksMania.Core.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class EventHook
    {
        public static void AddEvent_Message_NoArgs<T>(string eventName, Action callback) where T : Message
        {
            Message.AddListener(eventName, delegate (T c) { callback?.Invoke(); });
        }

        /*Removed dead code*/

        public static string OpenInvEventId { get => PlayerUIManager.EventIds.ShowInventory; }
        public static string CloseInvEventId { get => PlayerUIManager.EventIds.HideInventory; }
    }
}
