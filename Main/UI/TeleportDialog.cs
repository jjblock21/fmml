using Main.EnvironmentObserver;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Main.UI
{
    public class TeleportDialog
    {

        private static string teleportLocationX = "0";
        private static string teleportLocationY = "0";
        private static string teleportLocationZ = "0";
        private static bool isSelectionState = false;
        public static bool blockDialog = false;

        private static Dictionary<LocationInfo, Vector3> tpLocations = new Dictionary<LocationInfo, Vector3>();
        private static FirstPersonController fpsController = null;

        public static void InitLocations()
        {
            tpLocations.Add(new LocationInfo("Tim", Map.Town), new Vector3(47.5f, 1.5f, 31.2f));
            tpLocations.Add(new LocationInfo("Big House", Map.Town), new Vector3(4f, 5f, -17.5f));
            tpLocations.Add(new LocationInfo("Mountain", Map.Town), new Vector3(118f, 44f, 55f));
            tpLocations.Add(new LocationInfo("Flamingo", Map.Town), new Vector3(-344.8f, 43f, -61.1f));

            tpLocations.Add(new LocationInfo("Tim", Map.Ranch), new Vector3(-44f, 0.5f, -17.5f));
            tpLocations.Add(new LocationInfo("Wind Turbine", Map.Ranch), new Vector3(-69.6f, 47.5f, -240.0f));
            tpLocations.Add(new LocationInfo("Barn", Map.Ranch), new Vector3(15f, 1f, -24f));

            tpLocations.Add(new LocationInfo("Park", Map.City), new Vector3(54.25f, 2.5f, 46.5f));
            tpLocations.Add(new LocationInfo("Gas Station", Map.City), new Vector3(115f, 2f, 77f));
            tpLocations.Add(new LocationInfo("City Hall", Map.City), new Vector3(-50f, 2.5f, -95f));
        }

        public static void FindComponents()
        {
            fpsController = Object.FindObjectOfType<FirstPersonController>();
        }

        public static void UpdateDialog()
        {
            if (blockDialog) return;
            Rect controlRect = UI.GetGraphicsRect();
            if (!isSelectionState)
            {
                Vector2 pos = new Vector2(controlRect.x + controlRect.width + 10, controlRect.y + 15);
                UI.Begin("Teleport", pos.x, pos.y, 300, 400, 25, 35, 10, 50, 20);
                UI.Label("Input a location (X, Y, Z)");

                teleportLocationX = UI.Input(teleportLocationX);
                teleportLocationY = UI.Input(teleportLocationY);
                teleportLocationZ = UI.Input(teleportLocationZ);

                if (UI.Button("Presets")) isSelectionState = true;
                UI.Space(20);
                if (UI.Button("Teleport"))
                {
                    Teleport();
                    Pages.CloseCurrentDialog();
                }
                if (UI.NavigationButton("Cancel"))
                    Pages.CloseCurrentDialog();
                return;
            }
            Vector2 pos1 = new Vector2(controlRect.x + controlRect.width + 10, controlRect.y);
            UI.Begin("Teleport", pos1.x, pos1.y, 300, 450, 25, 35, 10, 50, 20);
            CreateButtons();
            if (UI.NavigationButton("Back")) isSelectionState = false;
        }

        private static void CreateButtons()
        {
            Map map = MapManager.GetLoadedMap();
            foreach (KeyValuePair<LocationInfo, Vector3> pair in tpLocations)
            {
                if (pair.Key.Map != map) continue;
                if (UI.Button(pair.Key.Name))
                {
                    TeleportToLocation(pair.Value);
                    Pages.CloseCurrentDialog();
                }
            }
        }

        private static void Teleport()
        {
            Vector3 tpPos = new Vector3(
                fpsController.gameObject.transform.position.x,
                fpsController.gameObject.transform.position.y,
                fpsController.gameObject.transform.position.z
            );
            if (float.TryParse(teleportLocationX, out float x)
            && float.TryParse(teleportLocationY, out float y)
            && float.TryParse(teleportLocationZ, out float z))
                tpPos = new Vector3(x, y, z);
            TeleportToLocation(tpPos);
        }

        public static void ShowDialog()
        {
            Pages.OpenDialog(UpdateDialog);
        }

        public static void ResetText()
        {
            isSelectionState = false;
            teleportLocationX = "0";
            teleportLocationY = "0";
            teleportLocationZ = "0";
        }

        private static void TeleportToLocation(Vector3 pos)
        {
            if (fpsController != null) fpsController.ResetMovement(pos);
        }
    }

    public struct LocationInfo
    {
        private string name;
        private Map map;

        public LocationInfo(string name, Map map)
        {
            this.name = name;
            this.map = map;
        }

        public Map Map { get { return map; } }
        public string Name { get { return name; } }
    }
}