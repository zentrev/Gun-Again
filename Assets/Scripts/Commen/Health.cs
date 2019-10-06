using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] UnityEvent OnDeath;
    [SerializeField] [Range(0.0f, 500.0f)] float m_maxHealth = 0.0f;
    private float m_currentHealth = 0.0f;

    private void Awake()
    {
        m_currentHealth = m_maxHealth;
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
