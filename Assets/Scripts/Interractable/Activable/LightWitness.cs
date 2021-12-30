using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightWitness : MonoBehaviour, IActivable, IShadable
{

    [SerializeField] private GameObject objectToLight;
    [SerializeField] private Color turnedOffColor;
    [SerializeField] private Color activatedColor;
    private float intensity = 2f;

    public virtual bool TurnOnCondition { get; }

    public bool IsActivated => TurnOnCondition;


    // Update is called once per frame
    void Update()
    {
        if (TurnOnCondition)
        {
            OnColorize();
        }
        else
        {
            OnDecolorize();
        }
    }

    public void OnColorize()
    {
        objectToLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", activatedColor * intensity);
    }

    public void OnDecolorize()
    {
        objectToLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", turnedOffColor * intensity);
    }

}
