using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private static bool menuIsOpen = false;

    [SerializeField] private Canvas pauseMenuUI;

    [SerializeField] private InputAction openMenu;
    [Space][SerializeField] private InputActionAsset menuControl;

    [SerializeField] private changeScene changeScene;


    public void Awake()
    {
        var menuActionMap = menuControl.FindActionMap("UI");

        openMenu = menuActionMap.FindAction("OpenMenu");

        openMenu.performed += OpenMenu;
        //openMenu.canceled += OpenMenu;
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (menuIsOpen)
            Resume();
        else
            Pause();
    }

   
    public void Start()
    {
        Resume();

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    

    public void Resume()
    {
        pauseMenuUI.enabled = false;
        //Time.timeScale = 1f;
        menuIsOpen = false;
        ResetSelection();
        changeScene.LoadScene("Menu");
    }

    public void Pause()
    {
        pauseMenuUI.enabled = true;
        //Time.timeScale = 0f;
        menuIsOpen = true;
    }

    private void ResetSelection()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
