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
            var toolHandler = UnityEngine.Object.FindObjectOfType<ToolMenuManager>();
            GameObject obj = null;
            if (toolHandler == null)
            {
                obj = new GameObject("ToolManager");
                toolHandler = obj.AddComponent<ToolMenuManager>();
            }
            switch (tool)
            {
                case SelectedTool.FuseTool:
                    toolHandler.SelectFuseConnectionTool();
                    break;
                case SelectedTool.Hand:
                    toolHandler.SelectHandTool();
                    break;
                case SelectedTool.None:
                    toolHandler.SelectNoneTool();
                    break;
                case SelectedTool.PhysicsTool:
                    toolHandler.SelectPhysicsTool();
                    break;
                case SelectedTool.Torch:
                    toolHandler.SelectIgnitorTool();
                    break;
                case SelectedTool.DeleteTool:
                    TrySelectEraser(toolHandler);
                    break;
            }
            if (obj == null) return;
            UnityEngine.Object.Destroy(obj);
        }

        private static void TrySelectEraser(ToolMenuManager t)
        {
            t.SelectEraserTool();
        }

        public static uint GetToolId(SelectedTool tool) => (uint)tool;
    }

    public enum SelectedTool : uint
    {
        None = 0,
        Hand = 1,
        Torch = 3,
        PhysicsTool = 2,
        FuseTool = 4,
        DeleteTool = 5
    }
}
