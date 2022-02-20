using FireworksMania.Common;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Interactions.Tools;
using FireworksMania.Interactions.Tools.IgniteTool;
using FireworksMania.Props;
using FireworksMania.UI;
using Injected;
using Injected.UI;
using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Main.Setup;
using FModApi;
using Main.EnvironmentObserver;
using Main;
using Main.UI;
using Doozy.Engine;
using FireworksMania.Input;
using Main.Miscellaneous;

[AttachToGame(AttachMode.ModObject)]
public class ModMain : MonoBehaviour
{
    #region Variables
    private Toggle visibleToggle = new Toggle(true);
    public static bool visible = true;

    private int igniteEverythingDelay = 1;
    private int jumpHeight = 25;
    private int speed = 50;

    private bool disableKeys = false;

    private Toggle crazyClonerToggle = new Toggle();
    private Toggle newtonifierToggle = new Toggle();
    private bool crazyClonerActive = false;
    private bool newtonifierActive = false;

    public bool superSpeedActive = false;
    public Toggle superSpeedToggle = new Toggle();
    public bool superJumpActive = false;
    public Toggle superJumpToggle = new Toggle();

    public bool autoClickerActive = false;
    private bool autoClickerButtonLeft = true;

    private Toggle aToggle = new Toggle();
    private Toggle aToggle2 = new Toggle();
    private Toggle aToggle3 = new Toggle();

    public Toggle eraserToggle = new Toggle();
    public Toggle flameThrowerToggle = new Toggle();
    public Toggle clonerToggle = new Toggle();

    public bool clonerActive = false;
    public bool eraserActive = false;
    public bool flameThrowerActive = false;

    public static Camera _cam;
    public static Player _controller;

    public static Toggle fireworksAutoSpawnToggle = new Toggle();
    public static bool fireworksAutoSpawn = false;

    public bool autoSpawnAllFireworks = false;

    private LineManager markerLineRenderer = new LineManager();
    private FireworksAutoSpawn fireworksAutoSpawnObject = new FireworksAutoSpawn();
    private TimeManager timeManager = new TimeManager();

    private FuseConnectionType connectAllFuseSpeed = FuseConnectionType.Fast;

    #endregion

    #region Start
    public void Start()
    {
        // Add Events
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        superSpeedToggle.StateChanged += SsToggle_StateChanged;
        superJumpToggle.StateChanged += SjToggle_StateChanged;

        GameUIManager.AddEvents();
        GameUIManager.InventoryEvent += GameUIManager_InventoryEvent;

        // Chache Components
        FindComponentReferences();

        // Update the version label of the game
        StartCoroutine(VersionLabelCoroutine());

        // Init the Pagesystem.
        AddPages();

        // Add the Teleport Locations
        TeleportDialog.InitLocations();

        // Create GUI Textures on Runtime
        UIStyles.CreateTextures(250, 35);
        UIStyles.CreateStyles();

        fireworksAutoSpawnObject.UpdateSpawnAllFireworksSettings(autoSpawnAllFireworks);
    }

    private void GameUIManager_InventoryEvent(bool isOpen)
    {
        // Disable keys
        disableKeys = isOpen;

        // Disable Teleport Dialog
        TeleportDialog.blockDialog = isOpen;
    }
    #endregion

    #region SuperJump/Speed

    private void UpdateSuperJump(bool e)
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        if (firstPerson == null) return;
        GameReflector gr = new GameReflector(firstPerson);
        FieldInfo field = gr.GetField("m_JumpSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (e) field.SetValue(firstPerson, jumpHeight);
        else field.SetValue(firstPerson, 10);
    }

    private void UpdateSuperSpeed(bool e)
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        if (firstPerson == null) return;
        GameReflector gr = new GameReflector(firstPerson);
        FieldInfo field = gr.GetField("m_RunSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (e) field.SetValue(firstPerson, speed);
        else field.SetValue(firstPerson, 10);
    }

    private void SjToggle_StateChanged(object sender, bool e)
    {
        UpdateSuperJump(e);
    }

    private void SsToggle_StateChanged(object sender, bool e)
    {
        UpdateSuperSpeed(e);
    }

    #endregion

    #region Actions

    public void DoAutoclickerClick()
    {
        if (aToggle2.Switch())
        {
            if (!autoClickerButtonLeft) Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
            else Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
        }
        else
        {
            if (!autoClickerButtonLeft) Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
            else Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
        }
    }

    #endregion

    #region SilvesterSimulation

    private void UpdateSilvesterSimulation()
    {
        if (fireworksAutoSpawn)
        {
            fireworksAutoSpawnObject.UpdateFireworkSpawn();
        }
    }

    #endregion

    /*
     * Adding Pages to the PageSystem.
     */
    #region AddPages
    private void AddPages()
    {
        Pages.AddPage(MainPage, "main");
        Pages.AddPage(ToolsPage, "tools");
        Pages.AddPage(FireworksPage, "fireworks");
        Pages.AddPage(ExperimentalToolsPage, "experimental_tools");
        Pages.AddPage(AboutPage, "about");
        Pages.AddPage(ControlsPage, "controls");
        Pages.AddPage(HacksPage, "hacks");
        Pages.AddPage(MarkersPage, "markers");
        Pages.AddPage(SettingsPage, "settings");
        Pages.AddPage(TimePage, "time");
    }
    #endregion

