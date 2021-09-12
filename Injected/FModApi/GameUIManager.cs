using Doozy.Engine;
using System;

namespace FModApi
{
    public static class GameUIManager
    {
        public static bool isInventoryOpen = false;

        public static event Action<bool> InventoryEvent;

        public static void AddEvents()
        {
            EventHook.AddEvent<GameEventMessage>(EventHook.OpenInventoryEvent, OnOpenInventory);
            EventHook.AddEvent<GameEventMessage>(EventHook.CloseInventoryEvent, OnCloseInventory);
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

        public static void OpenInventory() => SetInventoryActive(true);
        public static void CloseInventory() => SetInventoryActive(false);

        public static void SetInventoryActive(bool active)
        {
            if (active) EventHook.CallGameEvent(EventHook.OpenInventoryEvent);
            else EventHook.CallGameEvent(EventHook.CloseInventoryEvent);
        }
    }
}
