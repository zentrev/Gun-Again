using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class TurretHandle : MonoBehaviour
{
    [SerializeField] GameObject m_turret = null;
    [SerializeField] [Range(0.0f, 50.0f)] float m_barrelRotationSpeed = 5.0f;

    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;

    protected Quaternion handleOffset = Quaternion.identity;
    protected Quaternion targetRotation = Quaternion.identity;

    protected float initialMappingOffset;
    protected int sampleCount = 0;

    protected Interactable interactable;

    #region MonoBehaviour

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        handleOffset = Quaternion.LookRotation(m_turret.transform.position - transform.position, Vector3.up);
    }

    #endregion

    #region Hand Sytems
    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            m_turret.GetComponent<TurretController>().AttachHand(hand);
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        targetRotation = Quaternion.LookRotation(m_turret.transform.position - hand.transform.position) * Quaternion.Inverse(handleOffset);
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0.0f);
        m_turret.transform.rotation = Quaternion.Slerp(m_turret.transform.rotation, targetRotation, Time.deltaTime * m_barrelRotationSpeed);

        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
        }
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        m_turret.GetComponent<TurretController>().DetachHand(hand);
    }

    #endregion
}