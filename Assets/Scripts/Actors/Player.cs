using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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
        private Vector3 joystickDirection;
        private bool controlInteractable = false;

        [SerializeField] private ControlableInteractable controlable;

        public ControlableInteractable Controlable
        {
            get => controlable;
            set
            {
                controlable = value;
                if(value != null)
                {
                    controlable.OnControl += CanMoveInteractable;
                    controlable.OnDiscontrol += CantMoveInteractable;
                }
            }
        }

        void Awake()
        {

        }

        private void Start()
        {
            var spawn = FindObjectOfType<Spawn>();
            if (spawn != null)
            {
                this.transform.position = spawn.gameObject.transform.position;
                this.transform.rotation = spawn.gameObject.transform.rotation;
                Debug.Log("TELEPORTED TO SPAWN");
            } else
            {
                Debug.Log("SPAWN NOT FOUND");
            }
            
        }

        public void InputMovementRobot(InputAction.CallbackContext callbackContext)
        {
            Vector2 directionAction = callbackContext.ReadValue<Vector2>();
            joystickDirection = new Vector3(directionAction.x, 0, directionAction.y);
            ApplyMovementToInteractable();
        }

        public void CanMoveInteractable()
        {
            Debug.Log("CanMove in interactable");
            controlInteractable = true;
            joystickInput.action.started += InputMovementRobot;
            joystickInput.action.canceled += InputMovementRobot;
        }

        public void CantMoveInteractable()
        {
            controlInteractable = false;
            joystickInput.action.started -= InputMovementRobot;
            joystickInput.action.canceled -= InputMovementRobot;
            controlable = null;
        }

        private void ApplyMovementToInteractable()
        {
            Controlable.VectorMovement = joystickDirection;
        }

        public void Update()
        {

        }

    }
}
