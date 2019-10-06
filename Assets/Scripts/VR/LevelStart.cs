using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public TruckFlock m_flocking = null;

    void Start()
    {
        m_flocking.enabled = false;
    }

    void Update()
    {
        
    }
}
