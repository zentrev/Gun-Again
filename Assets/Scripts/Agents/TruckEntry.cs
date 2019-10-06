using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEntry : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end = EndOfPathInstruction.Stop;
    public float speed = 30;
    public float spawnDistance = 100.0f;
    float dstTravelled;
    float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.Instance.AgentSpeed;
        gameObject.transform.parent = null;
        gameObject.GetComponent<TruckFlock>().enabled = false;
        totalDistance = pathCreator.path.length;
        if (LevelCreator.Instance.Enemies.Count >= 5)
        {
            Destroy(gameObject);
        }
        else
        {
            LevelCreator.Instance.Enemies.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < spawnDistance)
        {
            EnterCanyon();
        }
        if (dstTravelled >= totalDistance)
        {
            gameObject.GetComponent<TruckShooty>().enabled = true;
            gameObject.GetComponent<TruckFlock>().enabled = true;
            //gameObject.GetComponent<EnemyDeath>().enabled = true;
            enabled = false;
        }
    }

    void EnterCanyon()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
    }
}
