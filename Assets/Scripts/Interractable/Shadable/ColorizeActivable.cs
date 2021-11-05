using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizeActivable : MonoBehaviour, IShadable
{
    private Activable activable;
    private Color defaultColor;
    public Color DefaultColor { get => defaultColor; set => defaultColor = value; }

    public void OnColorize()
    {
        //if (activable.IsActivated)
        //    activable.GetComponent<Renderer>().material.color = Color.green;
        //else
        //    activable.GetComponent<Renderer>().material.color = defaultColor;
    }

    // Start is called before the first frame update
    void Awake()
    {
        activable = GetComponent<Activable>();
        defaultColor = GetComponent<Renderer>().material.color;
    }
}
