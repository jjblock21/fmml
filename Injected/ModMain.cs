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

/*
 * FMML (Fireworks Mania ModLoader)
 * by jjblock21
 * Idk what to write here :)
 */

public class ModMain : MonoBehaviour
{
    /*
     * Creating instances of the toggle class.
     * TODO: Just make this better, idk.
     */
    #region Toggles
    private ToggleClass fToggle = new ToggleClass();
    private ToggleClass clonerToggle = new ToggleClass();
    private ToggleClass vToggle = new ToggleClass();
    private ToggleClass aToggle = new ToggleClass();
    private ToggleClass aToggle2 = new ToggleClass();
    private ToggleClass aToggle3 = new ToggleClass();
    private ToggleClass crazyClonerToggle = new ToggleClass();
    private ToggleClass eraserToggle = new ToggleClass();
    private ToggleClass newtonifierToggle = new ToggleClass();

    private ToggleClass debugLineToggle = new ToggleClass();

    private int igniteEverythingDelay = 1;
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

    private void DebugLineToggle_StateChanged(object sender, bool e)
    {
        if (e == false) ShapeDrawer.RemovePlane();
    }

    // TODO Replace with GameReflector

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

    private int jumpHeight = 25;
    private int speed = 50;

    #region Update
    public void Update()
    {
        // FlameThrower
        if (Input.GetKey(KeyCode.C) && flameThrowerActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                Actions.SpawnFire(hit.collider.gameObject);
        }

        // Hide/Show
        if (Input.GetKeyDown(KeyCode.F1))
            visible = vToggle.Toggle(true);

        //Cloner
        if (Input.GetKeyDown(KeyCode.X) && clonerActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                Actions.Clone(hit.collider, hit.point);
        }
        else if (Input.GetKeyDown(KeyCode.X) && crazyClonerActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            Actions.CrazyClone(hit.collider, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.T) && newtonifierActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null) Actions.Newtonify(hit.collider);
        }

        // Delete Tool
        if (Input.GetKeyDown(KeyCode.V) && eraserActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null) Actions.Delete(hit.collider);
        }

        // Auto Clicker
        if (Input.GetKey(KeyCode.R) && autoClickerActive)
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

        // Draw Debug Line
        if (flameThrowerActive || clonerActive || eraserActive)
        {
            if (showDebugLine)
            {
                Ray ray1 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(-0.075f, 0, -0.075f));
                Ray ray2 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(0.075f, 0, -0.075f));
                Ray ray3 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(0.075f, 0, 0.075f));
                Ray ray4 = new Ray(_cam.transform.position, _cam.transform.forward + new Vector3(-0.075f, 0, 0.075f));
                Physics.Raycast(ray1, out RaycastHit hit1);
                Physics.Raycast(ray2, out RaycastHit hit2);
                Physics.Raycast(ray3, out RaycastHit hit3);
                Physics.Raycast(ray4, out RaycastHit hit4);
                ShapeDrawer.DrawPlane(hit1.point, hit2.point, hit3.point, hit4.point, Color.red);
            }
        }
        else
        {
            if (showDebugLine) ShapeDrawer.RemovePlane();
        }
    }

    #endregion

    private bool showDebugLine = false;

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
        flameThrowerActive = fToggle.Toggle(UIHelper.Button("Flamethrower", flameThrowerActive));
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
        UIHelper.Begin("Fireworks Mania Modloader - Controls", 10, 10, 300, 600, 25, 35, 10, 50, 10);
        UIHelper.Label("F1: Show/Hide the Menu.");
        UIHelper.Space(10);
        UIHelper.Label("Flamethrower Tool:\nC: Ingite Flammable Material.");
        UIHelper.Space(10);
        UIHelper.Label("Cloning Machine Tool:\nX: Clone object (For both clone tools)");
        UIHelper.Space(10);
        UIHelper.Label("Autoclicker Hack:\nR: Click every second Frame.");
        UIHelper.Space(10);
        UIHelper.Label("Delete Tool:\nV: Delete literally anything!");
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
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    private bool superSpeedActive = false;
    private ToggleClass ssToggle = new ToggleClass();

    private bool superJumpActive = false;
    private ToggleClass sjToggle = new ToggleClass();

    //Page 4
    private void HacksPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        autoClickerActive = aToggle.Toggle(UIHelper.Button("Inbuilt Auto Clicker", autoClickerActive));
        autoClickerButtonLeft = aToggle3.Toggle(UIHelper.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !autoClickerButtonLeft));
        UIHelper.Space(20);
        superSpeedActive = ssToggle.Toggle(UIHelper.Button("Super Speed", superSpeedActive));
        superJumpActive = sjToggle.Toggle(UIHelper.Button("Super Jump", superJumpActive));
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
            Utils.DrawLine(0.4f, m, _controller.transform.position - Vector3.up, _controller.transform.position + Vector3.up * 100);
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

    private IEnumerator UpdateVersionLabel()
    {
        yield return new WaitForSeconds(0.05f);
        var obj = FindObjectOfType<VersionLabel>();
        var obj2 = obj.gameObject.GetComponentInParent<TextMeshProUGUI>();
        obj2.text = "v" + Application.version + " & FMML " + Loader.version;
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