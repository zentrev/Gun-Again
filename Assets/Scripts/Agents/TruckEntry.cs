using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEntry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<TruckFlock>().enabled = false;
        if (LevelCreator.Instance.Enemies.Count >= 5)
        {
            print("bleh");
            Destroy(gameObject);
        }
        else
        {
            LevelCreator.Instance.Enemies.Add(gameObject);
            print("yay");
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(gameObject.transform.position, Camera.main.transform.position));
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < 500)
        {
            EnterCanyon();
            enabled = false;
        }
    }

    void EnterCanyon()
    {
        print("vroom vroom x" + LevelCreator.Instance.Enemies.Count);
    }
}
