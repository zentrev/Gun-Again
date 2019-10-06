using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmokingScript : MonoBehaviour
{
    [SerializeField] Health m_health = null;
    [SerializeField] [Range(0.00f, 1.0f)] float m_smokingStart = .25f;
    private ParticleSystem m_particals = null;

    void Awake()
    {
        m_particals = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        m_particals.enableEmission = (m_health.m_currentHealth / m_health.m_maxHealth <= m_smokingStart) ? true : false;
    }
}
