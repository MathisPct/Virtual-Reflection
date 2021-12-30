using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorWitness : LightWitness
{
    [SerializeField] private Interruptor energySource;
    public override bool TurnOnCondition { get => energySource.IsActivated; }
    // Start is called before the first frame update

}
