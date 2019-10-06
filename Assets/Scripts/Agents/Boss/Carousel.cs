using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    [SerializeField] GameObject missile = null;
    [SerializeField] Transform missileSpawnPoint = null;

    public void Shoot()
    {
        Instantiate(missile, missileSpawnPoint.position, Quaternion.identity, null);
    }
}
