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
using Main.FModApi;
using Main;
using Main.UI;
using Doozy.Engine;
using FireworksMania.DayNightCycle;
using FireworksMania.Input;

/*
 * FMML (Fireworks Mania ModLoader)
 * by jjblock21
 * Idk what to write here :)
 */

/*
 * Scenes
 * 
    1   Splash
    2   UnableToConnect
    3   Loading
    4   MainMenu
    5   Town                         [Map]
    6   Ranch                        [Map]
    7   Flat                         [Map]
    8   Lab (Not in the game rn)     [Map]
*/

[AttachToGame(AttachMode.ModObject)]
public class ModMain : MonoBehaviour
{
    /*
     * Creating instances of the toggle class.
     * TODO: Just make this better, idk.
     */
    #region Variables
    private ToggleClass visibleToggle = new ToggleClass(true);
    public static bool visible = true;

    private int igniteEverythingDelay = 1;
    private int jumpHeight = 25;
    private int speed = 50;

    private bool disableKeys = false;

    private ToggleClass crazyClonerToggle = new ToggleClass();
    private ToggleClass newtonifierToggle = new ToggleClass();
    private bool crazyClonerActive = false;
    private bool newtonifierActive = false;

    public bool superSpeedActive = false;
    public ToggleClass superSpeedToggle = new ToggleClass();
    public bool superJumpActive = false;
    public ToggleClass superJumpToggle = new ToggleClass();

    public bool autoClickerActive = false;
    private bool autoClickerButtonLeft = true;

    private ToggleClass aToggle = new ToggleClass();
    private ToggleClass aToggle2 = new ToggleClass();
    private ToggleClass aToggle3 = new ToggleClass();

    public ToggleClass eraserToggle = new ToggleClass();
    public ToggleClass flameThrowerToggle = new ToggleClass();
    public ToggleClass clonerToggle = new ToggleClass();

    public bool clonerActive = false;
    public bool eraserActive = false;
    public bool flameThrowerActive = false;

    public static Camera _cam;
    public static Player _controller;

    private FireworksAutoSpawn silvesterSimulation = new FireworksAutoSpawn();

    public static ToggleClass silvesterSimulationToggle = new ToggleClass();
    public static bool fireworksAutoSpawn = false;

    private float originalFov = 0;

    // TODO: Put the width, the control height and the x / y is variables. Make sure to update the RuntimeTextureGeneration too.

    #endregion

    /*
     * Basicly the Main() function.
     */

    /*
     * Wow, these comments are so helpfull me.
     */
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
        ChacheComponents();

        // Update the version label of the game
        StartCoroutine(VersionLabelCoroutine());

        // Init the Pagesystem.
        AddPages();

        // Add the Teleport Locations
        TeleportDialog.InitLocations();

        // Create GUI Textures on Runtime
        UIStyles.CreateTextures(250, 35);
        UIStyles.CreateStyles();

