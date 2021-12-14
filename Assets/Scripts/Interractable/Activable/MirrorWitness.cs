using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWitness : LightWitness
{
    [SerializeField] private Miror energySource;
    public override bool TurnOnCondition { get => energySource.IsActivated; }
}
