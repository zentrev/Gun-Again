using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class LogIndexValues : MonoBehaviour
{
    [SerializeField] List<IndexInput> Inputs = new List<IndexInput>();

    void Update()
    {
        //ClearConsole();

        foreach (IndexInput input in Inputs)
        {
            print(input.gameObject.name + " Curl: " + input.ThumbCurl + ", " + input.IndexCurl + ", " + input.MiddleCurl + ", " + input.RingCurl + ", " + input.PinkyCurl);
            print(input.gameObject.name + " Splay: " + input.ThumbIndexSplay + ", " + input.IndexMiddleSplay + ", " + input.MiddleRingSplay + ", " + input.RingPinkySplay);
            print(input.gameObject.name + " Trigger: " + input.TriggerPull + ", " + input.TriggerClick + ", " + input.TriggerTouch);
            print(input.gameObject.name + " TrackPad: " + input.TrackPad.x + ":" + input.TrackPad.y + ", " + input.TrackPadClick + ", " + input.TrackPadTouch);
            print(input.gameObject.name + " ThumbStick: " + input.ThumbStick.x + ":" + input.ThumbStick.y + ", " + input.ThumbStickClick + ", " + input.ThumbStickTouch);
            print(input.gameObject.name + " A Button: " + input.AButton + ", " + input.AButtonTouch);
            print(input.gameObject.name + " B Button: " + input.BButton + ", " + input.BButtonTouch);
            print(input.gameObject.name + " Squeeze: " + input.Squeeze); 
            print(input.gameObject.name + " Grip: " + input.Grip); 
            print(input.gameObject.name + " Pinch: " + input.Pinch); 
        }


    }

    //void ClearConsole()
    //{
    //    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    //    var type = assembly.GetType("UnityEditor.LogEntries");
    //    var method = type.GetMethod("Clear");
    //    method.Invoke(new object(), null);
    //}
}
