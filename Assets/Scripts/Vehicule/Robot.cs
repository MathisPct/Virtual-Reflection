using Assets.Scripts.SelectionManager;
using Assets.Scripts.TeleportationManager;
using Assets.Scripts.XRExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Robot : MonoBehaviour, IAwareness
{

    public float MotorForce, SteerForce, BreakForce, friction;
    public WheelCollider front_L_Wheel, front_R_wheel, wheelbackleft, wheelbackright;
    public GameObject car;
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField]private Vector3 direction;
    [SerializeField] private Camera headPlayer;
    private TeleportAwareness teleportationAnchor;
    [SerializeField]private TeleportationManager teleportationManager;
    
    private bool canMove = false;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Awake()
    {
        teleportationAnchor = GetComponent<TeleportAwareness>();
        inputActionReference.action.started += Movement;
        inputActionReference.action.canceled += Movement;
    }

    private void Movement(InputAction.CallbackContext obj)
    {
        Vector2 directionAction = obj.ReadValue<Vector2>();
        direction = new Vector3(directionAction.x, 0, directionAction.y);
        Debug.Log(direction);
    }

    void Update()
    {
        TranslatePlayerRotation();
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    wheelbackleft.brakeTorque = BreakForce;
        //    wheelbackright.brakeTorque = BreakForce;
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    wheelbackleft.brakeTorque = 0;
        //    wheelbackright.brakeTorque = 0;
        //}
        //if (Input.GetAxis("Vertical") == 0)
        //{
        //    if (wheelbackleft.brakeTorque <= BreakForce && wheelbackright.brakeTorque <= BreakForce)
        //    {
        //        wheelbackleft.brakeTorque += friction * Time.deltaTime * BreakForce;
        //        wheelbackright.brakeTorque += friction * Time.deltaTime * BreakForce;
        //    }
        //    else
        //    {
        //        wheelbackleft.brakeTorque = BreakForce;
        //        wheelbackright.brakeTorque = BreakForce;
        //    }
        //}
        //else
        //{
        //    wheelbackleft.brakeTorque = 0;
        //    wheelbackright.brakeTorque = 0;
        //}
    }

    /// <summary>
    /// Allow when player is in robot
    /// </summary>
    private void TranslatePlayerRotation()
    {
        if (CanMove)
        {
            car.transform.Translate(this.direction * 0.005f);
            this.transform.Rotate(Vector3.up * SteerForce * Time.deltaTime * headPlayer.transform.rotation.y, Space.World);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void TeleportationBehaviour()
    {
        CanMove = true;
        Debug.Log("Player can move in robot");
    }

    public void NotTeleportationBehaviour()
    {
        CanMove = false;
        Debug.Log("Player can't move in robot");
    }
}