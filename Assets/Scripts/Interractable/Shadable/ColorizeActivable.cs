using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizeActivable : MonoBehaviour, IShadable
{
    [SerializeField] private Activable activable;
    [SerializeField] private Material emissiveMat;
    [SerializeField] private Color emissiveColorDefault;
    [SerializeField] private Color activatedColor;
    private float intensity = 3f;


    public void OnColorize()
    {

        emissiveMat.SetColor("_EmissionColor", activatedColor* intensity);
        //Debug.Log("OnColorize called");
    }

    public void OnDecolorize()
    {
        emissiveMat.SetColor("_EmissionColor", emissiveColorDefault* intensity);
        //Debug.Log("OnDecolorize called");
    }

    // Start is called before the first frame update
    void Awake()
    {
        activable = GetComponent<Activable>();      
    }

}
