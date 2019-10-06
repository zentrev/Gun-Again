using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float missileForce = 10;

    Vector3 targetPosition;
    Rigidbody rb = null;

    private void Start()
    {
        targetPosition += GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.0f, transform.position.y, Random.Range(0.0f, 5.0f));
        rb = GetComponent<Rigidbody>();
        rb.AddForce(targetPosition * missileForce);
    }
}
