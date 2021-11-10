using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour, ISensor
{
    [SerializeField] private bool hit;

    public bool IsRecepting
    {
        get => hit;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Laser>().OnLaserColliding += OnLaserDetectingCollision;
    }

    // Update is called once per frame

    #region event
    void OnLaserDetectingCollision(List<GameObject> objectsCollided)
    {
        hit = false;
        foreach(GameObject objectCollided in objectsCollided)
        {
            var potentialSensor = objectCollided.GetComponent<LaserSensor>();
            if(potentialSensor != null)
            {
                if(potentialSensor == this)
                {
                    hit = true;
                }
            }
        }
    }
    #endregion

}
