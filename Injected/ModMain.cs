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
    private ToggleClass flameThrowerToggle = new ToggleClass();
    private ToggleClass clonerToggle = new ToggleClass();
    private ToggleClass visibleToggle = new ToggleClass();
    private ToggleClass aToggle = new ToggleClass();
    private ToggleClass aToggle2 = new ToggleClass();
    private ToggleClass aToggle3 = new ToggleClass();
    private ToggleClass crazyClonerToggle = new ToggleClass();
    private ToggleClass eraserToggle = new ToggleClass();
    private ToggleClass newtonifierToggle = new ToggleClass();
    private ToggleClass spaceModeToggle = new ToggleClass();

    private ToggleClass debugLineToggle = new ToggleClass();

    private int igniteEverythingDelay = 1;
    private int jumpHeight = 25;
    private int speed = 50;

    private bool disableKeys = false;
    private bool isMenuOpen = false;


    private bool spaceModeActive = false;
    private bool showDebugLine = false;

    private bool superSpeedActive = false;
    private ToggleClass superSpeedToggle = new ToggleClass();
    private bool superJumpActive = false;
    private ToggleClass superJumpToggle = new ToggleClass();

    public Camera _cam;
    public Player _controller;

    private bool flameThrowerActive = false;
    private bool clonerActive = false;
    private bool visible = true;
    private bool autoClickerActive = false;
    private bool autoClickerButtonLeft = true;
    private bool crazyClonerActive = false;
    private bool eraserActive = false;
    private bool newtonifierActive = false;
    private bool authorised = false;

    private SilvesterSimulation silvesterSimulation = new SilvesterSimulation();
    private ToggleClass silvesterSimulationToggle = new ToggleClass();
    private bool silvesterSimulationActive = false;

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
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        superSpeedToggle.StateChanged += SsToggle_StateChanged;
        superJumpToggle.StateChanged += SjToggle_StateChanged;
        debugLineToggle.SetState(true);
        debugLineToggle.StateChanged += DebugLineToggle_StateChanged;

        // Chache Components
        ChacheComponents();

        // Update the version label of the game
        StartCoroutine(UpdateVersionLabel());

        // Init the Pagesystem.
        AddPages();

        // Add the Teleport Locations
        TeleportDialog.InitLocations();

        authorised = true;
    }
    #endregion

    private void DebugLineToggle_StateChanged(object sender, bool e)
    {
        if (e == false) ShapeDrawer.RemovePlane();
    }

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

    private void SpaceMode(bool enabled)
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        if (firstPerson == null) return;
        GameReflector gr = new GameReflector(firstPerson);
        FieldInfo field = gr.GetField("m_JumpSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (enabled)
        {
            Physics.gravity = Vector3.zero;
            field.SetValue(firstPerson, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
            field.SetValue(firstPerson, 10);
        }
    }

    private void ToggleSpaceMode(bool input)
    {
        if (input && !spaceModeActive)
        {
            spaceModeActive = true;
            SpaceMode(true);
        }
        else if (!input && spaceModeActive)
        {
            spaceModeActive = false;
            SpaceMode(false);
        }
    }

    private void DoAutoclickerClick()
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

    #region FlyMode

    private bool flyModeActive = false;
    private FirstPersonController flyModeControler = null;
    private ToggleClass flyModeToggle = new ToggleClass();

    private void EnableFlyMode()
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        SetPlayerGravity(0, firstPerson);
        SetSpeed(20, firstPerson);
        flyModeControler = firstPerson;
        flyModeActive = true;
    }

    private void DisableFlyMode()
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        SetPlayerGravity(2, firstPerson);
        SetSpeed(10, firstPerson);
        flyModeControler = firstPerson;
        flyModeActive = false;
    }

    private void SetPlayerGravity(int gravity, FirstPersonController firstPerson)
    {
        if (firstPerson == null) return;
        GameReflector gr = new GameReflector(firstPerson);
        FieldInfo field = gr.GetField("m_GravityMultiplier", BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(firstPerson, gravity);
    }

    private void Jump(int force)
    {
        if (flyModeControler == null) return;
        GameReflector gr = new GameReflector(flyModeControler);
        Vector3 dir = (Vector3)gr.GetFieldValue("m_MoveDir");
        dir.y = force;
        gr.SetFieldValue("m_MoveDir", dir);
    }

    private void SetSpeed(int speed, FirstPersonController firstPerson)
    {
        if (firstPerson == null) return;
        GameReflector gr = new GameReflector(firstPerson);
        gr.SetFieldValue("m_RunSpeed", speed);
    }

    private void UpdateFlyMode()
    {
        if (flyModeActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(8);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                flyModeControler.ResetMovement(flyModeControler.gameObject.transform.position);
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Jump(-8);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                flyModeControler.ResetMovement(flyModeControler.gameObject.transform.position);
            }
        }
    }

    private void ToggleFlyMode(bool input)
    {
        if (input && !flyModeActive) EnableFlyMode();
        else if (!input && flyModeActive) DisableFlyMode();
    }

    #endregion

    /*
     * Adding all the Pages to the PageSystem.
     */
    #region AddPages
    private void AddPages()
    {
        PageSystem.AddPage(MainPage);
        PageSystem.AddPage(ToolsPage);
        PageSystem.AddPage(AboutPage);
        PageSystem.AddPage(ControllsPage);
        PageSystem.AddPage(HacksPage);
        PageSystem.AddPage(ExperimentalToolsPage);
        PageSystem.AddPage(MarkersPage);
        PageSystem.AddPage(SettingsPage);
    }
    #endregion

    /*
     * Redo all the chaching and overriding when a new Scene is Loaded.
     */
    #region UpdateScene
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        ChacheComponents();
        StartCoroutine(UpdateVersionLabel());
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
        isMenuOpen = false;
    }

    #region Update
    public void Update()
    {
        if (silvesterSimulationActive) silvesterSimulation.UpdateFireworkSpawn();

        // Auto Dissable Key
        if (Input.GetKeyDown(KeyCode.Tab) && ModSceneManager.IsPlayableMapLoaded())
        {
            if (isMenuOpen)
            {
                isMenuOpen = false;
                disableKeys = false;
            }
            else
            {
                isMenuOpen = true;
                disableKeys = true;
            }
        }

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
            ToggleFlyMode(flyModeToggle.Toggle(true));
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

        UpdateFlyMode();

        // Draw Debug Line
        if (flameThrowerActive || clonerActive || eraserActive)
        {
            if (showDebugLine)
            {

            }
        }
        else
        {
            if (showDebugLine) ShapeDrawer.RemovePlane();
        }
    }

    #endregion

    /*
     * Where all the GUI stuff gets Updated.
     */
    #region OnGUI
    public void OnGUI()
    {
        if (authorised)
        {
            if (visible)
            {
                PageSystem.DrawPage();
                if (disableKeys) ModUI.Label("Keys are disabled!", "Enable using F2", 20, 40, 16, Color.red);
                if (Actions.TryGetPositionString(out string text, _controller))
                    ModUI.Label(text, "The players position", 20, 16, Color.white);
            }
            return;
        }
        Unauthorised();
    }
    #endregion

    #region Pages

    //Page 0
    private void MainPage()
    {
        ModUI.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 25);
        if (ModUI.Button("Tools"))
            PageSystem.SelectPage(1);
        if (ModUI.Button("Hacks"))
            PageSystem.SelectPage(4);
        ModUI.Space(25);
        if (ModUI.Button("About"))
            PageSystem.SelectPage(2);
        if (ModUI.Button("Controls"))
            PageSystem.SelectPage(3);
        ModUI.Space(25);
        if (ModUI.Button("Settings"))
        {
            OpenSettingsPage();
            PageSystem.SelectPage(7);
        }
        if (ModUI.BottomNavigationButton("Hide"))
            visible = false;
    }

    //Page 1
    private void ToolsPage()
    {
        ModUI.Begin("Fireworks Mania Modloader", 10, 10, 300, 700, 25, 35, 10, 50, 25);
        flameThrowerActive = flameThrowerToggle.Toggle(ModUI.Button("Flamethrower", flameThrowerActive));
        if (ModUI.Button("Cloning machine", clonerActive))
        {
            clonerActive = clonerToggle.Toggle(true);
            crazyClonerActive = false;
            crazyClonerToggle.SetState(false);
        }
        eraserActive = eraserToggle.Toggle(ModUI.Button("Delete Tool", eraserActive));
        ModUI.Space(20);
        if (ModUI.Button("Marker stuff"))
            PageSystem.SelectPage(6);
        if (ModUI.Button("Buggy Tools"))
            PageSystem.SelectPage(5);
        ModUI.Space(20);
        if (ModUI.Button("Teleporter") && _controller != null)
        {
            TeleportDialog.ResetText();
            TeleportDialog.ShowDialog();
        }
        ModUI.Space(20);
        if (ModUI.Button("Ignite Everything")) Lighter.IgniteAll(true, igniteEverythingDelay);
        if (ModUI.Button("Instantly Ignite Everything")) Lighter.IgniteAll(false, 1);
        ModUI.Space(20);
        if (ModUI.Button("Unlock Tim")) Actions.UnlockTim();
        ModUI.Space(20);
        silvesterSimulationActive = silvesterSimulationToggle.Toggle(
            ModUI.Button("Silvester Simulation", silvesterSimulationActive)
        );
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 2
    private void AboutPage()
    {
        ModUI.Begin("Fireworks Mania Modloader - About", 10, 10, 300, 450, 25, 35, 10, 50, 25);
        ModUI.Label("Made by jjblock21\nInspired by FMenu.");
        ModUI.Label("Special thanks also go to Keltusar.");
        ModUI.Space(10);
        ModUI.Label("Fireworks Mania ModLoader " + Utils.version);
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 3
    private void ControllsPage()
    {
        ModUI.Begin("Fireworks Mania Modloader - Controls", 10, 10, 300, 700, 25, 35, 10, 50, 25);
        ModUI.Label("F1: Show/Hide the Menu.");
        ModUI.Label("F2: Toggle keys, so you're be able to type.");
        ModUI.Space(10);
        ModUI.Label("Flamethrower Tool:\nC: Ingite Flammable Material.");
        ModUI.Space(10);
        ModUI.Label("Cloning Machine Tool:\nX: Clone object (For both clone tools)");
        ModUI.Space(10);
        ModUI.Label("Autoclicker Hack:\nR: Click every second Frame.");
        ModUI.Space(10);
        ModUI.Label("Delete Tool:\nV: Delete what you're looking at!");
        ModUI.Space(10);
        ModUI.Label("Ignite Everything:\nK: Ignite everything.\nL: Ignite Everything with specified delay.", 50);
        ModUI.Space(10);
        ModUI.Label("Fast navigation:\n" +
            "1: Physics Tool, 2: Lighter,\n" +
            "3: Fuse Tool, 4: Clear view\n" +
            "5: Eraser Tool", 65
        );
        ModUI.Space(10);
        ModUI.Label("Newtonifier:\nT: Newtonify stuff.");
        ModUI.Space(10);
        ModUI.Label("Fly Mode:\n" +
            "G: Toggle Fly Mode.\n" +
            "Space: Fly up\n" +
            "Ctrl: Fly down", 65);
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 4
    private void HacksPage()
    {
        ModUI.Begin("Fireworks Mania Modloader", 10, 10, 300, 500, 25, 35, 10, 50, 25);
        autoClickerActive = aToggle.Toggle(ModUI.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.Toggle(ModUI.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        ModUI.Space(20);
        superSpeedActive = superSpeedToggle.Toggle(ModUI.Button("Super Speed", superSpeedActive));
        superJumpActive = superJumpToggle.Toggle(ModUI.Button("Super Jump", superJumpActive));
        ModUI.Space(20);
        ToggleSpaceMode(spaceModeToggle.Toggle(ModUI.Button("Space Mode", spaceModeActive)));
        ToggleFlyMode(flyModeToggle.Toggle(ModUI.Button("Fly Mode", flyModeActive)));
        ModUI.Space(20);
        if (ModUI.Button("Delete Everything")) Actions.DeleteAll();
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    // Page 5
    private void ExperimentalToolsPage()
    {
        ModUI.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 25);
        ModUI.Label("Warning: These Tools are Experimental\nand can be buggy.", 30, Utils.CreateColorGUIStyle(Color.yellow));
        newtonifierActive = newtonifierToggle.Toggle(ModUI.Button("Newtonifier", newtonifierActive));
        if (ModUI.Button("Crazy Cloner", crazyClonerActive))
        {
            crazyClonerActive = crazyClonerToggle.Toggle(true);
            clonerActive = false;
            clonerToggle.SetState(false);
        }
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(1);
    }

    // Page 6
    private void MarkersPage()
    {
        ModUI.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 25);
        if (ModUI.Button("Place Marker"))
        {
            var m = Utils.GetUnlitMaterial(Utils.GetRandomColor());
            var p = _controller.transform.position;
            Utils.DrawLine(0.4f, m, new Vector3(p.x, 0, p.z), new Vector3(p.x, 100, p.z));
        }
        if (ModUI.Button("Clear Markers")) Utils.ClearLines();
        if (ModUI.BottomNavigationButton("Back"))
            PageSystem.SelectPage(1);
    }

    private int tempSettingsIED = 0;
    private int tempSettingsFSR = 0;

    private void OpenSettingsPage()
    {
        tempSettingsIED = igniteEverythingDelay;
        tempSettingsFSR = silvesterSimulation.Rarity;
    }

    // Page 7
    private void SettingsPage()
    {
        try
        {
            ModUI.Begin("Fireworks Mania Modloader - Settings", 10, 10, 300, 450, 25, 35, 10, 50, 25);

            ModUI.Label("Super Jump Force", 20);
            jumpHeight = int.Parse(ModUI.Input(jumpHeight.ToString(), 2));
            ModUI.Label("Speed", 20);
            speed = int.Parse(ModUI.Input(speed.ToString(), 2));

            /*ModUI.Space(10);
            showDebugLine = debugLineToggle.Toggle(ModUI.Button("Red Laser Thing", showDebugLine));*/

            ModUI.Space(10);
            ModUI.Label("Ingite All Delay (ms): " + tempSettingsIED, 20);
            tempSettingsIED = (int)ModUI.Slider(tempSettingsIED, 1, 1000);

            ModUI.Space(10);
            ModUI.Label("Firework Spawn Rarity: " + tempSettingsFSR, 20);
            tempSettingsFSR = (int)ModUI.Slider(tempSettingsFSR, 2, 50);

            if (ModUI.BottomNavigationButton("Apply"))
            {
                // Update Affected Modules
                UpdateSuperJump(superJumpActive);
                UpdateSuperSpeed(superSpeedActive);

                igniteEverythingDelay = tempSettingsIED;
                silvesterSimulation.Rarity = tempSettingsFSR;

                PageSystem.SelectPage(0);
            }
        }
        catch
        {

        }
    }

    #endregion

    private void ChacheComponents()
    {
        _cam = FindObjectOfType<Camera>();
        _controller = FindObjectOfType<Player>();
        ModSceneManager.ChacheComponents();
        TeleportDialog.ChacheComponents();
        FireworkSpawner.ChacheComponents();
    }

    private IEnumerator UpdateVersionLabel()
    {
        yield return new WaitForSeconds(0.05f);
        var obj = FindObjectOfType<VersionLabel>();
        var obj2 = obj.gameObject.GetComponentInParent<TextMeshProUGUI>();
        obj2.transform.position = new Vector3(
            obj2.transform.position.x,
            obj2.transform.position.y - 10,
            obj2.transform.position.z);
        obj2.text = "v" + Application.version + "\nFMML " + Utils.modVersion;
    }

    #region AuthorisationStuff
    private void DisableAll()
    {
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "Loader") continue;
            obj.SetActive(false);
        }
    }

    private void Unauthorised()
    {
        DisableAll();
        ModUI.Begin("Unauthorised Game", Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 400, 25, 35, 10, 50, 10);
        ModUI.Label("Failed to authorise this instance of the game,\nplease make sure you are connected to the\ninternet.",
            30, Utils.CreateColorGUIStyle(Color.red, 26));
        if (ModUI.BottomNavigationButton("Unload Mod and close Game"))
        {
            Loader.Disable();
            Application.Quit();
        }
    }
    #endregion
}