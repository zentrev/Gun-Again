using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 100.0f)] float m_radius = 5.0f;
    [SerializeField] [Range(1.0f, 500.0f)] float m_damage = 5.0f;

    private Collider[] collisionPoints;
    [SerializeField] List<AudioClip> m_explosionSounds = new List<AudioClip>();
    protected AudioSource m_audioSource = null;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Explode();
        }
    }

    public void Explode()
    {
        collisionPoints = Physics.OverlapSphere(transform.position, m_radius);
        foreach (Collider collider in collisionPoints)
        {
            GameObject gameObject = collider.transform.gameObject;
            if (gameObject.GetComponent<Health>()) gameObject.GetComponent<Health>().TakeDamage(m_damage);

            if(m_explosionSounds.Count != 0)
            {
                m_audioSource.clip = m_explosionSounds[Random.Range(0, m_explosionSounds.Count)];
                m_audioSource.Play();
            }
        }
    }
}
