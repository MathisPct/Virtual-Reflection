using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeable
{
    public float MaxCharge { get; }
    public float LoadSpeed { get; }
    public float UnloadSpeed { get; }
    public float SufficientChargeRatio { get; }
}
