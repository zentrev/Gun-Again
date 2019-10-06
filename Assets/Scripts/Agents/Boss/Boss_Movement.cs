using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    GameObject player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player != null)
        {
            if((transform.position - player.transform.position).magnitude < 100)
            {
                transform.position += transform.forward * 30 * Time.deltaTime;
            }
        }
    }
}
