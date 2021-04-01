using System;
using System.Collections.Generic;

namespace Injected.UI
{
    public static class PageSystem
    {
        private static List<Action> methods = new List<Action>();
        private static Action _dialog = null;
        private static int selectedIndex = 0;

        public static void AddPage(Action method) => methods.Add(method);
        public static void SelectPage(int index) => selectedIndex = index;

        public static void SetDialog(Action dialog) => _dialog = dialog;
        public static void RemoveActiveDialog() => _dialog = null;

        public static void DrawPage()
        {
            methods[selectedIndex]();
            if (_dialog != null) _dialog();
        }
    }
}