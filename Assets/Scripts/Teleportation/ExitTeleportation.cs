using Assets.Scripts.SelectionManager;
using Assets.Scripts.TeleportationManager;
using Assets.Scripts.XRExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.XRExtension
{
    public class ExitTeleportation : TeleportAwareness, IAwareness
    {
        [SerializeField] private string nextLevelName;

        public void BehaviourWhenPlayerEnter()
        {
            if (nextLevelName != null)
            {
                SceneManager.LoadScene(nextLevelName);
            }
        }

        public void BehaviourWhenPlayerExit()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