    /*
     * Redo all the chaching and overriding when a new Scene is Loaded.
     */
    #region UpdateScene

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        FindComponentReferences();
        timeManager.Setup();
        StartCoroutine(VersionLabelCoroutine());
        UpdateSuperJump(superJumpActive);
        UpdateSuperSpeed(superSpeedActive);
        ResetDisableKeys();
    }
    #endregion

    private void ResetDisableKeys()
    {
        disableKeys = false;
        TeleportDialog.blockDialog = false;
    }

    #region Update
    public void Update()
    {
        // Auto Dissable Key

        // Disable/Enable Key Shortcuts with F2
        if (Input.GetKeyUp(KeyCode.F2))
        {
            if (disableKeys) disableKeys = false;
            else disableKeys = true;
        }

        if (disableKeys) goto endOfKeys;

        // FlameThrower
        if (Input.GetKey(KeyCode.C) && flameThrowerActive)
        {
            RaycastHit hit = Utilities.DoScreenRaycast(_cam);
            if (hit.collider != null)
                Lighter.SpawnFire(hit.collider.gameObject);
        }

        // Hide/Show
        if (Input.GetKeyDown(KeyCode.F1))
            visible = visibleToggle.Switch(true);

        //Cloner
        if (Input.GetKeyDown(KeyCode.X) && clonerActive)
        {
            RaycastHit hit = Utilities.DoScreenRaycast(_cam);
            if (hit.collider != null)
                Cloner.Clone(hit.collider, hit.point);
        }
        else if (Input.GetKeyDown(KeyCode.X) && crazyClonerActive)
        {
            RaycastHit hit = Utilities.DoScreenRaycast(_cam);
            Cloner.CrazyClone(hit.collider, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.T) && newtonifierActive)
        {
            RaycastHit hit = Utilities.DoScreenRaycast(_cam);
            if (hit.collider != null) Cloner.Newtonify(hit.collider);
        }

        // Delete Tool
        if (Input.GetKeyDown(KeyCode.V) && eraserActive)
        {
            RaycastHit hit = Utilities.DoScreenRaycast(_cam);
            if (hit.collider != null) Stuff.Delete(hit.collider);
        }

        // Auto Clicker
        if (Input.GetKey(KeyCode.R) && autoClickerActive)
        {
            DoAutoclickerClick();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.Switch(true));
        }

        // Ignite All
        if (Input.GetKeyDown(KeyCode.K)) Lighter.IgniteAll(false, 1);
        if (Input.GetKeyDown(KeyCode.L)) Lighter.IgniteAll(true, igniteEverythingDelay);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Tool.SetSelectedTool(SelectedTool.PhysicsTool);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Tool.SetSelectedTool(SelectedTool.Torch);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Tool.SetSelectedTool(SelectedTool.FuseTool);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Tool.SetSelectedTool(SelectedTool.None);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Tool.SetSelectedTool(SelectedTool.TimeTool);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Tool.SetSelectedTool(SelectedTool.DeleteTool);

        // Stuff in the update function that will not get skipped when disableKeys is on.
        endOfKeys:

        FlyMode.UpdateFlyMode();
        UpdateSilvesterSimulation();
    }

    #endregion

    /*
     * Where all the GUI stuff gets Updated.
     */
    #region OnGUI
    public void OnGUI()
    {
        if (visible)
        {
            Pages.MakeDrawCalls();
            if (disableKeys) UI.FancyLabel("Hotkeys are disabled!", "Enable using F2", 20, 40, 16, Color.red);
            if (Stuff.TryGetPositionString(out string text, _controller))
                UI.FancyLabel(text, "The players position", 20, 16, Color.white);
        }
        return;
    }
    #endregion

    #region Pages

    //Page 0
    private void MainPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        if (UI.Button("Tools"))
            Pages.SelectPage("tools");
        if (UI.Button("Hacks"))
            Pages.SelectPage("hacks");
        UI.Space(25);
        if (UI.Button("About"))
            Pages.SelectPage("about");
        if (UI.Button("Controls"))
            Pages.SelectPage("controls");
        UI.Space(25);
        if (UI.Button("Settings"))
        {
            OpenSettingsPage();
            Pages.SelectPage("settings");
        }
        if (UI.Button("Time and Weather"))
        {
            Pages.SelectPage("time");
        }
        if (UI.NavigationButton("Hide"))
            visible = false;
    }

    //Page 1
    private void ToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 600, 25, 35, 10, 50, 25);
        flameThrowerActive = flameThrowerToggle.Switch(UI.Button("Flamethrower", flameThrowerActive));
        if (UI.Button("Cloning machine", clonerActive))
        {
            clonerActive = clonerToggle.Switch(true);
            crazyClonerActive = false;
            crazyClonerToggle.SetState(false);
        }
        eraserActive = eraserToggle.Switch(UI.Button("Delete Tool", eraserActive));
        UI.DefSpace();
        if (UI.Button("Marker stuff"))
            Pages.SelectPage("markers");
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
        if (UI.Button("Legacy Physics Gun"))
        {
            Tool.SetSelectedTool(SelectedTool.Hand);
        }
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }

    private void FireworksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        if (UI.Button("Ignite Everything")) Lighter.IgniteAll(true, igniteEverythingDelay);
        if (UI.Button("Instantly Ignite Everything")) Lighter.IgniteAll(false, 1);
        UI.Space(10);
        if (UI.Button("Clear Fireworks")) Stuff.ClearFireworks();
        UI.DefSpace();
        if (UI.Button("Unlock Tim")) Stuff.UnlockTim();
        UI.DefSpace();
        fireworksAutoSpawn = fireworksAutoSpawnToggle.Switch(
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

    public void TimePage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);

        if (UI.NavigationButton("Apply"))
        {
            Pages.SelectPage("main");
        }
    }

    //Page 2
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

    //Page 3
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

    //Page 4
    private void HacksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 500, 25, 35, 10, 50, 25);
        autoClickerActive = aToggle.Switch(UI.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.Switch(UI.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        UI.DefSpace();
        superSpeedActive = superSpeedToggle.Switch(UI.Button("Super Speed", superSpeedActive));
        superJumpActive = superJumpToggle.Switch(UI.Button("Super Jump", superJumpActive));
        UI.DefSpace();
        SpaceMode.ToggleSpaceMode(SpaceMode.spaceModeToggle.Switch(UI.Button("Space Mode", SpaceMode.spaceModeActive)));
        FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.Switch(UI.Button("Fly Mode", FlyMode.flyModeActive)));
        UI.DefSpace();
        //if (UI.Button("Delete Everything")) Actions.DeleteAll();
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }

    // Page 5
    private void ExperimentalToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        UI.FancyLabel("Warning: These Tools are Experimental\nand can be buggy.", 30, Utilities.CreateColorGUIStyle(Color.yellow));
        newtonifierActive = newtonifierToggle.Switch(UI.Button("Newtonifier", newtonifierActive));
        if (UI.Button("Crazy Cloner", crazyClonerActive))
        {
            crazyClonerActive = crazyClonerToggle.Switch(true);
            clonerActive = false;
            clonerToggle.SetState(false);
        }
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("tools");
    }

    // Page 6
    private void MarkersPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        if (UI.Button("Place Marker"))
        {
            var m = Utilities.CreateMaterial(Utilities.GetRandomColor(), MaterialType.Unlit);
            var p = _controller.transform.position;
            markerLineRenderer.CreateLine(0.4f, m, new Vector3(p.x, 0, p.z), new Vector3(p.x, 100, p.z));
        }
        if (UI.Button("Clear Markers")) markerLineRenderer.ClearLines();
        if (UI.NavigationButton("Back"))
            Pages.SelectPage("main");
    }

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

    // Page 7
    private void SettingsPage()
    {
        try
        {
            UI.Begin("Fireworks Mania Modloader - Settings", 10, 20, 300, 600, 25, 35, 10, 50, 25);

            UI.ZeroSpaceLabel("Super Jump Force", 15);
            jumpHeight = UI.ClampedIntegerInput(jumpHeight, 1, 50);

            UI.ZeroSpaceLabel("Speed", 15);
            speed = UI.ClampedIntegerInput(speed, 1, 50);

            UI.DefSpace();
            UI.ZeroSpaceLabel("Ingite All Delay (ms)", 15);
            tempSettingsIED = UI.ClampedIntegerInput(tempSettingsIED, 1, 1000);

            UI.DefSpace();
            UI.ZeroSpaceLabel("Firework Spawn Rarity", 15);
            tempSettingsFSR = UI.ClampedIntegerInput(tempSettingsFSR, 2, 50);

            UI.ZeroSpaceLabel("Firework Spawn Mode", 15);
            if (autoSpawnAllFireworks)
            {
                if (UI.Button("All Fireworks (includes Mods)")) autoSpawnAllFireworks = false;
            }
            else if (UI.Button("Cakes and Rockets")) autoSpawnAllFireworks = true;

            UI.DefSpace();
            UI.ZeroSpaceLabel("Fuse Speed", 15);
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

    #endregion

    private void FindComponentReferences()
    {
        _cam = FindObjectOfType<Camera>();
        _controller = FindObjectOfType<Player>();

        MapManager.FindComponents();
        TeleportDialog.FindComponents();
        FireworkSpawner.FindComponents();
    }

    private IEnumerator VersionLabelCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        var label = FindObjectOfType<VersionLabel>();
        if (label == null) yield break;
        var text = label.gameObject.GetComponentInParent<TextMeshProUGUI>();
        if (text == null) yield break;
        text.transform.position = new Vector3(
            text.transform.position.x,
            text.transform.position.y - 10,
            text.transform.position.z);
        text.text = "v" + Application.version + "\nFMML " + Utilities.modVersion;
    }
}