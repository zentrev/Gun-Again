using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckShooty : MonoBehaviour
{
    [SerializeField] float m_fireRate = 1.0f;

    [SerializeField] GameObject m_turret = null;

    [SerializeField] GameObject m_target = null;

    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Animator>().speed = m_fireRate;
    }

    void Update()
    {
        m_turret.transform.rotation = Quaternion.LookRotation(m_turret.transform.position - m_target.transform.position);
    }

    public void Fire()
    {

    }
}
