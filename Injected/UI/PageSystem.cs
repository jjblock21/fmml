using Main.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Injected.UI
{
    public static class PageSystem
    {
        private static List<PageDrawCallTarget> pages = new List<PageDrawCallTarget>();
        private static DialogDrawCallTarget _dialog = null;
        private static int selectedIndex = 0;

        public static void AddPage(PageDrawCallTarget page)
        {
            pages.Add(page);
        }
        public static void SelectPage(int index)
        {
            selectedIndex = index;
        }

        public static void OpenDialog(DialogDrawCallTarget dialog)
        {
            _dialog = dialog;
        }
        public static void CloseCurrentDialog()
        {
            _dialog = null;
        }

        public static void MakeDrawCalls()
        {
            pages[selectedIndex]?.Invoke();
            _dialog?.Invoke();
        }

        public delegate void PageDrawCallTarget();
        public delegate void DialogDrawCallTarget();
    }
}
