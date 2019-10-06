using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] [Range(0.0f, .10f)] float flashTime = 00.0f;


    [SerializeField] Vector3 rotationAxis = Vector3.zero;
    [SerializeField] [Range(0.0f, 2.0f)] float minScale = 00.0f;
    [SerializeField] [Range(0.0f, 2.0f)] float maxScale = 00.0f;

    [SerializeField] List<GameObject> muzzleFlashes = new List<GameObject>();
    protected Dictionary<GameObject, Vector3> flashes = new Dictionary<GameObject, Vector3>();
    void Awake()
    {
        foreach (GameObject flash in muzzleFlashes)
        {
            flash.GetComponent<MeshRenderer>().enabled = false;

            flashes.Add(flash, flash.transform.localScale);
        }

        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator Flash()
    {
        foreach (GameObject flash in flashes.Keys)
        {
            flash.GetComponent<MeshRenderer>().enabled = true;
            flash.transform.localScale = new Vector3(flashes[flash].x * (Random.Range(minScale, maxScale)), flashes[flash].y * (Random.Range(minScale, maxScale)), flashes[flash].z * (Random.Range(minScale, maxScale)));
            flash.transform.localRotation *= Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), rotationAxis);
        }

        yield return new WaitForSeconds(flashTime);

        foreach (GameObject flash in flashes.Keys)
        {
            flash.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
