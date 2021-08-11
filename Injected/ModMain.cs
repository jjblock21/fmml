﻿using FireworksMania.Common;
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

/*
 * FMML (Fireworks Mania ModLoader)
 * by jjblock21
 * Idk what to write here :)
 */

[AttachToGame(AttachMode.ModObject)]
public class ModMain : MonoBehaviour
{
    /*
     * Creating instances of the toggle class.
     * TODO: Just make this better, idk.
     */
    #region Toggles
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
    private bool spaceModeActive = false;
    private bool showDebugLine = false;

    private bool superSpeedActive = false;
    private ToggleClass ssToggle = new ToggleClass();
    private bool superJumpActive = false;
    private ToggleClass sjToggle = new ToggleClass();

    public Camera _cam;
    public ToolsBobbing _toolAnimation;
    public Player _controller;
    public TorchTool _torch;

    private bool flameThrowerActive = false;
    private bool clonerActive = false;
    private bool visible = true;
    private bool autoClickerActive = false;
    private bool autoClickerButtonLeft = true;
    private bool crazyClonerActive = false;
    private bool eraserActive = false;
    private bool newtonifierActive = false;
    private bool authorised = false;

    #endregion

    /*
     * Basicly the Main() function.
     */
    #region Start
    public void Start()
    {
        // Add Events
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        ssToggle.StateChanged += SsToggle_StateChanged;
        sjToggle.StateChanged += SjToggle_StateChanged;
        debugLineToggle.SetState(true);
        debugLineToggle.StateChanged += DebugLineToggle_StateChanged;

        // Chache Components
        ChacheComponents();

        // Update the version label of the game
        StartCoroutine(UpdateVersionLabel());

        // Init the Pagesystem.
        AddPages();

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
        SetPlayerGravity(1, firstPerson);
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
    }
    #endregion

    /*
     * The Update function, this is where most of the stuff is happening.
     */

    #region Update
    public void Update()
    {
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
                Actions.SpawnFire(hit.collider.gameObject);
        }

        // Hide/Show
        if (Input.GetKeyDown(KeyCode.F1))
            visible = visibleToggle.Toggle(true);

