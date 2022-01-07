using FireworksMania.Common;
using FireworksMania.Core.Common;
using FireworksMania.UI.ToolsMenu;
using System;
using System.Reflection;
using UnityEngine;

namespace FModApi
{
    public static class Tool
    {
        public static void SetSelectedTool(SelectedTool tool)
        {
            var toolManager = UnityEngine.Object.FindObjectOfType<ToolMenuManager>();
            GameObject obj = null;
            if (toolManager == null)
            {
                obj = new GameObject("ToolManager");
                toolManager = obj.AddComponent<ToolMenuManager>();
            }
            switch (tool)
            {
                case SelectedTool.FuseTool:
                    toolManager.SelectFuseConnectionTool();
                    break;
                case SelectedTool.Hand:
                    toolManager.SelectHandTool();
                    break;
                case SelectedTool.None:
                    toolManager.SelectNoneTool();
                    break;
                case SelectedTool.PhysicsTool:
                    toolManager.SelectPhysicsTool();
                    break;
                case SelectedTool.Torch:
                    toolManager.SelectIgnitorTool();
                    break;
                case SelectedTool.DeleteTool:
                    TrySelectEraser(toolManager);
                    break;
                case SelectedTool.TimeTool:
                    toolManager.SelectDayNightTimeTool();
                    break;
            }
            if (obj == null) return;
            UnityEngine.Object.Destroy(obj);
        }

        private static void TrySelectEraser(ToolMenuManager toolManager)
        {
            toolManager.SelectEraserTool();
        }
    }

    public enum SelectedTool : uint
    {
        None = 0,
        Hand = 1,
        Torch = 3,
        PhysicsTool = 2,
        FuseTool = 4,
        DeleteTool = 5,
        TimeTool = 6
    }
}
