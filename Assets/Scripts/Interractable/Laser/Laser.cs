using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour, ILaser
{
    private int maxBounces = 3;
    private int maxDistance = 50;
    [SerializeField] private int bounceCount = 0;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool ReflectOnlyMirror = true;

    [SerializeField] private List<GameObject> objectsCollided = new List<GameObject>();

    public delegate void ObjectHitDelegate();
    public event Action<List<GameObject>> OnLaserColliding;

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }


    public virtual void CastLaser(Vector3 position, Vector3 direction)
    {
        InitialLaserRenderingPosition();

        RaycastHit hit;
        Ray ray = new Ray(position, direction);

        bool rayHitSomething = Physics.Raycast(ray, out hit, maxDistance, 1);
        AddToCollidedObjectsList(hit);

        if (rayHitSomething)
        {
            bounceCount++;
            ObjectCollision(objectsCollided); //utile pour déclencher l'évenement de collision d'object

            position = hit.point;
            direction = Vector3.Reflect(direction, hit.normal);
            laser.positionCount = laser.positionCount + 1;
            laser.SetPosition(bounceCount, hit.point);

            if (isMirrorCollided(hit) && bounceCount <= maxBounces)
            {
                CastLaser(position, direction);
            }
            else
            {
                //points forward in case nothing is collided
                laser.SetPosition(bounceCount + 1, hit.point);
            }
        }
        else
        {
            //points forward in case nothing is collided
            laser.SetPosition(bounceCount + 1, direction * maxDistance);
        }
    }

    private void InitialLaserRenderingPosition()
    {
        if (bounceCount == 0)
        {
            laser.SetPosition(bounceCount, startPoint.position);
        }
    }

    private void AddToCollidedObjectsList(RaycastHit hit)
    {
        var objectToAdd = hit.transform.gameObject;
        if (!objectsCollided.Contains(objectToAdd))
        {
            objectsCollided.Add(objectToAdd);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CastLaser(transform.position, transform.forward);
        bounceCount = 0;
        laser.positionCount = bounceCount + 2;
        CastLaser(transform.position, transform.forward);
        objectsCollided.Clear();
    }

    private bool isMirrorCollided(RaycastHit hit)
    {
        return hit.transform.CompareTag("Mirror");
    }

    /// <summary>
    /// Cette méthode notifie tout les abonnés de l'évenement OnObjectHit quel object a été touché par le laser
    /// </summary>
    /// <param name="hit"></param>
    private void ObjectCollision(List<GameObject> objectsCollidedByLaser)
    {
        OnLaserColliding(objectsCollidedByLaser);
    }
}