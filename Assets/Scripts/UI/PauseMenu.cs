using Assets.Scripts.XRExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuUI;
    [SerializeField] private Button resumButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button lastCamButton;
    [SerializeField] private Button gotToSpawnButton;

    private TeleportationManager teleportationManager;

    [SerializeField] private InputAction openMenu;
    [Space][SerializeField] private InputActionAsset menuControl;

    private changeScene changeScene;


    public void Awake()
    {
        var menuActionMap = menuControl.FindActionMap("UI");
        openMenu = menuActionMap.FindAction("OpenMenu");
        openMenu.performed += OnMenu;
        this.changeScene = FindObjectOfType<changeScene>();

        pauseMenuUI.enabled = false;
        teleportationManager = FindObjectOfType<TeleportationManager>();
    }

    /// <summary>
    /// When player open menu with input binding
    /// </summary>
    /// <param name="context"></param>
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (pauseMenuUI!= null)
            if (pauseMenuUI.enabled)
            {
                QuitMenu();
            }
        else
            OpenMenu();
    }

   
    public void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    private void SetupButtons()
    {
        this.resumButton.onClick.AddListener(delegate { QuitMenu(); });
        this.quitButton.onClick.AddListener(delegate { ReturnToLaunch(); });
        this.lastCamButton.onClick.AddListener(delegate { ReturnToLastPoint(); });
        this.gotToSpawnButton.onClick.AddListener(delegate { GoToSpawn(); });
    }

    public void QuitMenu()
    {
        pauseMenuUI.enabled = false;
        Debug.Log("Quit menu");
        UnPause();
        ResetSelection();
    }

    /// <summary>
    /// Return to the main menu 
    /// Call when player clicked in quit button
    /// </summary>
    public void ReturnToLaunch()
    {
        Debug.Log("Return to main menu");
        changeScene.LoadScene("Menu");
        QuitMenu();
    }

    public void ReturnToLastPoint()
    {
        teleportationManager.GoToPreviousPoint();
        QuitMenu();
    }

    public void GoToSpawn()
    {
        teleportationManager.GoToSpawn();
        QuitMenu();
    }

    public void OpenMenu()
    {
        pauseMenuUI.enabled = true;
        InitTeleportationButtons();
        Pause();
    }

    private void UnPause()
    {
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void ResetSelection()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    /// <summary>
    /// Affiche les boutons si le joueur s'est déjà téléporté dans un objet
    /// </summary>
    private void InitTeleportationButtons()
    {
        lastCamButton.gameObject.SetActive(teleportationManager.LastObjectPlayerTeleportIn != null);
        gotToSpawnButton.gameObject.SetActive(teleportationManager.LastObjectPlayerTeleportIn != null);
    }
}
