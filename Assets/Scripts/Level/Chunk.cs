using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] Transform truckPosition;
    [SerializeField] Transform startNode;
    [SerializeField] Transform endNode;
    [SerializeField] float chunkLoadDistance = 1000;
    [SerializeField] float chunkDestroyDistance = 500;
    public bool newChunkLoaded = false;
    public bool bossLoaded = false;
    public float endNodeDistance;
    public float startNodeDistance;
    private GameObject nextChunk;
    private Vector3 distance;

    public Transform StartNode { get => startNode; set => startNode = value; }
    public Transform EndNode { get => endNode; set => endNode = value; }

    //True means it is the start, false means the end node.

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        truckPosition = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        endNodeDistance = EndNode.transform.position.magnitude - truckPosition.transform.position.magnitude;
        if(endNodeDistance < chunkLoadDistance && !newChunkLoaded)
        {
            if(!bossLoaded && GameManager.Instance.enemiesKilled < 15)
            {
                nextChunk = LevelCreator.Instance.CanyonBits[Random.Range(0, LevelCreator.Instance.CanyonBits.Length - 1)];
            }
            else if (!bossLoaded)
            {
                nextChunk = LevelCreator.Instance.CanyonBits[LevelCreator.Instance.CanyonBits.Length];
                bossLoaded = true;
            }
            else
            {
                nextChunk = LevelCreator.Instance.CanyonBits[1];
            }
            distance = nextChunk.transform.position - nextChunk.GetComponent<Chunk>().startNode.position;
            Instantiate(nextChunk, EndNode.transform.position + distance , gameObject.transform.rotation);
            newChunkLoaded = true;
        }

        startNodeDistance = truckPosition.transform.position.magnitude - StartNode.transform.position.magnitude;
        if(startNodeDistance > chunkDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
