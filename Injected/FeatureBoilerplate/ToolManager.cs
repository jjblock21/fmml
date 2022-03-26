using FireworksMania;
using FireworksMania.UI.ToolsMenu;
using UnityEngine;

namespace FModApi
{
    public static class ToolManager
    {
        public static void SelectTool(SelectedTool tool)
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

        public static void FreezeTimeTool(SelectedTool toolAfterChange)
        {
            SelectTool(SelectedTool.TimeTool);
            InternalTimeFreeze();
            SelectTool(toolAfterChange);
        }

        private static void InternalTimeFreeze()
        {
            DayNightTimeTool timeTool = Object.FindObjectOfType<DayNightTimeTool>();
            if (timeTool == null) Debug.LogWarning("Attempted to modify time without active TimeTool instance.");
            GameReflector gameReflector = new GameReflector(timeTool);
            gameReflector.SetFieldValue("_currentDayNightTimeSpeedIndex", 0);
            gameReflector.InvokeMethod("UpdateWatchArm");
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