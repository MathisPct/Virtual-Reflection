using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour, IActivable
{
    [SerializeField] private List<GameObject> activableGameObjects = new List<GameObject>();
    [SerializeField] private ISensor sensor;

    public bool IsActivated => AreInputsActivated() && sensor.IsRecepting;


    void Awake()
    {
        var selectionSensor = GetComponentInChildren<ISensor>();
        if (selectionSensor != null)
        {
            sensor = selectionSensor;
            Debug.Log("Sensor of interruptor found");
        }
    }
    public bool AreInputsActivated()
    {
        bool areInputsActivated = false;

        if (activableGameObjects != null && activableGameObjects.Count > 0)
        {
            areInputsActivated = true;
            foreach (GameObject gameObject in activableGameObjects)
            {
                if(gameObject != this.gameObject)
                {
                    IActivable selectionIActivable = gameObject.GetComponent<IActivable>();
                    if (selectionIActivable != null)
                    {
                        if (!selectionIActivable.IsActivated)
                        {
                            areInputsActivated = false;
                        }
                    }
                }
            }
            
        }
        return areInputsActivated;
    }

}
