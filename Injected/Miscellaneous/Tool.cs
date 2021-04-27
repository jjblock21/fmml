using FireworksMania.Common;
using FireworksMania.UI.ToolsMenu;
using System;
using System.Reflection;
using UnityEngine;

namespace Injected
{
    public static class Tool
    {
        public static void Init()
        {

        }

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
            try
            {
                t.SelectEraserTool();
            }
            catch
            {
                Debug.LogError("FMML Error: EraserTool is not available in this version of Fireworks Mania.");
            }
        }

        public static uint GetToolId(SelectedTool tool) => (uint)tool;

        [Obsolete]
        private static void BroadcasatToolEvent(ToolMenuItemSelectedMessengerEvent.ToolMenuItemType type)
        {
            //var reflector = new FMAssemblyReflector();
            /*
            var types = new[] { typeof(string), typeof(object) };
            var assemblyType = reflector.GetAssemblyType("Messenger");
            var method = reflector.GetMethod(assemblyType, "Broadcast", types, BindingFlags.Static | BindingFlags.Public);
            Debug.LogError(assemblyType.ToString());
            Debug.LogError(types.ToString());
            if (method == null) Debug.LogError("METHOD was null");
            reflector.InvokeStaticGenericMethod(
                null,
                method,
                new object[]
                {
                    "ToolMenuItemSelected",
                    new ToolMenuItemSelectedMessengerEvent(type)
                },
                typeof(ToolMenuItemSelectedMessengerEvent)
            );
            */
            /*var assembly = reflector.GetAssembly();
            var t = assembly.CreateInstance("Messenger").GetType();
            var method = reflector.GetMethod(t, "Broadcast", new[] { typeof(string), typeof(ToolMenuItemSelectedMessengerEvent) }, BindingFlags.Static | BindingFlags.Public);
            if (method == null) Debug.LogError("METHOD was null");
            reflector.InvokeStaticGenericMethod(
                null, method,
                new object[]
                {
                    "ToolMenuItemSelected",
                    new ToolMenuItemSelectedMessengerEvent(type)
                },
                typeof(ToolMenuItemSelectedMessengerEvent)
            );*/
        }
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
