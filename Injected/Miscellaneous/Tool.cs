using FireworksMania.Common;
using FireworksMania.Core.Common;
using FireworksMania.UI.ToolsMenu;
using System.Reflection;
using UnityEngine;

namespace FModApi
{
    public static class Tool
    {
        public static void SetSelectedTool(SelectedTool tool)
        {
            var toolManager = Object.FindObjectOfType<ToolMenuManager>();
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
                    toolManager.SelectEraserTool();
                    break;
                case SelectedTool.TimeTool:
                    toolManager.SelectDayNightTimeTool();
                    break;
            }
            if (obj == null) return;
            Object.Destroy(obj);
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
