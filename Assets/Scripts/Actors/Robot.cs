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
    [SerializeField] private GameObject robotGameObject;
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField]private Vector3 direction;
    [SerializeField] private Camera headPlayer;
    private TeleportAwareness teleportationAnchor;
    [SerializeField]private TeleportationManager teleportationManager;
    
    /// <summary>
    /// Robot can move when player move
    /// </summary>
    private bool canMoveWhenPlayerMove = false;
    /// <summary>
    /// Robot can rotate when player move
    /// </summary>
    private bool canRotateWhenPlayerRotate = false;

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
    }

    /// <summary>
    /// Allow when player is in robot
    /// </summary>
    private void TranslatePlayerRotation()
    {
        if (canRotateWhenPlayerRotate)
        {
            Vector3 sameRotationAsPlayer = new Vector3(this.transform.eulerAngles.x, headPlayer.transform.eulerAngles.y, this.transform.eulerAngles.z);
            this.transform.rotation = Quaternion.Euler(sameRotationAsPlayer);
        }
    }

    private void TranslatePlayerMove()
    {
        if (canMoveWhenPlayerMove)
        {
            robotGameObject.transform.Translate(this.direction * 0.005f);
        }
    }

    public void BehaviourWhenPlayerEnter()
    {
        canMoveWhenPlayerMove = true;
        canRotateWhenPlayerRotate = true;
        Debug.Log("Player can move in robot");
    }

    public void BehaviourWhenPlayerExit()
    {
        canMoveWhenPlayerMove = false;
        canRotateWhenPlayerRotate = false;
        Debug.Log("Player can't move in robot");
    }
}