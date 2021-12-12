using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitingDoor : Openable
{
    [SerializeField] private Room roomToExit;

    private new void Awake()
    {
        base.Awake();
        roomToExit = FindObjectOfType<Room>();
    }

    public override bool OpeningCondition { get => roomToExit.IsUnlocked; }

}
