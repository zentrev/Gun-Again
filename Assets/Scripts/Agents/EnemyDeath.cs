using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public PathCreator[] paths;
    public PathCreator pathCreator;
    public EndOfPathInstruction end = EndOfPathInstruction.Stop;
    public float speed = 30;
    float dstTravelled;
    float totalDistance;
    int pathChoice = -1;
    // Start is called before the first frame update
    public void Start()
    {
        if(pathChoice != -1)
        {
            pathCreator = paths[pathChoice];
        }
        else
        {
            pathCreator = paths[0];
        }
        pathCreator.gameObject.transform.parent = null;
        totalDistance = pathCreator.path.length;
    }

    // Update is called once per frame
    void Update()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
        if (dstTravelled >= totalDistance)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        enabled = true;
        pathChoice = 1;
    }

}
