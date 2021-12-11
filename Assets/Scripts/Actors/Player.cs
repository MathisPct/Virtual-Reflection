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
        [SerializeField] private InputActionReference joystickMouvementInput;
        [SerializeField] private InputActionReference joystickRotateInput;
        [SerializeField] private Robot robot;

        private Vector3 joystickMouvement;
        private Vector3 joystickRotation;

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
                    controlable.OnControl += CanRotateInteractable;
                    controlable.OnDiscontrol += CantMoveInteractable;
                    controlable.OnDiscontrol += CantRotateInteractable;
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
            joystickMouvement = new Vector3(directionAction.x, 0, directionAction.y);
            ApplyMovementToInteractable();
        }

        public void InputRotateRobot(InputAction.CallbackContext callbackContext)
        {
            Vector2 rotateAction = callbackContext.ReadValue<Vector2>();
            joystickRotation = new Vector3(rotateAction.x, 0, rotateAction.y);
            ApplyRotationToInteractable();
        }

        public void CanRotateInteractable()
        {
            Debug.Log("CanRotate interactable");
            controlInteractable = true;
            joystickRotateInput.action.started += InputRotateRobot;
            joystickRotateInput.action.canceled += InputRotateRobot;
        }

        public void CantRotateInteractable()
        {
            controlInteractable = false;
            joystickRotateInput.action.started -= InputRotateRobot;
            joystickRotateInput.action.canceled -= InputRotateRobot;
            controlable = null;
        }

        public void CanMoveInteractable()
        {
            Debug.Log("CanMove in interactable");
            controlInteractable = true;
            joystickMouvementInput.action.started += InputMovementRobot;
            joystickMouvementInput.action.canceled += InputMovementRobot;
        }

        public void CantMoveInteractable()
        {
            controlInteractable = false;
            joystickMouvementInput.action.started -= InputMovementRobot;
            joystickMouvementInput.action.canceled -= InputMovementRobot;
            controlable = null;
        }

        private void ApplyMovementToInteractable()
        {
            Controlable.VectorMovement = joystickMouvement;
        }

        private void ApplyRotationToInteractable()
        {
            Controlable.VectorRotation = joystickRotation;
        }

        public void Update()
        {

        }

    }
}
