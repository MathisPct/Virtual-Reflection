using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour, ILaser, IStartable, IAwakable
{
    private int maxBounces = 10;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool ReflectOnlyMirror = true;

    public delegate void LaserSensorHitDelegate();
    public event Action<LaserSensor> onLaserSensorHit;

    void Awake()
    {
        OnAwake();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    public void OnAwake()
    {
        laser = GetComponent<LineRenderer>();
    }

    public void OnStart()
    {
        laser.positionCount = maxBounces + 1;
    }

    public virtual void CastLaser(Vector3 position, Vector3 direction)
    {
        laser.SetPosition(0, startPoint.position);
        bool rayHitSomething = false;

        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;
            rayHitSomething = Physics.Raycast(ray, out hit, 300, 1);
            if (rayHitSomething)
            {
                checkSensorCollision(hit);

                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                laser.SetPosition(i + 1, hit.point);

                //Kill laser bounces
                if (!isMirrorCollided(hit) && ReflectOnlyMirror)
                {
                    for (int j = (i + 1); j <= maxBounces; j++)
                    {
                        laser.SetPosition(j, hit.point);
                    }
                    break;
                }
            }
            else
            {
                //points forward in case nothing is collided
                laser.SetPosition(i + 1, direction * 500);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CastLaser(transform.position, transform.forward);
    }

    private bool isMirrorCollided(RaycastHit hit)
    {
        return hit.transform.CompareTag("Mirror");
    }

    private LaserSensor LaserSensorCollided(RaycastHit hit)
    {
        LaserSensor sensor = null;
        if (hit.transform.gameObject.GetComponent<LaserSensor>())
        {
            sensor = hit.transform.gameObject.GetComponent<LaserSensor>();
        }
        return sensor;
    }

    private void checkSensorCollision(RaycastHit hit)
    {
        if(LaserSensorCollided(hit) != null)
        {
            onLaserSensorHit(LaserSensorCollided(hit));
        }
    }


}