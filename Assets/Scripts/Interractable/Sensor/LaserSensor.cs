using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : Activable, ISensor
{
    [SerializeField] private bool hit;

    public bool IsRecepting
    {
        get => IsActivated;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Laser>().onLaserSensorHit += OnLaserSensorHit;
    }

    // Update is called once per frame

    #region event
    void OnLaserSensorHit(LaserSensor laserSensor)
    {
        if (laserSensor == this)
        {
            hit = true;
        }
    }
    #endregion

    void Update()
    {

        if (hit)
        {
            IsActivated = true;
            hit = false;
        }
    }

    void LateUpdate()
    {
        IsActivated = false;
    }
}
