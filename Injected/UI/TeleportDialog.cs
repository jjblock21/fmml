using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Injected.UI
{
    public class TeleportDialog
    {

        private static string tpX = "", tpY = "", tpZ = "";

        private static bool isSelectionState = false;

        private static Dictionary<string, Vector3> tpLocations = new Dictionary<string, Vector3>();

        public static void InitLocations()
        {
            tpLocations.Add("Tim - Town", new Vector3(47.5f, 1.5f, 31.2f));
            tpLocations.Add("Tim - Ranch", new Vector3(-44f, 0.5f, -17.5f));
            tpLocations.Add("Flamingo - Town", new Vector3(-344.8f, 43f, -61.1f));
            tpLocations.Add("Wind Turbine - Ranch", new Vector3(-69.6f, 47.5f, -240.0f));
            tpLocations.Add("Big House - Town", new Vector3(4f, 5f, -17.5f));
            tpLocations.Add("Mountain - Town", new Vector3(118f, 44f, 55f));
            tpLocations.Add("Barn - Ranch", new Vector3(15f, 1f, -24f));
        }

        public static void ChacheComponents()
        {
            p = Object.FindObjectOfType<FirstPersonController>();
        }

        private static FirstPersonController p = null;

        public static void UpdateDialog()
        {
            Rect controlRect = ModUI.GetGraphicsRect();
            if (!isSelectionState)
            {
                Vector2 position = new Vector2(controlRect.x + controlRect.width + 10, controlRect.y + 15);
                DialogUI.Begin("Teleport", position.x, position.y, 300, 400, 25, 35, 10, 50, 20);
                Vector3 tpPos = new Vector3(p.gameObject.transform.position.x, p.gameObject.transform.position.y, p.gameObject.transform.position.z);
                DialogUI.Label("Input a location (X, Y, Z)");
                tpX = DialogUI.Input(tpX);
                tpY = DialogUI.Input(tpY);
                tpZ = DialogUI.Input(tpZ);
                if (DialogUI.Button("Presets")) isSelectionState = true;
                DialogUI.Space(20);
                if (DialogUI.Button("Teleport"))
                {
                    if (float.TryParse(tpX, out float x) && float.TryParse(tpY, out float y) && float.TryParse(tpZ, out float z))
                        tpPos = new Vector3(x, y, z);
                    Teleport(tpPos);
                    PageSystem.RemoveActiveDialog();
                }
                if (DialogUI.BottomNavigationButton("Cancel")) PageSystem.RemoveActiveDialog();
            }
            else
            {
                Vector2 position = new Vector2(controlRect.x + controlRect.width + 10, controlRect.y + 15);
                DialogUI.Begin("Teleport", position.x, position.y, 300, 450, 25, 35, 10, 50, 20);
                CreateButtons();
                if (DialogUI.BottomNavigationButton("Back")) isSelectionState = false;
            }
        }

        private static void CreateButtons()
        {
            foreach (KeyValuePair<string, Vector3> pair in tpLocations)
            {
                if (DialogUI.Button(pair.Key))
                {
                    Teleport(pair.Value);
                    PageSystem.RemoveActiveDialog();
                }
            }
        }

        public static void ShowDialog() => PageSystem.SetDialog(UpdateDialog);
        public static void HideDialog() => PageSystem.RemoveActiveDialog();

        public static void ResetText()
        {
            isSelectionState = false;
            tpX = "";
            tpY = "";
            tpZ = "";
        }

        private static void Teleport(Vector3 pos)
        {
            if (p != null) p.ResetMovement(pos);
        }
    }
}