        originalFov = _cam.fieldOfView;
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
        if (aToggle2.Toggle(true))
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
            silvesterSimulation.UpdateFireworkSpawn();
        }
    }

    #endregion

    /*
     * Adding all the Pages to the PageSystem.
     */
    #region AddPages
    private void AddPages()
    {
        PageSystem.AddPage(MainPage, "main");
        PageSystem.AddPage(ToolsPage, "tools");
        PageSystem.AddPage(ToolsPage, "fireworks");
        PageSystem.AddPage(ExperimentalToolsPage, "experimental_tools");
        PageSystem.AddPage(AboutPage, "about");
        PageSystem.AddPage(ControlsPage, "controls");
        PageSystem.AddPage(HacksPage, "hacks");
        PageSystem.AddPage(MarkersPage, "markers");
        PageSystem.AddPage(SettingsPage, "settings");
    }
    #endregion

    /*
     * Redo all the chaching and overriding when a new Scene is Loaded.
     */
    #region UpdateScene

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        ChacheComponents();
        StartCoroutine(VersionLabelCoroutine());
        UpdateSuperJump(superJumpActive);
        UpdateSuperSpeed(superSpeedActive);
        ResetDisableKeys();
    }
    #endregion

    /*
     * The Update function, this is where most of the stuff is happening.
     */

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
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null)
                Lighter.SpawnFire(hit.collider.gameObject);
        }

        // Hide/Show
        if (Input.GetKeyDown(KeyCode.F1))
            visible = visibleToggle.Toggle(true);

        //Cloner
        if (Input.GetKeyDown(KeyCode.X) && clonerActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null)
                Cloner.Clone(hit.collider, hit.point);
        }
        else if (Input.GetKeyDown(KeyCode.X) && crazyClonerActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            Cloner.CrazyClone(hit.collider, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.T) && newtonifierActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null) Cloner.Newtonify(hit.collider);
        }

        // Delete Tool
        if (Input.GetKeyDown(KeyCode.V) && eraserActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null) Actions.Delete(hit.collider);
        }

        // Auto Clicker
        if (Input.GetKey(KeyCode.R) && autoClickerActive)
        {
            DoAutoclickerClick();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.Toggle(true));
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
            PageSystem.MakeDrawCalls();
            if (disableKeys) UI.Label("Hotkeys are disabled!", "Enable using F2", 20, 40, 16, Color.red);
            if (Actions.TryGetPositionString(out string text, _controller))
                UI.Label(text, "The players position", 20, 16, Color.white);
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
            PageSystem.SelectPage("tools");
        if (UI.Button("Hacks"))
            PageSystem.SelectPage("hacks");
        UI.Space(25);
        if (UI.Button("About"))
            PageSystem.SelectPage("about");
        if (UI.Button("Controls"))
            PageSystem.SelectPage("controls");
        UI.Space(25);
        if (UI.Button("Settings"))
        {
            OpenSettingsPage();
            PageSystem.SelectPage("settings");
        }
        if (UI.BottomNavigationButton("Hide"))
            visible = false;
    }

    //Page 1
    private void ToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 600, 25, 35, 10, 50, 25);
        flameThrowerActive = flameThrowerToggle.Toggle(UI.Button("Flamethrower", flameThrowerActive));
        if (UI.Button("Cloning machine", clonerActive))
        {
            clonerActive = clonerToggle.Toggle(true);
            crazyClonerActive = false;
            crazyClonerToggle.SetState(false);
        }
        eraserActive = eraserToggle.Toggle(UI.Button("Delete Tool", eraserActive));
        UI.Space(20);
        if (UI.Button("Marker stuff"))
            PageSystem.SelectPage("markers");
        if (UI.Button("Buggy Tools"))
            PageSystem.SelectPage("experimental_tools");
        if (UI.Button("Teleporter") && _controller != null)
        {
            TeleportDialog.ResetText();
            TeleportDialog.ShowDialog();
        }
        if (UI.Button("Fireworks Related"))
            PageSystem.SelectPage("fireworks");
        UI.Space(20);
        if (UI.Button("Legacy Physics Gun"))
        {
            Tool.SetSelectedTool(SelectedTool.Hand);
        }
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("main");
    }

    private void FireworksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 500, 25, 35, 10, 50, 25);
        if (UI.Button("Ignite Everything")) Lighter.IgniteAll(true, igniteEverythingDelay);
        if (UI.Button("Instantly Ignite Everything")) Lighter.IgniteAll(false, 1);
        UI.Space(10);
        if (UI.Button("Clear Fireworks")) Actions.ClearFireworks();
        UI.Space(20);
        if (UI.Button("Unlock Tim")) Actions.UnlockTim();
        UI.Space(20);
        fireworksAutoSpawn = silvesterSimulationToggle.Toggle(
            UI.Button("Fireworks Autospawn", fireworksAutoSpawn)
        );
        UI.Space(20);
        if (UI.Button("Fuse all Fireworks - Fast"))
        {
            StartCoroutine(Actions.FuseAll(FuseConnectionType.Fast));
        }
        if (UI.Button("Fuse all Fireworks - Instant"))
        {
            StartCoroutine(Actions.FuseAll(FuseConnectionType.Instant));
        }
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("tools");
    }

    //Page 2
    private void AboutPage()
    {
        UI.Begin("Fireworks Mania Modloader - About", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        UI.Label("Made by jjblock21\nInspired by FMenu.");
        UI.Label("Special thanks also go to Keltusar.");
        UI.Space(10);
        UI.Label("Fireworks Mania ModLoader\n" + Utils.version);
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("main");
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
           "5: Eraser Tool", 65
        );
        UI.Space(10);
        UI.Label("Newtonifier:\nT: Newtonify stuff.");
        UI.Space(10);
        UI.Label("Fly Mode:\n" +
           "G: Toggle Fly Mode.\n" +
           "Space: Fly up\n" +
           "Ctrl: Fly down", 65);
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("main");
    }

    //Page 4
    private void HacksPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 500, 25, 35, 10, 50, 25);
        autoClickerActive = aToggle.Toggle(UI.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.Toggle(UI.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        UI.Space(20);
        superSpeedActive = superSpeedToggle.Toggle(UI.Button("Super Speed", superSpeedActive));
        superJumpActive = superJumpToggle.Toggle(UI.Button("Super Jump", superJumpActive));
        UI.Space(20);
        SpaceMode.ToggleSpaceMode(SpaceMode.spaceModeToggle.Toggle(UI.Button("Space Mode", SpaceMode.spaceModeActive)));
        FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.Toggle(UI.Button("Fly Mode", FlyMode.flyModeActive)));
        UI.Space(20);
        //if (UI.Button("Delete Everything")) Actions.DeleteAll();
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("main");
    }

    // Page 5
    private void ExperimentalToolsPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        UI.Label("Warning: These Tools are Experimental\nand can be buggy.", 30, Utils.CreateColorGUIStyle(Color.yellow));
        newtonifierActive = newtonifierToggle.Toggle(UI.Button("Newtonifier", newtonifierActive));
        if (UI.Button("Crazy Cloner", crazyClonerActive))
        {
            crazyClonerActive = crazyClonerToggle.Toggle(true);
            clonerActive = false;
            clonerToggle.SetState(false);
        }
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("tools");
    }

    // Page 6
    private void MarkersPage()
    {
        UI.Begin("Fireworks Mania Modloader", 10, 20, 300, 450, 25, 35, 10, 50, 25);
        if (UI.Button("Place Marker"))
        {
            var m = Utils.GetUnlitMaterial(Utils.GetRandomColor());
            var p = _controller.transform.position;
            Utils.DrawLine(0.4f, m, new Vector3(p.x, 0, p.z), new Vector3(p.x, 100, p.z));
        }
        if (UI.Button("Clear Markers")) Utils.ClearLines();
        if (UI.BottomNavigationButton("Back"))
            PageSystem.SelectPage("main");
    }

    private int tempSettingsIED = 0;
    private int tempSettingsFSR = 0;

    private void OpenSettingsPage()
    {
        tempSettingsIED = igniteEverythingDelay;
        tempSettingsFSR = silvesterSimulation.Rarity;
    }

    private void UpdateSettingsFromTemp()
    {
        UpdateSuperJump(superJumpActive);
        UpdateSuperSpeed(superSpeedActive);

        igniteEverythingDelay = tempSettingsIED;
        silvesterSimulation.Rarity = tempSettingsFSR;
    }

    // Page 7
    private void SettingsPage()
    {
        try
        {
            UI.Begin("Fireworks Mania Modloader - Settings", 10, 20, 300, 500, 25, 35, 10, 50, 25);

            UI.Label("Super Jump Force", 15);
            jumpHeight = UI.ClampedIntegerInput(jumpHeight, 1, 50);

            UI.Space(10);
            UI.Label("Speed", 15);
            speed = UI.ClampedIntegerInput(speed, 1, 50);

            UI.Space(20);
            UI.Label("Ingite All Delay (ms)", 15);
            tempSettingsIED = UI.ClampedIntegerInput(tempSettingsIED, 1, 1000);

            UI.Space(20);
            UI.Label("Firework Spawn Rarity", 15);
            tempSettingsFSR = UI.ClampedIntegerInput(tempSettingsFSR, 2, 50);

            if (UI.BottomNavigationButton("Apply"))
            {
                UpdateSettingsFromTemp();
                PageSystem.SelectPage("main");
            }
        }
        catch (Exception e)
        {
            UI.Begin("Fireworks Mania Modloader - Settings", 10, 20, 300, 200, 25, 35, 10, 50, 25);
            UI.Label(e.Message + "\n" +
                "An unspecified error has occurred\n" +
                "while drawing the page.");
            if (UI.BottomNavigationButton("Back"))
                PageSystem.SelectPage("main");
        }
    }

    #endregion

    private void ChacheComponents()
    {
        _cam = FindObjectOfType<Camera>();
        _controller = FindObjectOfType<Player>();

        ModSceneManager.FindComponents();
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
        text.text = "v" + Application.version + "\nFMML " + Utils.modVersion;
    }
}
