using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaser
{
    public void CastLaser(Vector3 position, Vector3 direction);
}
