using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.SelectionManager
{
    public class Controlable : Selectable, IControlable
    {
        [SerializeField] private InputActionReference inputActionRef;

        public InputActionReference InputActionRef { get => inputActionRef; set => inputActionRef = value; }

        [SerializeField] private Vector3 direction;
        public Vector3 Direction
        {
            get => direction;
        }
        [Space] [SerializeField] private InputActionAsset robotControls;

        public override TeleportationAnchor teleportationAnchor
        {
            get => teleportationAnchor;
            set
            {
                this.teleportationAnchor = value;
            }
        }

        public void SetMovementAction(InputActionReference actionReference)
        {
            this.InputActionRef = actionReference;
        }
        
        public InputActionReference getRefInput()
        {
            return InputActionRef;
        }

        public void Movement(InputAction.CallbackContext obj)
        {
            Vector2 directionAction = obj.ReadValue<Vector2>();
            direction = new Vector3(directionAction.x, 0, directionAction.y);
            Debug.Log(direction);
        }

        public void OnControl(SelectEnterEventArgs args)
        {
            InputActionRef.action.started += Movement;
            InputActionRef.action.canceled += Movement;
        }

        public void OnDiscontrol()
        {
            InputActionRef.action.started -= Movement;
            InputActionRef.action.canceled -= Movement;
        }
    }
}
