using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseJets : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 2.0f)] float minScale = .5f;
    [SerializeField] [Range(0.0f, 3.0f)] float maxScale = 1.5f;

    [HideInInspector] Vector3 startScale = Vector3.down;


    [Header("Noise")]
    [SerializeField] float heightScale = 1.0f;
    [SerializeField] float xScale = 1.0f;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float modify = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        float scaleModify = ((maxScale - minScale) * modify) + minScale;
        Vector3 vec = new Vector3(0, startScale.y * scaleModify, 0);
        transform.localScale = vec;
    }
}
