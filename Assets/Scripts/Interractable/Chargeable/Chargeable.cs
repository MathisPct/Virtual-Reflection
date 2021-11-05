using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chargeable : MonoBehaviour
{
    private float charge;
    private IChargeable Ichargeable;

    public float Charge { get => charge; set => charge = value; }

    void Awake()
    {
        //Ichargeable = GetComponent<IChargeable>();
        var selection = GetComponent<IChargeable>();
        if (selection != null)
        {
            Ichargeable = selection;
            Debug.Log("ICHARGEABLE FOUND");
        }
    }

    protected void ClampLoad()
    {
        if (charge < 0) charge = 0;
        if (charge > Ichargeable.MaxCharge) charge = Ichargeable.MaxCharge;
    }

    public bool hasSufficientCharge()
    {
        bool asSufficientCharge = false;
        float sufficientCharge = Ichargeable.SufficientChargeRatio * Ichargeable.MaxCharge;

        if (charge >= sufficientCharge)
            asSufficientCharge = true;

        return asSufficientCharge;
    }


    public void OnLoad()
    {
        charge += Ichargeable.LoadSpeed *Time.deltaTime;
        ClampLoad();
    }

    public virtual void OnUnload()
    {
        charge -= Ichargeable.UnloadSpeed * Time.deltaTime;
        ClampLoad();
    }
}
