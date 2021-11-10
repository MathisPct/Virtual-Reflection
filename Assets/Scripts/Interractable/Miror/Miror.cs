using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miror : Activable
{
    [SerializeField] private ISensor sensor;

    void Awake()
    {
        var selectionSensor = GetComponent<ISensor>();
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

    // Update is called once per frame
    void Update()
    {
        IsActivated = sensor.IsRecepting;
    }
}
