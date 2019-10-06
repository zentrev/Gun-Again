using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    protected Animator m_barrelAnimator = null;
    public List<GameObject> m_barrels = null;
    private float m_minTimer = 5.0f;
    private float m_maxTimer = 15.0f;

    private void Awake()
    {
        m_barrelAnimator = GetComponent<Animator>();
        TimeLease.NewTime(gameObject.GetInstanceID().ToString(), Random.Range(m_minTimer, m_maxTimer));
    }

    void Update()
    {
        if (TimeLease.CheckTime(gameObject.GetInstanceID().ToString()))
        {
            TimeLease.NewTime(gameObject.GetInstanceID().ToString(), Random.Range(m_minTimer, m_maxTimer));
            EjectBarrel();
        }
    }

    public void PopTrunk()
    {
        m_barrelAnimator.SetTrigger("Pop");
    }

    private void EjectBarrel()
    {
        if (m_barrels.Count <= 0) return;
        GameObject barrel = m_barrels[0];
        m_barrels.RemoveAt(0);

        barrel.transform.SetParent(null);
        barrel.GetComponent<Rigidbody>().isKinematic = false;
        barrel.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
    }
}
