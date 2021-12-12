using Assets.Scripts;
using Assets.Scripts.SelectionManager;
using Assets.Scripts.TeleportationManager;
using Assets.Scripts.XRExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlableInteractable : XRBaseInteractable, IAwareness, IControlable
{
    private float speedMovement = 1f;
    private float speedRotation = 50f;
    [SerializeField] private XRBaseInteractor leftInteractor;
    [SerializeField] private XRBaseInteractor rightInteractor;

    [SerializeField] private GameObject rotatingPart;

    private Vector3 vectorMovement;
    private Vector3 vectorRotation;
    public Vector3 VectorMovement { get => vectorMovement; set => vectorMovement = value; }
    public Vector3 VectorRotation { get => vectorRotation; set => vectorRotation = value; }

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

    void Update()
    {
        if (canMoveWhenPlayerMove)
        {
            Move();
        }

        if (canRotateWhenPlayerRotate)
        {
            Rotate();
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        FindPlayer().Controlable = this;
        MapCorrectInteractor(args.interactor);
        BehaviourWhenPlayerEnter();
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        UnmapCorrectInterractor(args.interactor);

        //seulement si l'autre interactor que celui là est null
        if (GetOppositeInteractor(args.interactor) == null)
        {
            BehaviourWhenPlayerExit();
        }
    }

    private Player FindPlayer()
    {
        Player player = FindObjectOfType<Player>();
        return player;
    }

    public void Move()
    {
        //Stuff to move
        float x = VectorMovement.x * speedMovement * Time.deltaTime;
        float z = VectorMovement.z * speedMovement * Time.deltaTime;
        this.transform.Translate(x, 0, z, Space.Self);
    }

    private void Rotate()
    {
        //Stuff to rotate
        float y = VectorRotation.x * speedRotation * Time.deltaTime;
        rotatingPart.transform.Rotate(0, y, 0, Space.World);
    }

    public void BehaviourWhenPlayerEnter()
    {
        Debug.Log("Player can move controlable");
        canMoveWhenPlayerMove = true;
        canRotateWhenPlayerRotate = true;
        OnControl?.Invoke();
    }

    public void BehaviourWhenPlayerExit()
    {
        Debug.Log("Player can't move controlable");
        OnDiscontrol?.Invoke();
        vectorMovement = Vector3.zero;
        canMoveWhenPlayerMove = false;
        canRotateWhenPlayerRotate = false;
    }

    public string InteractorName(XRBaseInteractor interactorToGuess)
    {
        return interactorToGuess.name;
    }

    private XRBaseInteractor GetOppositeInteractor(XRBaseInteractor interactor)
    {
        XRBaseInteractor oppositeInteractor = null;

        switch (interactor.name)
        {
            case "LeftHand Controller": 
                oppositeInteractor = rightInteractor;
                break;
            case "RightHand Controller":
                oppositeInteractor = leftInteractor;
                break;
        }
        return oppositeInteractor;
    }

    private void MapCorrectInteractor(XRBaseInteractor interactorToMap)
    {
        switch (interactorToMap.name)
        {
            case "LeftHand Controller":
                leftInteractor = interactorToMap;
                break;
            case "RightHand Controller":
                rightInteractor = interactorToMap;
                break;
        }
    }

    private void UnmapCorrectInterractor(XRBaseInteractor interactorToMap)
    {
        switch (interactorToMap.name)
        {
            case "LeftHand Controller":
                leftInteractor = null;
                break;
            case "RightHand Controller":
                rightInteractor = null;
                break;
        }
    }
}