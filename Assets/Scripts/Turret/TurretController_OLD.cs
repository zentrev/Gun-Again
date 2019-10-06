using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TurretController_OLD : MonoBehaviour
{

    [Header("Firing")]
    [SerializeField] Transform fireTransform = null;
    [SerializeField] GameObject projectile = null;
    [SerializeField] bool sphereCastFireing = true;
    [SerializeField] LayerMask sphereCastLayerMask = 0;
    [SerializeField] float sphereCastSize = .1f;
    [SerializeField] float sphereCastDistance = 1000.0f;

    [SerializeField] [Range(0.0f, 1.0f)] float minTriggerPull = .2f;
    [SerializeField] MuzzleFlash muzzleFlash = null;

    [Header("Rotation")]
    [SerializeField] float coastSpeed = .5f;
    [SerializeField] float touchSpeed = 7.0f;
    [SerializeField] float fullSpeed = 15.0f;
    [SerializeField] float acceleration = .5f;
    [SerializeField] float deceleration = .5f;

    protected float targetSpeed = 0.0f;
    protected List<Hand> currentHands = new List<Hand>();
    protected Animator turretAnimatior = null;


    void Awake()
    {
        targetSpeed = coastSpeed;
        turretAnimatior = GetComponent<Animator>();
    }

    void Update()
    {

        turretAnimatior.speed = Mathf.Lerp(turretAnimatior.speed, targetSpeed, (turretAnimatior.speed < targetSpeed ? acceleration : deceleration) * Time.deltaTime);
        //Debug.Log(turretAnimatior.speed);

        foreach (Hand hand in currentHands)
        {
            IndexInput input = hand.GetComponent<IndexInput>(); 
            if(input.TriggerTouch)
            {
                targetSpeed = touchSpeed;
            }
            if (input.TriggerPull > 0)
            {
                targetSpeed = (((fullSpeed - touchSpeed) * input.TriggerPull) + touchSpeed);
            }
            if(input.TriggerClick)
            {
                targetSpeed = fullSpeed;
            }
        }
        if (currentHands.Count == 0) targetSpeed = coastSpeed;

        
    }

    void Fire()
    {
        foreach (Hand hand in currentHands)
        {
            IndexInput input = hand.GetComponent<IndexInput>();
            if (input.TriggerPull > minTriggerPull)
            {
                //if(sphereCastFireing)
                //{
                //    if(Physics.SphereCast(fireTransform.position, sphereCastSize, fireTransform.forward, out RaycastHit hitInfo, sphereCastDistance, sphereCastLayerMask))
                //    {
                //        Debug.Log(hitInfo.transform.name);
                //    }
                //}

                //GameObject _projectile = Instantiate(projectile, fireTransform);

                StartCoroutine(muzzleFlash.Flash());
            }
        }
    }

    public void AttachHand(Hand hand)
    {
        if (currentHands.Contains(hand)) throw new System.Exception("How did you do this???");
        currentHands.Add(hand);
    }

    public void DetachedHand(Hand hand)
    {
        if (!currentHands.Contains(hand)) return; 
        currentHands.Remove(hand);
    }

}
