using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class TurretController : MonoBehaviour
{

    #region Firing Fields
    [Header("Firing")]
    [SerializeField] Transform m_fireTransform = null;
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] [Range(0.0f, 1.0f)] float m_minTriggerPull = 0.2f;
    [SerializeField] bool m_sphereCastFiring = true;
    [SerializeField] [Range(0.0f, 10.0f)] float m_spherecastSize = 0.1f;
    [SerializeField] [Range(0.0f, 2000.0f)] float m_sphereCastDistance = 1000.0f;
    [SerializeField] LayerMask m_spherecastLayerMask = 0;
    #endregion


    #region Rotation Fields
    [Header("Rotation")]
    [SerializeField] [Range(0.0f, 20.0f)] float m_coastSpeed = 0.5f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_touchSpeed = 7.0f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_fullSpeed = 15.0f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_acceleration = 0.5f;
    [SerializeField] [Range(0.0f, 20.0f)] float m_deceleration = 0.5f;
    #endregion

    [SerializeField] MuzzleFlash m_muzzelFlash = null;
    protected float m_targetSpeed = 0.0f;
    protected List<Hand> m_currentHands = new List<Hand>();
    protected Animator m_turretAnimator = null;

    private void Awake()
    {
        m_targetSpeed = m_coastSpeed;
        m_turretAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_turretAnimator.speed = Mathf.Lerp(m_turretAnimator.speed, m_targetSpeed, (m_turretAnimator.speed < m_targetSpeed ? m_acceleration : m_deceleration) * Time.deltaTime);

        foreach (Hand hand in m_currentHands)
        {
            IndexInput input = hand.GetComponent<IndexInput>();
            if (input.TriggerTouch) m_targetSpeed = m_touchSpeed;
            if (input.TriggerPull > 0.0f) m_targetSpeed = (((m_fullSpeed - m_touchSpeed) * input.TriggerPull) + m_touchSpeed);
            if (input.TriggerClick) m_targetSpeed = m_fullSpeed;
        }
        if (m_currentHands.Count == 0) m_targetSpeed = m_coastSpeed;
    }

    public void Fire()
    {
        foreach (Hand hand in m_currentHands)
        {
            IndexInput input = hand.GetComponent<IndexInput>();
            if (input.TriggerPull > m_minTriggerPull)
            {
                if (m_sphereCastFiring)
                {
                    if (Physics.SphereCast(m_fireTransform.position, m_spherecastSize, m_fireTransform.forward, out RaycastHit hitInfo, m_sphereCastDistance, m_spherecastLayerMask))
                    {
                        Debug.Log(hitInfo.transform.name);
                    }
                    GameObject _projectile = Instantiate(m_projectilePrefab, m_fireTransform);

                    StartCoroutine(m_muzzelFlash.Flash());
                }
            }
        }
    }

    public void AttachHand(Hand hand)
    {
        if (m_currentHands.Contains(hand)) throw new System.Exception("Multiple hands on one thing. How'd you do this????");
        m_currentHands.Add(hand);
    }
    public void DetachHand(Hand hand)
    {
        if (!m_currentHands.Contains(hand)) return;
        m_currentHands.Remove(hand);
    }
}
