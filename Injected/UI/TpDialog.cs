﻿using FireworksMania.Common;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Injected.UI
{
    public class TpDialog
    {

        private static string TpX = "", TpY = "", TpZ = "";

        private static bool isSelectionState = false;

        private static GameObject P => Object.FindObjectOfType<FirstPersonController>().gameObject;

        public static void UpdateDialog()
        {
            if (!isSelectionState)
            {
                Vector2 position = new Vector2(10, UIHelper.GetGraphicsRect().height + 45);
                DialogHelper.Begin("Teleport", position.x, position.y, 300, 400, 25, 35, 10, 50, 20);
                Vector3 tpPos = new Vector3(P.transform.position.x, P.transform.position.y, P.transform.position.z);
                DialogHelper.Label("Input a location (X, Y, Z)");
                TpX = DialogHelper.Input(TpX);
                TpY = DialogHelper.Input(TpY);
                TpZ = DialogHelper.Input(TpZ);
                if (DialogHelper.Button("Teleport"))
                {
                    if (float.TryParse(TpX, out float x) && float.TryParse(TpY, out float y) && float.TryParse(TpZ, out float z))
                        tpPos = new Vector3(x, y, z);
                    Teleport(tpPos);
                    PageSystem.RemoveActiveDialog();
                }
                DialogHelper.Space(20);
                if (DialogHelper.Button("Presets")) isSelectionState = true;
                if (DialogHelper.BottomNavigationButton("Cancel")) PageSystem.RemoveActiveDialog();
            }
            else
            {
                Vector2 position = new Vector2(10, UIHelper.GetGraphicsRect().height + 45);
                DialogHelper.Begin("Teleport", position.x, position.y, 300, 400, 25, 35, 10, 50, 20);
                if (DialogHelper.Button("Tim - Town"))
                {
                    Teleport(new Vector3(47.5f, 1.5f, 31.2f));
                    PageSystem.RemoveActiveDialog();
                }
                if (DialogHelper.Button("Tim - Ranch"))
                {
                    Teleport(new Vector3(-44f, 0.5f, -17.5f));
                    PageSystem.RemoveActiveDialog();
                }
                if (DialogHelper.Button("Flamingo - Town"))
                {
                    Teleport(new Vector3(-344.8f, 43f, -61.1f));
                    PageSystem.RemoveActiveDialog();
                }
                if (DialogHelper.Button("Wind Turbine - Ranch"))
                {
                    Teleport(new Vector3(-69.6f, 47.5f, -240.0f));
                    PageSystem.RemoveActiveDialog();
                }
                if (DialogHelper.BottomNavigationButton("Back")) isSelectionState = false;
            }
        }

        public static void ShowDialog() => PageSystem.SetDialog(UpdateDialog);
        public static void HideDialog() => PageSystem.RemoveActiveDialog();

        public static void ResetText()
        {
            isSelectionState = false;
            TpX = "";
            TpY = "";
            TpZ = "";
        }

        private static void Teleport(Vector3 pos)
        {
            if (P != null) P.transform.position = pos;
        }
    }
}
