using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    [SerializeField] GameObject missile = null;
    [SerializeField] Transform missileSpawnPoint = null;

    GameObject player = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        if(player != null)
        {
            if((transform.position - player.transform.position).magnitude < 500)
            {
                Instantiate(missile, missileSpawnPoint.position, missile.transform.rotation, null);
            }
        }
    }
}
