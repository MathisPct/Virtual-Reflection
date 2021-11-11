using Assets.Scripts.SelectionManager;
using Assets.Scripts.TeleportationManager;
using Assets.Scripts.XRExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Robot : MonoBehaviour, IAwareness, IControlable
{
    [SerializeField] private GameObject robotGameObject;
    [SerializeField] private Camera headPlayer;
    [SerializeField]private TeleportationManager teleportationManager;
    [SerializeField] private float speedMovement = 1f;
    
    /// <summary>
    /// Robot can move when player move
    /// </summary>
    private bool canMoveWhenPlayerMove = false;
    /// <summary>
    /// Robot can rotate when player move
    /// </summary>
    private bool canRotateWhenPlayerRotate = false;

    public event ControlHandler OnControl;
    public event DiscontrolHandler OnDiscontrol;

    void Awake()
    {
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

    public void Move(Vector3 direction)
    {
        Vector3 move = transform.right * direction.x + transform.forward * direction.z;
        this.robotGameObject.transform.Translate(move * speedMovement * Time.deltaTime);
    }

    public void BehaviourWhenPlayerEnter()
    {
        canMoveWhenPlayerMove = true;
        canRotateWhenPlayerRotate = true;
        OnControl?.Invoke();
        Debug.Log("Player can move in robot");
    }

    public void BehaviourWhenPlayerExit()
    {
        canMoveWhenPlayerMove = false;
        canRotateWhenPlayerRotate = false;
        OnDiscontrol?.Invoke();
        Debug.Log("Player can't move in robot");
    }
}