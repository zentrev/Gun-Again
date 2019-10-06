using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveExample : MonoBehaviour
{
    [Header("Try moving me")]
    public int saveNumber = 1;
    public int level = 3;
    public float love = 0.044f;

    public bool save = false;
    public bool load = false;
    public bool deleteData = false;

    private void Update()
    {
        SaveSystem.CurrentSave = saveNumber;

        if (save)
        {
            save = false;
            SaveSystem.SaveExample(this);
        }

        if (load)
        {
            load = false;
            ExampleData data = SaveSystem.LoadExample();
            if (data != null)
            {
                data.SetData(this);
            }
            else
            {
                SaveSystem.SaveExample(this);
            }
        }

        if(deleteData)
        {
            deleteData = false;
            SaveSystem.DeleteExample();
        }
    }

    private void Awake()
    {
        load = true;
    }
}

[System.Serializable]
public class ExampleData
{
    public int level;
    public float love;
    public float[] position;

    public ExampleData(SaveExample ex)
    {
        level = ex.level;
        love = ex.love;

        position = new float[3];
        position[0] = ex.transform.position.x;
        position[1] = ex.transform.position.y;
        position[2] = ex.transform.position.z;
    }

    public void SetData(SaveExample ex)
    {
        ex.level = level;
        ex.love = love;
        Vector3 pos = new Vector3(position[0], position[1], position[2]);
        ex.transform.position = pos;
    }
}