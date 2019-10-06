using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TruckShooty : MonoBehaviour
{
    [SerializeField] float m_fireRate = 1.0f;

    [SerializeField] Transform m_fireTransform = null;
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_spherecastSize = 0.1f;
    [SerializeField] [Range(0.0f, 2000.0f)] float m_sphereCastDistance = 1000.0f;
    [SerializeField] LayerMask m_spherecastLayerMask = 0;
    [SerializeField] [Range(0.0f, 1000.0f)] float m_damageAmount = 0.0f;

    [SerializeField] GameObject m_turret = null;
    [SerializeField] GameObject m_target = null;
    [SerializeField] MuzzleFlash m_muzzelFlash = null;
    [SerializeField] List<AudioClip> m_firingSounds = new List<AudioClip>();
    protected AudioSource m_audioSource = null;




    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Animator>().speed = m_fireRate;
        m_audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        m_turret.transform.rotation = Quaternion.LookRotation(m_turret.transform.position - m_target.transform.position);
    }

    public void Fire()
    {
        if (Physics.SphereCast(m_fireTransform.position, m_spherecastSize, m_fireTransform.forward, out RaycastHit hitInfo, m_sphereCastDistance, m_spherecastLayerMask))
        {
            //Debug.Log(hitInfo.transform.name);
            Health health = hitInfo.transform.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(m_damageAmount);
            }
        }
        GameObject _projectile = Instantiate(m_projectilePrefab, m_fireTransform);
        _projectile.transform.SetParent(null);

        StartCoroutine(m_muzzelFlash.Flash());

        if (m_firingSounds.Count != 0)
        {
            m_audioSource.clip = m_firingSounds[Random.Range(0, m_firingSounds.Count-1)];
            m_audioSource.Play();
        }
    }
}
