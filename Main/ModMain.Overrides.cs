﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Main;
using Main.EnvironmentObserver;
using Main.UI;


public partial class ModMain
{
    private Toggle cameraShakeToggle = new Toggle();
    private bool tempCameraShake = true;

    private string tempSnapAngleText = "";

    private void OpenOverridesPage()
    {
        tempSnapAngleText = physicsToolSnapAngle.ToString(globalCulture);
        cameraShakeToggle.SetState(cameraShakeEnabled);
        tempCameraShake = cameraShakeEnabled;
    }

    private void UpdateOverridesFromTemp()
    {
        if (float.TryParse(tempSnapAngleText, NumberStyles.AllowDecimalPoint, globalCulture, out float result))
        {
            physicsToolSnapAngle = result;
            if (MapManager.PlayableMapLoaded)
                ToolManager.ChangeSnapAngle(result, SelectedTool.Torch);
        }
        cameraShakeEnabled = cameraShakeToggle.State;
        Stuff.CameraShake(cameraShakeToggle.State);
    }

    private void OverridesPage()
    {
        UI.Begin(Utilities.AppName, 10, 20, 300, 500, 25, 35, 10, 50, 25);
        //TODO: Add Map check
        UI.ZeroSpaceLabel("Physics tool snaping angle", 20);
        tempSnapAngleText = UI.Input(tempSnapAngleText);
        UI.DefSpace();
        tempCameraShake = cameraShakeToggle.SwitchUI(
            UI.Button("Camera Shake: ", tempCameraShake)
        );
        if (UI.NavigationButton("Apply"))
        {
            // TODO: Debug feature (remove pls)
            UpdateOverridesFromTemp();
            Pages.SelectPage("main");
        }
    }
}