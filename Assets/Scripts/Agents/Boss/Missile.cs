using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float missileForce = 10;
    [SerializeField] float missileDamage = 10;

    Vector3 targetPosition;
    Rigidbody rb = null;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = player.transform.position + new Vector3(0.0f, player.transform.position.y + Random.Range(-15.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        rb = GetComponent<Rigidbody>();
        rb.AddForce((targetPosition - transform.position) * missileForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] objectsExplodedness = Physics.OverlapSphere(transform.position, 10);

        for(int i = 0; i < objectsExplodedness.Length; i++)
        {
            if(objectsExplodedness[i].gameObject.GetComponent<Health>())
            {
                objectsExplodedness[i].gameObject.GetComponent<Health>().TakeDamage(missileDamage);
            }
        }
    }
}
