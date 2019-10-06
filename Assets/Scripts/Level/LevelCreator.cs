using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : Singleton<LevelCreator>
{
    [SerializeField] GameObject[] canyonBits;

    public GameObject[] CanyonBits { get => canyonBits; set => canyonBits = value; }
    public List<GameObject> Enemies { get => enemies; set => enemies = value; }

    private List<GameObject> enemies = null;


    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
