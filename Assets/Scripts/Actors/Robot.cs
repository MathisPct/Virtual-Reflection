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
    [SerializeField] private TeleportationManager teleportationManager;
    [SerializeField] private float speedMovement = 2f;
    private CharacterController controller;
    private Vector3 vectorMovement;
    public Vector3 VectorMovement { get => vectorMovement; set => vectorMovement = value; }

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
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        TranslatePlayerRotation();
        if (canMoveWhenPlayerMove)
        {
            controller.Move(VectorMovement * speedMovement * Time.deltaTime);
        }
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
        VectorMovement = direction;
    }

    public void BehaviourWhenPlayerEnter()
    {
        Debug.Log("Player can move in robot");
        canMoveWhenPlayerMove = true;
        canRotateWhenPlayerRotate = true;
        OnControl?.Invoke();
    }

    public void BehaviourWhenPlayerExit()
    {
        Debug.Log("Player can't move in robot");
        canMoveWhenPlayerMove = false;
        canRotateWhenPlayerRotate = false;
        OnDiscontrol?.Invoke();
    }
}