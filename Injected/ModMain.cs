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
    private ToggleClass cToggle = new ToggleClass();
    private ToggleClass vToggle = new ToggleClass();
    private ToggleClass aToggle = new ToggleClass();
    private ToggleClass aToggle2 = new ToggleClass();
    private ToggleClass aToggle3 = new ToggleClass();
    private ToggleClass ccToggle = new ToggleClass();
    private ToggleClass eToggle = new ToggleClass();
    private ToggleClass nToggle = new ToggleClass();
    #endregion

    /*
     * Basicly the Main() function.
     */
    #region Start
    public void Start()
    {
        // Disabled
        //if (SteamChecker.IsAuthorisedGameInstance() == 0)
        //{
        // Add Events
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        ssToggle.StateChanged += SsToggle_StateChanged;
        sjToggle.StateChanged += SjToggle_StateChanged;

        // Chache Components
        ChacheComponents();

        // Update the version label of the game
        StartCoroutine(UpdateVersionLabel());

        // Init the Pagesystem.
        AddPages();

        // Init the SUPER SONIC AUTOCKLICKER!
        Mouse.InitSuperSonicAutoClicker();

        // Authorise game and return
        authorised = true;
        return;
        //}
    }

    private void UpdateSuperJump(bool e)
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        if (firstPerson == null) return;
        FieldInfo field = firstPerson.GetType().GetField("m_JumpSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (e) field.SetValue(firstPerson, jumpHeight);
        else field.SetValue(firstPerson, 10);
    }

    private void UpdateSuperSpeed(bool e)
    {
        FirstPersonController firstPerson = FindObjectOfType<FirstPersonController>();
        if (firstPerson == null) return;
        FieldInfo field = firstPerson.GetType().GetField("m_RunSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (e) field.SetValue(firstPerson, 50);
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

    private int prevDebugLine = -1;

    /*
     * The Update function, this is where most of the stuff is happening.
     */

    private int jumpHeight = 25;

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
        else if (Input.GetKeyDown(KeyCode.X) && ccActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                Actions.CrazyClone(hit.collider, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.T) && nActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null) Actions.Newtonify(hit.collider);
        }

        // Delete Tool
        if (Input.GetKeyDown(KeyCode.V) && eActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null) Actions.Delete(hit.collider);
        }

        // Auto Clicker
        if (Input.GetKey(KeyCode.R) && acActive)
        {
            if (aToggle2.Toggle(true))
            {
                if (!acButtonLeft) Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
                else Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
            }
            else
            {
                if (!acButtonLeft) Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                else Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) Actions.SpawnTim(_cam, this);

        // Ignite All
        if (Input.GetKeyDown(KeyCode.K)) Actions.IgniteAll(false);

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
        if (flameThrowerActive || clonerActive || eActive)
        {
            prevDebugLine = Utils.TryDrawDebugLine(_cam.transform.position - new Vector3(0, 0.25f, 0),
                _cam.transform.forward, Color.red, prevDebugLine, _cam.transform.position);
        }
        else
        {
            if (prevDebugLine == -1) return;
            Utils.RemoveLine(prevDebugLine);
            prevDebugLine = -1;
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
            clonerActive = cToggle.Toggle(true);
            ccActive = false;
            ccToggle.SetState(false);
        }
        eActive = eToggle.Toggle(UIHelper.Button("Delete Tool", eActive));
        UIHelper.Space(20);
        if (UIHelper.Button("Marker stuff"))
            PageSystem.SelectPage(6);
        if (UIHelper.Button("Buggy Tools"))
            PageSystem.SelectPage(5);
        UIHelper.Space(30);
        if (UIHelper.Button("Ignite Everything")) Actions.IgniteAll(true);
        if (UIHelper.Button("Instantly Ignite Everything")) Actions.IgniteAll(false);
        UIHelper.Space(20);
        //if (UIHelper.DisabledButton("Spawn Tim")) Actions.SpawnTim(_cam, this);
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
        UIHelper.Label("Ignite Everything:\nK: Ignite everything.");
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
        acActive = aToggle.Toggle(UIHelper.Button("Inbuilt Auto Clicker", acActive));
        acButtonLeft = aToggle3.Toggle(UIHelper.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !acButtonLeft));
        UIHelper.Space(20);
        superSpeedActive = ssToggle.Toggle(UIHelper.Button("Super Speed", superSpeedActive));
        superJumpActive = sjToggle.Toggle(UIHelper.Button("Super Jump", superJumpActive));
        UIHelper.Space(20);
        if (UIHelper.Button("Teleporter") && _controller != null)
        {
            TpDialog.ResetText();
            TpDialog.ShowDialog();
        }
        UIHelper.Space(20);
        if (UIHelper.Button("Use the Infinity Gauntlet")) Actions.DeleteAll();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    // Page 5
    private void ExperimentalToolsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        UIHelper.Label("Warning: These Tools are Experimental\nand can be buggy.", 30, Utils.CreateColorGUIStyle(Color.yellow));
        nActive = nToggle.Toggle(UIHelper.Button("Newtonifier", nActive));
        if (UIHelper.Button("Crazy Cloner", ccActive))
        {
            ccActive = ccToggle.Toggle(true);
            clonerActive = false;
            cToggle.SetState(false);
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
            Utils.DrawLine(0.5f, m, _controller.transform.position, _controller.transform.position + Vector3.up * 100);
        }
        if (UIHelper.Button("Clear Markers")) Utils.ClearLines();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(1);
    }

    // Page 7
    private void SettingsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader - Settings", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        UIHelper.Label("Super Jump Force", 25);
        jumpHeight = int.Parse(UIHelper.Input(jumpHeight.ToString(), 2));
        if (UIHelper.BottomNavigationButton("Back"))
        {
            // Update Affected Modules
            UpdateSuperJump(superJumpActive);
            PageSystem.SelectPage(0);
        }
    }

    #endregion

    private void ChacheComponents()
    {
        _cam = FindObjectOfType<Camera>();
        _toolAnimation = FindObjectOfType<ToolsBobbing>();
        _controller = FindObjectOfType<Player>();
        _torch = FindObjectOfType<TorchTool>();
        TpDialog.Init(_controller);
    }

    public Camera _cam;
    public ToolsBobbing _toolAnimation;
    public Player _controller;
    public TorchTool _torch;

    private bool flameThrowerActive = false;
    private bool clonerActive = false;
    private bool visible = true;
    private bool acActive = false;
    private bool acButtonLeft = true;
    private bool ccActive = false;
    private bool eActive = false;
    private bool nActive = false;
    private bool authorised = false;

    private IEnumerator UpdateVersionLabel()
    {
        yield return new WaitForSeconds(0.05f);
        var obj = FindObjectOfType<VersionLabel>();
        var obj2 = obj.gameObject.GetComponentInParent<TextMeshProUGUI>();
        obj2.text = "v" + Application.version + " (MODDED)";
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