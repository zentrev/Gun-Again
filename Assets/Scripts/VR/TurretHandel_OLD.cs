using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class TurretHandel_OLD : MonoBehaviour
{
    [SerializeField] GameObject turret = null;
    [SerializeField] [Range(0.0f, 50.0f)] float rotationSpeed = 5.0f;

    public bool maintainMomemntum = true;
    public float momemtumDampenRate = 5.0f;

    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;

    protected Quaternion handelOffset = Quaternion.identity;
    protected Quaternion targetRotation = Quaternion.identity;

    protected float initialMappingOffset;
    protected int numMappingChangeSamples = 5;
    protected float[] mappingChangeSamples;
    protected float prevMapping = 0.0f;
    protected float mappingChangeRate;
    protected int sampleCount = 0;

    protected Interactable interactable;


    protected virtual void Awake()
    {
        mappingChangeSamples = new float[numMappingChangeSamples];
        interactable = GetComponent<Interactable>();

        handelOffset = Quaternion.LookRotation(turret.transform.position - transform.position, Vector3.up);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

        if (maintainMomemntum && mappingChangeRate != 0.0f)
        {
            //Dampen the mapping change rate and apply it to the mapping
            mappingChangeRate = Mathf.Lerp(mappingChangeRate, 0.0f, momemtumDampenRate * Time.deltaTime);

            //if (repositionGameObject)
            //{
            //    transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMapping.value);
            //}
        }
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {

        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            turret.GetComponent<TurretController>().AttachHand(hand);
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        targetRotation = Quaternion.LookRotation(turret.transform.position - hand.transform.position) * Quaternion.Inverse(handelOffset);
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0.0f);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); 

        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
        }
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        CalculateMappingChangeRate();
        turret.GetComponent<TurretController>().DetachHand(hand);

    }

    protected void CalculateMappingChangeRate()
    {
        mappingChangeRate = 0.0f;
        int mappingSamplesCount = Mathf.Min(sampleCount, mappingChangeSamples.Length);
        if (mappingSamplesCount != 0)
        {
            for (int i = 0; i < mappingSamplesCount; ++i)
            {
                mappingChangeRate += mappingChangeSamples[i];
            }
            mappingChangeRate /= mappingSamplesCount;
        }
    }


}
