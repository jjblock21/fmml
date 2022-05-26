using Main.UI;
using Main;
using UnityEngine;
using Main.EnvironmentObserver;

public partial class ModMain : MonoBehaviour
{
    private void AddPages()
    {
        Pages.AddPage(MainPage, "main");
        Pages.AddPage(ToolsPage, "tools");
        Pages.AddPage(FireworksPage, "fireworks");
        Pages.AddPage(ExperimentalToolsPage, "experimental_tools");
        Pages.AddPage(AboutPage, "about");
        Pages.AddPage(ControlsPage, "controls");
        Pages.AddPage(HacksPage, "hacks");
        Pages.AddPage(SettingsPage, "settings");
        Pages.AddPage(TimePage, "time");
    }


    private void MainPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        if (UI.Button("Tools"))
            Pages.SelectPage("tools");
        if (UI.Button("Hacks"))
            Pages.SelectPage("hacks");
        if (UI.Button("Time and Weather"))
            Pages.SelectPage("time");
        UI.Space(25);
        if (UI.Button("Settings"))
        {
            OpenSettingsPage();
            Pages.SelectPage("settings");
        }
        UI.Space(25);
        if (UI.Button("About"))
            Pages.SelectPage("about");
        if (UI.Button("Controls"))
            Pages.SelectPage("controls");
        if (UI.NavigationButton("Hide"))
            visible = false;
    }

    #region Tools
    private void ToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 600, 25, 35, 10, 50, 25);
        flameThrowerActive = flameThrowerToggle.SwitchUI(UI.Button("Flamethrower", flameThrowerActive));
        if (UI.Button("Cloning machine", clonerActive))
        {
            clonerActive = clonerToggle.SwitchUI(true);
            crazyClonerActive = false;
            crazyClonerToggle.SetState(false);
        }
        eraserActive = eraserToggle.SwitchUI(UI.Button("Delete Tool", eraserActive));
        UI.DefSpace();
        if (UI.Button("Buggy Tools"))
            Pages.SelectPage("experimental_tools");
        UI.DefSpace();
        if (UI.Button("Fireworks Related"))
            Pages.SelectPage("fireworks");
        UI.DefSpace();
        if (UI.Button("Teleporter") && _controller != null)
        {
            TeleportDialog.ResetText();
            TeleportDialog.ShowDialog();
        }
        UI.DefSpace();
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }
    #endregion

    #region Fireworks
    private void FireworksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 500, 25, 35, 10, 50, 25);
        if (UI.Button("Ignite Everything")) Lighter.IgniteAll(true, igniteEverythingDelay);
        if (UI.Button("Instantly Ignite Everything")) Lighter.IgniteAll(false, 1);
        UI.Space(10);
        if (UI.Button("Clear Fireworks")) Stuff.ClearFireworks();
        UI.DefSpace();
        if (UI.Button("Unlock Tim")) Stuff.UnlockTim();
        if (UI.Button("Unlock Karlson")) Stuff.UnlockKarlson();
        UI.DefSpace();
        fireworksAutoSpawn = fireworksAutoSpawnToggle.SwitchUI(
            UI.Button("Fireworks Autospawn", fireworksAutoSpawn)
        );
        UI.DefSpace();
        if (UI.Button("Fuse all Fireworks"))
        {
            StartCoroutine(Stuff.FuseAll(connectAllFuseSpeed));
        }
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("tools");
    }
    #endregion

    #region Time
    public void TimePage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);

        // TODO: Set default name to currently active season/weather or Nothing on the loading screen
        if (weatherSelector.UICycle(UI.Button("Weather: " + weatherButtonLabel)))
        {
            if (timeManager.IsEnabled)
            {
                Weather weather = weatherSelector.GetSelectedEnumEntry();
                timeManager.ChangeWeather(weather);
                weatherButtonLabel = weatherSelector.GetSelectedName();
            }
        }

        if (timeSelector.UICycle(UI.Button("Time: " + timeButtonLabel)))
        {
            if (timeManager.IsEnabled)
            {
                TimeOfDay time = timeSelector.GetSelectedEnumEntry();
                timeManager.SetTimeOfDayPreset(time);
                timeButtonLabel = timeSelector.GetSelectedName();
            }
        }

        if (UI.NavigationButton("Back"))
        {
            Pages.SelectPage("main");
        }
    }
    #endregion

    #region About
    private void AboutPage()
    {
        UI.Begin("Fireworks Mania Modloader - About", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        UI.Label("Made by jjblock21\nInspired by FMenu.");
        UI.Label("Special thanks also go to Keltusar.");
        UI.Space(10);
        UI.Label("Fireworks Mania ModLoader\n" + Utilities.version);
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }
    #endregion

    #region Controls
    private void ControlsPage()
    {
        UI.Begin("Fireworks Mania Modloader - Controls", 10, 20, 300, 750, 25, 35, 10, 50, 25);
        UI.Label("F1: Show/Hide the Menu.");
        UI.Label("F2: Toggle keys, so you're be able to type.");
        UI.Space(10);
        UI.Label("Flamethrower Tool:\nC: Ingite Flammable Material.");
        UI.Space(10);
        UI.Label("Cloning Machine Tool:\nX: Clone object (For both clone tools)");
        UI.Space(10);
        UI.Label("Autoclicker Hack:\nR: Click every second Frame.");
        UI.Space(10);
        UI.Label("Delete Tool:\nV: Delete what you're looking at!");
        UI.Space(10);
        UI.Label("Ignite Everything:\nK: Ignite everything.\nL: Ignite Everything with specified delay.", 50);
        UI.Space(10);
        UI.Label("Fast navigation:\n" +
           "1: Physics Tool, 2: Lighter,\n" +
           "3: Fuse Tool, 4: Clear view\n" +
           "5: Time Tool, 6: Eraser Tool", 65
        );
        UI.Space(10);
        UI.Label("Newtonifier:\nT: Newtonify stuff.");
        UI.Space(10);
        UI.Label("Fly Mode:\n" +
           "G: Toggle Fly Mode.\n" +
           "Space: Fly up\n" +
           "Ctrl: Fly down", 65);
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }
    #endregion

    #region Hacks
    private void HacksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 500, 25, 35, 10, 50, 25);
        autoClickerActive = aToggle.SwitchUI(UI.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.SwitchUI(UI.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        UI.DefSpace();
        superSpeedActive = superSpeedToggle.SwitchUI(UI.Button("Super Speed", superSpeedActive));
        superJumpActive = superJumpToggle.SwitchUI(UI.Button("Super Jump", superJumpActive));
        UI.DefSpace();
        SpaceMode.ToggleSpaceMode(SpaceMode.spaceModeToggle.SwitchUI(UI.Button("Space Mode", SpaceMode.spaceModeActive)));
        FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.SwitchUI(UI.Button("Fly Mode", FlyMode.flyModeActive)));
        UI.DefSpace();
        //if (UI.Button("Delete Everything")) Actions.DeleteAll();
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }
    #endregion

    #region ExperimentalTools
    private void ExperimentalToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        UI.FancyLabel("Warning: These Tools are Experimental\nand can be buggy.", 30, Utilities.CreateColorGUIStyle(Color.yellow));
        newtonifierActive = newtonifierToggle.SwitchUI(UI.Button("Newtonifier", newtonifierActive));
        if (UI.Button("Crazy Cloner", crazyClonerActive))
        {
            crazyClonerActive = crazyClonerToggle.SwitchUI(true);
            clonerActive = false;
            clonerToggle.SetState(false);
        }
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("tools");
    }
    #endregion
}