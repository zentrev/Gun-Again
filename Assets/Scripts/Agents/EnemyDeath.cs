using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end = EndOfPathInstruction.Stop;
    public float speed = 30;
    float dstTravelled;
    float totalDistance;
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = pathCreator.path.length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
        if (dstTravelled >= totalDistance)
        {
            Destroy(gameObject);
        }
    }
}
