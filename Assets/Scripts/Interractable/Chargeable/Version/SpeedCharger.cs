using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCharger : MonoBehaviour, IChargeable
{
    public float MaxCharge { get => 100; }
    public float LoadSpeed { get => 100; }
    public float UnloadSpeed { get => 100; }
    public float SufficientChargeRatio { get => 0.9f; }
}
