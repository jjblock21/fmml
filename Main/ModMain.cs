using FireworksMania.Common;
using FireworksMania.UI;
using Main;
using Main.UI;
using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Helpers;
using Main.EnvironmentObserver;
using Main.Miscellaneous;
using FireworksMania.Core.Behaviors;

public partial class ModMain : MonoBehaviour
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

    // Cycle Buttons
    private string weatherButtonLabel = "";
    private SelectionWheel<Weather> weatherSelector = new SelectionWheel<Weather>();

    private string timeButtonLabel = "";
    private SelectionWheel<TimeOfDay> timeSelector = new SelectionWheel<TimeOfDay>();

    #endregion

    #region CycleButtonSetup

    private void SetupCycleButtons()
    {
        if (timeManager.IsEnabled)
        {
            /*weatherButtonLabel = weatherSelector.GetDefaultElementName();
            timeButtonLabel = timeSelector.GetDefaultElementName();*/
            weatherButtonLabel = "Default";
            timeButtonLabel = "Default";
            return;
        }
        weatherButtonLabel = "Nothing";
        timeButtonLabel = "Nothing";
    }

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

        // Create GUI Textures on Runtime
        UIStyles.CreateTextures(250, 35);
        UIStyles.CreateStyles(250);

        // Add the Teleport Locations
        TeleportDialog.InitLocations();

        SetupCycleButtons();

        // Update Time Manager
        timeManager.Setup();

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

    #region UpdateScene
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        FindComponentReferences();
        timeManager.Setup();
        SetupCycleButtons();
        StartCoroutine(VersionLabelCoroutine());
        UpdateSuperJump(superJumpActive);
        UpdateSuperSpeed(superSpeedActive);
        ResetDisableKeys();
        TryFreezeTime();
    }
    #endregion

    #region TimeFreeze

    private void TryFreezeTime()
    {
        if (MapManager.IsPlayableMapLoaded())
        {
            ToolManager.FreezeTimeTool(SelectedTool.Torch);
            timeManager.FreezeTime();
        }
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
            visible = visibleToggle.SwitchUI(true);

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
            FlyMode.ToggleFlyMode(FlyMode.flyModeToggle.SwitchUI(true));
        }

        // Ignite All
        if (Input.GetKeyDown(KeyCode.K)) Lighter.IgniteAll(false, 1);
        if (Input.GetKeyDown(KeyCode.L)) Lighter.IgniteAll(true, igniteEverythingDelay);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ToolManager.SelectTool(SelectedTool.PhysicsTool);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ToolManager.SelectTool(SelectedTool.Torch);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ToolManager.SelectTool(SelectedTool.FuseTool);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ToolManager.SelectTool(SelectedTool.None);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ToolManager.SelectTool(SelectedTool.TimeTool);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            ToolManager.SelectTool(SelectedTool.DeleteTool);

        // Stuff in the update function that will not get skipped when disableKeys is on.
        endOfKeys:

        FlyMode.UpdateFlyMode();
        UpdateSilvesterSimulation();
    }

    #endregion

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