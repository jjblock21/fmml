using Main.UI;
using System;
using Main.EnvironmentObserver;

public partial class ModMain
{
    private int tempSettingsIED = 0;
    private int tempSettingsFSR = 0;
    private FuseConnectionType tempSettingsFCS = 0;

    private void OpenSettingsPage()
    {
        tempSettingsIED = igniteEverythingDelay;
        tempSettingsFSR = fireworksAutoSpawnObject.Rarity;
        tempSettingsFCS = connectAllFuseSpeed;
    }

    private void UpdateSettingsFromTemp()
    {
        UpdateSuperJump(superJumpActive);
        UpdateSuperSpeed(superSpeedActive);

        igniteEverythingDelay = tempSettingsIED;
        fireworksAutoSpawnObject.Rarity = tempSettingsFSR;

        fireworksAutoSpawnObject.UpdateSpawnAllFireworksSettings(autoSpawnAllFireworks);

        connectAllFuseSpeed = tempSettingsFCS;
    }

    private void SettingsPage()
    {
        try
        {
            UI.Begin("Fireworks Mania Modloader - Settings", 10, 20, 300, 500, 25, 35, 10, 50, 25);

            jumpHeight = (int)UI.LabelSlider("Super Jump Force", jumpHeight, 1, 50);
            speed = (int)UI.LabelSlider("Speed", speed, 1, 50);

            UI.DefSpace();
            tempSettingsIED = (int)UI.LabelSlider("Ignite all Delay", tempSettingsIED, 1, 1000);
            UI.DefSpace();
            tempSettingsFSR = (int)UI.LabelSlider("Fireworks Spawn Rarity", tempSettingsFSR, 2, 50);

            UI.ZeroSpaceLabel("Firework Spawn Mode", 15);
            if (autoSpawnAllFireworks)
            {
                if (UI.Button("All Fireworks (includes Mods)")) autoSpawnAllFireworks = false;
            }
            else if (UI.Button("Cakes and Rockets")) autoSpawnAllFireworks = true;

            UI.DefSpace();
            UI.ZeroSpaceLabel("Fuse Speed", 15);
            //TODO: Replace with new selectionWheel
            if (UI.Button(Enum.GetName(typeof(FuseConnectionType), tempSettingsFCS)))
            {
                int newValue = (int)tempSettingsFCS + 1;
                if (newValue > 3) newValue = 0;
                tempSettingsFCS = (FuseConnectionType)newValue;
            }

            if (UI.NavigationButton("Apply"))
            {
                UpdateSettingsFromTemp();
                Pages.SelectPage("main");
            }
        }
        catch (Exception e)
        {
            UI.Begin("Fireworks Mania Modloader - Settings", 10, 20, 300, 200, 25, 35, 10, 50, 25);
            UI.Label(e.Message + "\n" +
                "An unspecified error has occurred\n" +
                "while drawing the page.");
            if (UI.NavigationButton("Back"))
                Pages.SelectPage("main");
        }
    }
}