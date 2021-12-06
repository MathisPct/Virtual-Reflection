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
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Rig where player is contain
        /// </summary>
        [SerializeField] private XRRig rigVR;
        [SerializeField] private InputActionReference joystickInput;
        [SerializeField] private Robot robot;
        private Vector3 direction;
        private bool controlRobot = false;

        void Awake()
        {
            robot.OnControl += CanMoveRobot;
            robot.OnDiscontrol += CantMoveRobot;
        }

        private void Start()
        {
            var spawn = FindObjectOfType<Spawn>();
            if (spawn != null)
            {
                //this.transform.position = spawn.gameObject.transform.position;
            }
            
        }

        public void InputMovementRobot(InputAction.CallbackContext callbackContext)
        {
            Vector2 directionAction = callbackContext.ReadValue<Vector2>();
            direction = new Vector3(directionAction.x, 0, directionAction.y);
            ApplyMovementToRobot();
        }

        public void CanMoveRobot()
        {
            Debug.Log("CanMove in robot");
            controlRobot = true;
            joystickInput.action.started += InputMovementRobot;
            joystickInput.action.canceled += InputMovementRobot;
        }

        private void ApplyMovementToRobot()
        {
            robot.VectorMovement = direction;
        }

        public void CantMoveRobot()
        {
            controlRobot = false;
            joystickInput.action.started -= InputMovementRobot;
            joystickInput.action.canceled -= InputMovementRobot;
        }

        private void SamePositionAsRobot()
        {
            rigVR.transform.position = robot.transform.TransformPoint(0, 0, 0);
        }

        public void Update()
        {
            if (controlRobot)
            {
                SamePositionAsRobot();
            }
        }
    }
}
