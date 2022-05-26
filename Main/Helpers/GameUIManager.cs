using Doozy.Engine;
using System;

namespace Helpers
{
    public static class GameUIManager
    {
        public static bool isInventoryOpen = false;

        public static event Action<bool> InventoryEvent;

        public static void AddEvents()
        {
            EventHook.AddEvent_Message_NoArgs<GameEventMessage>(EventHook.OpenInvEventId, OnOpenInventory);
            EventHook.AddEvent_Message_NoArgs<GameEventMessage>(EventHook.CloseInvEventId, OnCloseInventory);
        }

        private static void OnCloseInventory()
        {
            isInventoryOpen = false;
            CallInventoryEvent(false);
        }

        private static void OnOpenInventory()
        {
            isInventoryOpen = true;
            CallInventoryEvent(true);
        }

        private static void CallInventoryEvent(bool open)
        {
            Action<bool> a = InventoryEvent;
            a?.Invoke(open);
        }

        //TODO: Dead code
        public static void SetInventoryActive(bool active)
        {
            if (active)
            {
                GameEventMessage.SendEvent(EventHook.OpenInvEventId);
                return;
            }
            GameEventMessage.SendEvent(EventHook.CloseInvEventId);
        }
    }
}
