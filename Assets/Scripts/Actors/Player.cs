using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player
    {
        /// <summary>
        /// Rig where player is contain
        /// </summary>
        [SerializeField] private XRRig rigVR;
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private Robot robot;
        private Vector3 direction;

        void Awake()
        {
            inputActionReference.action.started += OnMovementChange;
            inputActionReference.action.canceled += OnMovementChange;
        }

        public void OnMovementChange(InputAction.CallbackContext callbackContext)
        {
            if (robot.CanMove)
            {
                Vector2 directionAction = callbackContext.ReadValue<Vector2>();
                direction = new Vector3(directionAction.x, 0, directionAction.y);
            }
        }

        public void Move()
        {
           
        }

        void Update()
        {

        }
    }
}
