﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] UnityEvent OnDeath;
    [SerializeField] [Range(0.0f, 500.0f)] public float m_maxHealth = 0.0f;
    public float m_currentHealth = 0.0f;

    void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        m_currentHealth = m_currentHealth - damage;

        if(m_currentHealth <= 0.0f)
        {
            OnDeath.Invoke();
        }
    }

}