        //Cloner
        if (Input.GetKeyDown(KeyCode.X) && clonerActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null)
                Actions.Clone(hit.collider, hit.point);
        }
        else if (Input.GetKeyDown(KeyCode.X) && crazyClonerActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            Actions.CrazyClone(hit.collider, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.T) && newtonifierActive)
        {
            RaycastHit hit = Utils.DoScreenRaycast(_cam);
            if (hit.collider != null) Actions.Newtonify(hit.collider);
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
        if (Input.GetKeyDown(KeyCode.K)) Actions.IgniteAll(false, 1);
        if (Input.GetKeyDown(KeyCode.L)) Actions.IgniteAll(true, igniteEverythingDelay);

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
                /*
                Ray ray1 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(-0.075f, 0, -0.075f));
                Ray ray2 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(0.075f, 0, -0.075f));
                Ray ray3 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(0.075f, 0, 0.075f));
                Ray ray4 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(-0.075f, 0, 0.075f));
                Physics.Raycast(ray1, out RaycastHit hit1);
                Physics.Raycast(ray2, out RaycastHit hit2);
                Physics.Raycast(ray3, out RaycastHit hit3);
                Physics.Raycast(ray4, out RaycastHit hit4);
                ShapeDrawer.DrawPlane(hit1.point, hit2.point, hit3.point, hit4.point, Color.red);
                */
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
                // Update Page
                PageSystem.DrawPage();

                if (disableKeys) UIHelper.Label("Keys are disabled!", "Enable using F2", 20, 40, 16, Color.red);

                // Update Position Display
                if (Actions.TryGetPositionString(out string text, _controller))
                    UIHelper.Label(text, "The players position", 20, 16, Color.white);
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
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        if (UIHelper.Button("Tools"))
            PageSystem.SelectPage(1);
        if (UIHelper.Button("Hacks"))
            PageSystem.SelectPage(4);
        UIHelper.Space(25);
        if (UIHelper.Button("About"))
            PageSystem.SelectPage(2);
        if (UIHelper.Button("Controls"))
            PageSystem.SelectPage(3);
        UIHelper.Space(25);
        if (UIHelper.Button("Settings"))
            PageSystem.SelectPage(7);
        if (UIHelper.BottomNavigationButton("Hide"))
            visible = false;
    }

    //Page 1
    private void ToolsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 600, 25, 35, 10, 50, 10);
        flameThrowerActive = flameThrowerToggle.Toggle(UIHelper.Button("Flamethrower", flameThrowerActive));
        if (UIHelper.Button("Cloning machine", clonerActive))
        {
            clonerActive = clonerToggle.Toggle(true);
            crazyClonerActive = false;
            crazyClonerToggle.SetState(false);
        }
        eraserActive = eraserToggle.Toggle(UIHelper.Button("Delete Tool", eraserActive));
        UIHelper.Space(20);
        if (UIHelper.Button("Marker stuff"))
            PageSystem.SelectPage(6);
        if (UIHelper.Button("Buggy Tools"))
            PageSystem.SelectPage(5);
        UIHelper.Space(30);
        if (UIHelper.Button("Ignite Everything")) Actions.IgniteAll(true, igniteEverythingDelay);
        if (UIHelper.Button("Instantly Ignite Everything")) Actions.IgniteAll(false, 1);
        UIHelper.Space(20);
        if (UIHelper.Button("Unlock Tim")) Actions.UnlockTim();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 2
    private void AboutPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader - About", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        UIHelper.Label("Made by jjblock21\nInspired by FMenu.");
        UIHelper.Space(20);
        UIHelper.Label("Fireworks Mania ModLoader v" + Utils.version);
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 3
    private void ControllsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader - Controls", 10, 10, 300, 700, 25, 35, 10, 50, 10);
        UIHelper.Label("F1: Show/Hide the Menu.");
        UIHelper.Label("F2: Toggle keys, so you're be able to type.");
        UIHelper.Space(10);
        UIHelper.Label("Flamethrower Tool:\nC: Ingite Flammable Material.");
        UIHelper.Space(10);
        UIHelper.Label("Cloning Machine Tool:\nX: Clone object (For both clone tools)");
        UIHelper.Space(10);
        UIHelper.Label("Autoclicker Hack:\nR: Click every second Frame.");
        UIHelper.Space(10);
        UIHelper.Label("Delete Tool:\nV: Delete anything you're looking at!");
        UIHelper.Space(10);
        UIHelper.Label("Ignite Everything:\nK: Ignite everything.\nL: Ignite Everything with specified delay.", 50);
        UIHelper.Space(10);
        UIHelper.Label("Fast navigation:\n" +
            "1: Physics Tool, 2: Lighter,\n" +
            "3: Fuse Tool, 4: Clear view\n" +
            "5: Eraser Tool", 50
        );
        UIHelper.Space(10);
        UIHelper.Label("Newtonifier:\nT: Newtonify stuff.");
        UIHelper.Space(10);
        UIHelper.Label("Fly Mode:\n" +
            "G: Toggle Fly Mode.\n" +
            "Space: Fly up\n" +
            "Ctrl: Fly down", 50);
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 4
    private void HacksPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 500, 25, 35, 10, 50, 10);
        autoClickerActive = aToggle.Toggle(UIHelper.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.Toggle(UIHelper.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        UIHelper.Space(20);
        superSpeedActive = ssToggle.Toggle(UIHelper.Button("Super Speed", superSpeedActive));
        superJumpActive = sjToggle.Toggle(UIHelper.Button("Super Jump", superJumpActive));
        UIHelper.Space(20);
        ToggleSpaceMode(spaceModeToggle.Toggle(UIHelper.Button("Space Mode", spaceModeActive)));
        ToggleFlyMode(flyModeToggle.Toggle(UIHelper.Button("Fly Mode", flyModeActive)));
        UIHelper.Space(20);
        if (UIHelper.Button("Delete Everything")) Actions.DeleteAll();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    // Page 5
    private void ExperimentalToolsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        UIHelper.Label("Warning: These Tools are Experimental\nand can be buggy.", 30, Utils.CreateColorGUIStyle(Color.yellow));
        newtonifierActive = newtonifierToggle.Toggle(UIHelper.Button("Newtonifier", newtonifierActive));
        if (UIHelper.Button("Crazy Cloner", crazyClonerActive))
        {
            crazyClonerActive = crazyClonerToggle.Toggle(true);
            clonerActive = false;
            clonerToggle.SetState(false);
        }
        UIHelper.Space(20);
        if (UIHelper.Button("Teleporter") && _controller != null)
        {
            TpDialog.ResetText();
            TpDialog.ShowDialog();
        }
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(1);
    }

    // Page 6
    private void MarkersPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        if (UIHelper.Button("Place Marker"))
        {
            var m = Utils.GetUnlitMaterial(Utils.GetRandomColor());
            var p = _controller.transform.position;
            Utils.DrawLine(0.4f, m, new Vector3(p.x, 0, p.z), new Vector3(p.x, 100, p.z));
        }
        if (UIHelper.Button("Clear Markers")) Utils.ClearLines();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(1);
    }

    // Page 7
    private void SettingsPage()
    {
        try
        {
            UIHelper.Begin("Fireworks Mania Modloader - Settings", 10, 10, 300, 450, 25, 35, 10, 50, 10);

            UIHelper.Label("Super Jump Force", 25);
            jumpHeight = int.Parse(UIHelper.Input(jumpHeight.ToString(), 2));
            UIHelper.Label("Speed", 25);
            speed = int.Parse(UIHelper.Input(speed.ToString(), 2));

            UIHelper.Space(10);
            showDebugLine = debugLineToggle.Toggle(UIHelper.Button("Red Laser Thing", showDebugLine));

            UIHelper.Space(10);
            UIHelper.Label("Ingite All Delay (ms)", 25);
            igniteEverythingDelay = int.Parse(UIHelper.Input(igniteEverythingDelay.ToString(), 5));

            if (UIHelper.BottomNavigationButton("Back"))
            {
                // Update Affected Modules
                UpdateSuperJump(superJumpActive);
                UpdateSuperSpeed(superSpeedActive);

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
        _toolAnimation = FindObjectOfType<ToolsBobbing>();
        _controller = FindObjectOfType<Player>();
        _torch = FindObjectOfType<TorchTool>();
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
        UIHelper.Begin("Unauthorised Game", Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 400, 25, 35, 10, 50, 10);
        UIHelper.Label("Failed to authorise this instance of the game,\nplease make sure you are connected to the\ninternet.",
            30, Utils.CreateColorGUIStyle(Color.red, 26));
        if (UIHelper.BottomNavigationButton("Unload Mod and close Game"))
        {
            Loader.Disable();
            Application.Quit();
        }
    }
    #endregion
}