using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckFlock : MonoBehaviour
{
    [SerializeField] float randomAngleMax = 0.1f;
    [SerializeField] float speed = 30.0f;
    [SerializeField] BoxCollider collisionCollider = null;
    private Vector3 forward = Vector3.right;
    private Vector3 newForward = Vector3.right;
    private float randomness = 1.0f;
    private float timer = 1.0f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        collisionCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= timer)
        {
            GetNewRandom();
            newForward = new Vector3(1 + randomness, 0, randomness);
            time = 0;
        }
        forward = Vector3.Lerp(forward, newForward, 1.0f * Time.deltaTime);
        //gameObject.transform.position = Vector3.Lerp(transform.position, transform.position + forward * speed * Time.deltaTime, 1);
        gameObject.transform.position += forward * speed * Time.deltaTime;
        time += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            forward = new Vector3(1, 0, -forward.z * 10);
        }
    }

    void GetNewRandom()
    {
        randomness = Random.Range(-randomAngleMax, randomAngleMax);
        if (Mathf.Abs(forward.z) + Mathf.Abs(randomness) > 1.0f)
        {
            GetNewRandom();
        }
    }
}
