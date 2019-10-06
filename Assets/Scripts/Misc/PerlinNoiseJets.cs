using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseJets : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 2.0f)] float minScale = .5f;
    [SerializeField] [Range(0.0f, 3.0f)] float maxScale = 1.5f;

    [HideInInspector] Vector3 startScale = Vector3.down;
    float random = 0.0f;


    [Header("Noise")]
    [SerializeField] float heightScale = 1.0f;
    [SerializeField] float xScale = 5.0f;

    void Start()
    {
        startScale = transform.localScale;
        random = Random.Range(0.0f, 60.0f);
    }

    void Update()
    {
        float modify = heightScale * Mathf.PerlinNoise((Time.time + random) * xScale, 0.0f);
        float scaleModify = ((maxScale - minScale) * modify) + minScale;
        Vector3 vec = new Vector3(1, startScale.y * scaleModify, startScale.z * scaleModify);
        transform.localScale = vec;
    }
}
