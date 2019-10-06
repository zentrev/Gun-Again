using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 10.0f)] float m_lifeTime = 5.0f;
    [SerializeField] [Range(0.0f, 1000000.0f)] float m_speed = 50.0f;

    void Awake()
    {
        Destroy(gameObject, m_lifeTime);
    }

    private void Update()
    {
        transform.position = transform.position + (transform.forward * m_speed * Time.deltaTime);
    }
}
