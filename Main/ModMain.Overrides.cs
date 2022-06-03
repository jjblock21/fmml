using System;
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
    //private bool tempCameraShake = false;
    private string tempSnapAngleText = "";

    private void OpenOverridesPage()
    {
        tempSnapAngleText = physicsToolSnapAngle.ToString(globalCulture);
    }

    private void UpdateOverridesFromTemp()
    {
        if (float.TryParse(tempSnapAngleText, NumberStyles.AllowDecimalPoint, globalCulture, out float result))
        {
            physicsToolSnapAngle = result;
            if (MapManager.PlayableMapLoaded)
                ToolManager.ChangeSnapAngle(result, SelectedTool.Torch);
        }
    }

    private void OverridesPage()
    {
        UI.Begin(Utilities.AppName, 10, 20, 300, 500, 25, 35, 10, 50, 25);
        //TODO: Add Map check
        UI.ZeroSpaceLabel("Physics tool snaping angle", 20);
        tempSnapAngleText = UI.Input(tempSnapAngleText);

        if (UI.NavigationButton("Apply"))
        {
            UpdateOverridesFromTemp();
            Pages.SelectPage("main");
        }
    }
}