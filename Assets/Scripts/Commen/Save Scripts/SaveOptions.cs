using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptions : MonoBehaviour
{
    public OptionsData opd;
    public bool save;
    public bool load;

    void Start()
    {

    }

    void OnValidate()
    {
        opd.SetData();
    }

    void Update()
    {
        if(save)
        {
            Options.Save();
            save = false;
        }

        if(load)
        {
            Options.Load();
            load = false;
            opd.GetData();
        }
    }
}