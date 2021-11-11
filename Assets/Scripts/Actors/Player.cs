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
    public class Player: MonoBehaviour
    {
        /// <summary>
        /// Rig where player is contain
        /// </summary>
        [SerializeField] private XRRig rigVR;
        [SerializeField] private InputActionReference joystickInput;
        [SerializeField] private Robot robot;
        private Vector3 direction;

        void Awake()
        {
            robot.OnControl += CanMoveRobot;
            robot.OnDiscontrol += CantMoveRobot;
        }

        public void OnMovementChange(InputAction.CallbackContext callbackContext)
        {
            Vector2 directionAction = callbackContext.ReadValue<Vector2>();
            direction = new Vector3(directionAction.x, 0, directionAction.y);
            ApplyMovementToRobot(direction);
        }

        public void CanMoveRobot()
        {
            joystickInput.action.started += OnMovementChange;
            joystickInput.action.canceled += OnMovementChange;
        }

        private void ApplyMovementToRobot(Vector3 direction)
        {
            robot.Move(direction);
        }

        public void CantMoveRobot()
        {
            joystickInput.action.started -= OnMovementChange;
            joystickInput.action.canceled -= OnMovementChange;
        }

        void Update()
        {

        }
    }
}
