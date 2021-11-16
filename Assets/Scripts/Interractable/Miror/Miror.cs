using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miror : MonoBehaviour, IActivable
{
    [SerializeField] private ISensor sensor;

    public bool IsActivated => sensor.IsRecepting;

    void Awake()
    {
        var selectionSensor = GetComponentInChildren<ISensor>();
        if (selectionSensor != null)
        {
            sensor = selectionSensor;
            Debug.Log("Sensor found");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
