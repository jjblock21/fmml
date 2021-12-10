using Main.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Injected.UI
{
    public static class PageSystem
    {
        private static List<PageDrawCallTarget> pageList = new List<PageDrawCallTarget>();
        private static Dictionary<string, int> indexDictionary = new Dictionary<string, int>();
        private static DialogDrawCallTarget _dialog = null;
        private static int selectedPage = 0;

        public static void AddPage(PageDrawCallTarget page, string name)
        {
            pageList.Add(page);
            indexDictionary.Add(name, pageList.Count - 1);
        }

        public static void SelectPage(string name)
        {
            if (indexDictionary.TryGetValue(name, out int index))
            {
                selectedPage = index;
            }
        }

        public static void OpenDialog(DialogDrawCallTarget dialog)
        {
            _dialog = dialog;
        }
        public static void CloseCurrentDialog()
        {
            _dialog = null;
        }

        public static void ClearPages()
        {
            pageList.Clear();
            indexDictionary.Clear();
        }

        public static void MakeDrawCalls()
        {
            _dialog?.Invoke();
            pageList[selectedPage]?.Invoke();
        }

        public delegate void PageDrawCallTarget();
        public delegate void DialogDrawCallTarget();
    }
}
