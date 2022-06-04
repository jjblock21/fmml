using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Main;
using Main.EnvironmentObserver;
using Main.Features;
using Main.UI;


public partial class ModMain
{
    private Toggle cameraShakeToggle = new Toggle();
    private bool tempCameraShake = true;

    private string tempSnapAngleText = "";
    private bool errorLastApply = false;

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
        else
        {
            errorLastApply = true;
            return;
        }
        cameraShakeEnabled = cameraShakeToggle.State;
        CameraShake.UpdateCameraShake(cameraShakeToggle.State);
    }

    private void OverridesPage()
    {
        UI.Begin(Utilities.AppName, 10, 20, 300, 500, 25, 35, 10, 50, 25);
        //TODO: Add Map check
        UI.ZeroSpaceLabel("Physics Tool Snaping Angle", 20);
        tempSnapAngleText = UI.Input(tempSnapAngleText);
        UI.DefSpace();
        tempCameraShake = cameraShakeToggle.SwitchUI(
            UI.Button("Camera Shake", tempCameraShake)
        );
        if (errorLastApply)
        {
            UI.Space(10);
            UI.FancyLabel("Failed to apply overrides!", 20,
                Utilities.CreateColorGUIStyle(UnityEngine.Color.red));
        }
        if (UI.NavigationButton("Apply"))
        {
            errorLastApply = false;
            // TODO: Debug feature (remove pls)
            UpdateOverridesFromTemp();
            if (errorLastApply) return;
            Pages.SelectPage("main");
        }
    }
}