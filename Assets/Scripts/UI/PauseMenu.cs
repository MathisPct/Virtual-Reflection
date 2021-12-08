using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private static bool menuIsOpen = false;

    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private InputAction openMenu;
    [Space][SerializeField] private InputActionAsset menuControl;


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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        menuIsOpen = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        menuIsOpen = true;
    }

}
