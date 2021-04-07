using Injected;
using Injected.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * FMML (Fireowrks Mania ModLoader)
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
    #endregion

    /*
     * Basicly the Main() function.
     */
    #region Start
    public void Start()
    {
        // Add Events
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        // Chache Components
        ChacheComponents();

        // Update the version label of the game
        StartCoroutine(UpdateVersionLabel());

        // Init the Pagesystem.
        AddPages();

        // Init the SUPER SONIC AUTOCKLICKER!
        Mouse.InitSuperSonicAutoClicker();
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
    }
    #endregion

    /*
     * The Update function, this is where most of the stuff is happening.
     */
    #region Update
    public void Update()
    {
        // FlameThrower
        if (Input.GetKey(KeyCode.C) && flameThrowerActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                SpawnFire(hit.collider.gameObject);
        }

        // Hide/Show
        if (Input.GetKeyDown(KeyCode.F1))
            visible = vToggle.Toggle(true);

        //C loner
        if (Input.GetKeyDown(KeyCode.X) && clonerActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                Clone(hit.collider, hit.point);
        }
        else if (Input.GetKeyDown(KeyCode.X) && ccActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null)
                CrazyClone(hit.collider, hit.point);
        }

        // Delete Tool
        if (Input.GetKeyDown(KeyCode.V) && eActive)
        {
            RaycastHit hit = Utils.DoRaycastThroughScreenPoint(_cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider != null) Delete(hit.collider);
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

        // Ignite All
        if (Input.GetKeyDown(KeyCode.K)) IgniteAll();

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
            // Update Page
            PageSystem.DrawPage();

            // Update Position Display
            if (TryGetPositionString(out string text))
                UIHelper.Label(text, "The players position", 20, 16, Color.white);
        }
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
        UIHelper.Space();
        if (UIHelper.Button("About"))
            PageSystem.SelectPage(2);
        if (UIHelper.Button("Controls"))
            PageSystem.SelectPage(3);
        if (UIHelper.BottomNavigationButton("Hide"))
            visible = false;
    }

    //Page 1
    private void ToolsPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        flameThrowerActive = fToggle.Toggle(UIHelper.Button("Flamethrower", flameThrowerActive));
        if (UIHelper.Button("Cloning machine", clonerActive))
        {
            clonerActive = cToggle.Toggle(true);
            ccActive = false;
            ccToggle.SetState(false);
        }
        if (UIHelper.Button("Crazy Cloner", ccActive))
        {
            ccActive = ccToggle.Toggle(true);
            clonerActive = false;
            cToggle.SetState(false);
        }
        eActive = eToggle.Toggle(UIHelper.Button("Delete Tool", eActive));
        UIHelper.Space(20);
        if (UIHelper.Button("Ignite Everything")) IgniteAll();
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
        UIHelper.Begin("Fireworks Mania Modloader - Controls", 10, 10, 300, 500, 25, 35, 10, 50, 10);
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
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
    }

    //Page 4
    private void HacksPage()
    {
        UIHelper.Begin("Fireworks Mania Modloader", 10, 10, 300, 450, 25, 35, 10, 50, 10);
        acActive = aToggle.Toggle(UIHelper.Button("Inbuilt Auto Clicker", acActive));
        acButtonLeft = aToggle3.Toggle(UIHelper.Button("AC Mouse Button: LMB", "AC Mouse Button: RMB", !acButtonLeft));
        UIHelper.Space(20);
        if (UIHelper.Button("Teleporter") && _controller != null)
        {
            TpDialog.ResetText();
            TpDialog.ShowDialog();
        }
        UIHelper.Space(20);
        if (UIHelper.Button("Use the Infinity Gauntlet")) DeleteAll();
        if (UIHelper.BottomNavigationButton("Back"))
            PageSystem.SelectPage(0);
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

    #region Features

    private void IgniteAll()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<IIgniteable>() != null)
                obj.GetComponent<IIgniteable>().Ignite(2500);
        }
    }

    private void SpawnFire(GameObject obj)
    {
        IFlammable flammeable = obj.GetComponent<IFlammable>();
        if (flammeable != null)
            flammeable.ApplyFireForce(2500f);
    }

    private void Clone(Collider collider, Vector3 hitPoint)
    {
        if (collider.attachedRigidbody != null)
        {
            GameObject obj = collider.gameObject;
            if (obj.tag != "MainCamera")
            {
                GameObject clone = Instantiate(obj) as GameObject;
                Rigidbody rb = clone.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                clone.transform.position = hitPoint;
                clone.SetActive(true);
                Utils.AddClone(clone);
            }
        }
    }

    private void CrazyClone(Collider collider, Vector3 hitPoint)
    {
        GameObject obj = collider.gameObject;
        if (obj.tag != "MainCamera")
        {
            GameObject clone = Instantiate(obj) as GameObject;
            clone.AddComponent<Rigidbody>();
            BoxCollider col = clone.AddComponent<BoxCollider>();
            Bounds bounds = clone.GetComponent<MeshFilter>().mesh.bounds;
            col.center = bounds.center;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            clone.transform.position = hitPoint;
            clone.SetActive(true);
            Utils.AddClone(clone);
        }
    }

    private bool DeleteAll()
    {
        foreach (Rigidbody obj in FindObjectsOfType<Rigidbody>())
        {
            try
            {
                if (obj.tag != "MainCamera")
                    Destroy(obj.gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " " + e.StackTrace);
                return false;
            }
        }
        return true;
    }

    private bool Delete(Collider collider)
    {
        try
        {
            if (collider.gameObject.tag != "MainCamera")
                Destroy(collider.gameObject);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + " " + e.StackTrace);
            return false;
        }
        return true;
    }

    private bool TryGetPositionString(out string text)
    {
        if (_controller != null)
        {
            string x = "X: " + Math.Round(_controller.gameObject.transform.position.x, 2).ToString();
            string y = "Y: " + Math.Round(_controller.gameObject.transform.position.y, 2).ToString();
            string z = "Z: " + Math.Round(_controller.gameObject.transform.position.z, 2).ToString();
            text = x + " " + y + " " + z;
            return true;
        }
        else
        {
            text = null;
            return false;
        }
    }

    #endregion

    private IEnumerator UpdateVersionLabel()
    {
        yield return new WaitForSeconds(0.05f);
        FindObjectOfType<VersionLabel>()
            .gameObject.GetComponentInParent<TextMeshProUGUI>()
            .text = "v" + Application.version + " (MODDED)";
    }
}