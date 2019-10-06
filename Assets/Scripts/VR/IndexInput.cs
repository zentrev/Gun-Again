using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class IndexInput : MonoBehaviour
{
    #region Accessors 

    public SteamVR_Input_Sources Input { get; set; }

    #region Skeleton
    public float[] Curl { get; set; }
    public float ThumbCurl { get; set; }
    public float IndexCurl { get; set; }
    public float MiddleCurl { get; set; }
    public float RingCurl { get; set; }
    public float PinkyCurl { get; set; }
    public float[] Splay { get; set; }
    public float ThumbIndexSplay { get; set; }
    public float IndexMiddleSplay { get; set; }
    public float MiddleRingSplay { get; set; }
    public float RingPinkySplay { get; set; }

    #endregion

    #region Trigger
    public float TriggerPull { get; set; }

    public bool TriggerClick { get; set; }
    public bool TriggerClickDown { get; set; }
    public bool TriggerClickUp { get; set; }

    public bool TriggerTouch { get; set; }
    #endregion

    #region TrackPad
    public Vector2 TrackPad { get; set; }

    public bool TrackPadClick { get; set; }
    public bool TrackPadClickDown { get; set; }
    public bool TrackPadClickUp { get; set; }

    public bool TrackPadTouch { get; set; }
    #endregion
    
    #region ThumbStick
    public Vector2 ThumbStick { get; set; }

    public bool ThumbStickClick { get; set; }
    public bool ThumbStickClickDown { get; set; }
    public bool ThumbStickClickUp { get; set; }

    public bool ThumbStickTouch { get; set; }
    #endregion

    #region Buttons
    public bool AButton { get; set; }
    public bool AButtonDown { get; set; }
    public bool AButtonUp { get; set; }
    public bool AButtonTouch { get; set; }

    public bool BButton { get; set; }
    public bool BButtonDown { get; set; }
    public bool BButtonUp { get; set; }
    public bool BButtonTouch { get; set; }
    #endregion

    #region Feedback
    public SteamVR_Action_Vibration Heptic { get; set; }
    #endregion

    #region Grip
    public float Squeeze { get; set; }
    public bool Grip { get; set; }
    public bool Pinch { get; set; }
    #endregion

    #endregion

    #region Actions

    [Header("Input")]
    [SerializeField] SteamVR_Input_Sources InputDevice;
    [SerializeField] SteamVR_Action_Skeleton SkeletonAction = null;

    [Header("Trigger")]
    [SerializeField] SteamVR_Action_Single TriggerPullAction = null;
    [SerializeField] SteamVR_Action_Boolean TriggerClickAction = null;
    [SerializeField] SteamVR_Action_Boolean TriggerTouchAction = null;

    [Header("TrackPad")]
    [SerializeField] SteamVR_Action_Vector2 TrackPadAction = null;
    [SerializeField] SteamVR_Action_Boolean TrackPadClickAction = null;
    [SerializeField] SteamVR_Action_Boolean TrackPadTouchAction = null;

    [Header("ThumbStick")]
    [SerializeField] SteamVR_Action_Vector2 ThumbStickAction = null;
    [SerializeField] SteamVR_Action_Boolean ThumbStickClickAction = null;
    [SerializeField] SteamVR_Action_Boolean ThumbStickTouchAction = null;

    [Header("Buttons")]
    [SerializeField] SteamVR_Action_Boolean AButtonAction = null;
    [SerializeField] SteamVR_Action_Boolean AButtonTouchAction = null;
    [SerializeField] SteamVR_Action_Boolean BButtonAction = null;
    [SerializeField] SteamVR_Action_Boolean BButtonTouchAction = null;

    [Header("Grip")]
    [SerializeField] SteamVR_Action_Single SqueezeAction = null;
    [SerializeField] SteamVR_Action_Boolean GripAction = null;
    [SerializeField] SteamVR_Action_Boolean PinchAction = null;

    [Header("Feedback")]
    [SerializeField] SteamVR_Action_Vibration HepticAction = null;

    #endregion

    #region Private Varibles
    List<Action> Inputs = new List<Action>();
    #endregion

    void Start()
    {
        Input = InputDevice;

        // Skeleton
        if (SkeletonAction != null) Inputs.Add(GetSkeleton);

        // Trigger
        if (TriggerPullAction != null) Inputs.Add(GetTriggerPull);
        if (TriggerClickAction != null) Inputs.Add(GetTriggerClick);
        if (TriggerTouchAction != null) Inputs.Add(GetTriggerTouch);

        // TrackPad
        if (TrackPadAction != null) Inputs.Add(GetTrackPad);
        if (TrackPadClickAction != null) Inputs.Add(GetTrackPadClick);
        if (TrackPadTouchAction != null) Inputs.Add(GetTrackPadTouch);
        
        // ThumbStick
        if (ThumbStickAction != null) Inputs.Add(GetThumbStick);
        if (ThumbStickClickAction != null) Inputs.Add(GetThumbStickClick);
        if (ThumbStickTouchAction != null) Inputs.Add(GetThumbStickTouch);

        // Buttons
        if (AButtonAction != null) Inputs.Add(GetAButton);
        if (AButtonTouchAction != null) Inputs.Add(GetAButtonTouch);
        if (BButtonAction != null) Inputs.Add(GetBButton);
        if (BButtonTouchAction != null) Inputs.Add(GetBButtonTouch);

        // Grip
        if (SqueezeAction != null) Inputs.Add(GetSqueeze);
        if (GripAction != null) Inputs.Add(GetGrip);
        if (PinchAction != null) Inputs.Add(GetPinch);

    }

    void Update()
    {
        foreach(Action input in Inputs) { input(); }
    }

    #region Data Setting

    #region Skeleton
    private void GetSkeleton()
    {
        Curl = SkeletonAction.fingerCurls;
        IndexCurl = SkeletonAction.indexCurl;
        MiddleCurl = SkeletonAction.middleCurl;
        RingCurl = SkeletonAction.ringCurl;
        PinkyCurl = SkeletonAction.pinkyCurl;
        ThumbCurl = SkeletonAction.thumbCurl;

        Splay = SkeletonAction.fingerSplays;
        ThumbIndexSplay = SkeletonAction.thumbIndexSplay;
        IndexMiddleSplay = SkeletonAction.indexMiddleSplay;
        MiddleRingSplay = SkeletonAction.middleRingSplay;
        RingPinkySplay = SkeletonAction.ringPinkySplay;
    }
    #endregion

    #region Trigger
    private void GetTriggerPull()
    {
        TriggerPull = TriggerPullAction.GetAxis(InputDevice);
    }

    private void GetTriggerClick()
    {
        TriggerClick = TriggerClickAction.GetState(InputDevice);
        TriggerClickDown = TriggerClickAction.GetState(InputDevice);
        TriggerClickUp = TriggerClickAction.GetState(InputDevice);
    }

    private void GetTriggerTouch()
    {
        TriggerTouch = TriggerTouchAction.GetState(InputDevice);
    }
    #endregion

    #region TrackPad
    private void GetTrackPad()
    {
        TrackPad = TrackPadAction.GetAxis(InputDevice);
    }

    private void GetTrackPadClick()
    {
        TrackPadClick = TrackPadClickAction.GetState(InputDevice);
        TrackPadClickDown = TrackPadClickAction.GetLastStateDown(InputDevice);
        TrackPadClickUp = TrackPadClickAction.GetStateUp(InputDevice);
    }

    private void GetTrackPadTouch()
    {
        TrackPadTouch = TrackPadTouchAction.GetState(InputDevice);
    }
    #endregion

    #region ThumbStick
    private void GetThumbStick()
    {
        ThumbStick = ThumbStickAction.GetAxis(InputDevice);
    }

    private void GetThumbStickClick()
    {
        ThumbStickClick = ThumbStickClickAction.GetState(InputDevice);
        ThumbStickClickDown = ThumbStickClickAction.GetLastStateDown(InputDevice);
        ThumbStickClickUp = ThumbStickClickAction.GetStateUp(InputDevice);
    }

    private void GetThumbStickTouch()
    {
        ThumbStickTouch = ThumbStickTouchAction.GetState(InputDevice);
    }
    #endregion

    #region Buttons
    private void GetAButton()
    {
        AButton = AButtonAction.GetState(InputDevice);
        AButtonDown = AButtonAction.GetLastStateDown(InputDevice);
        AButtonUp = AButtonAction.GetStateUp(InputDevice);
    }

    private void GetAButtonTouch()
    {
        AButtonTouch = AButtonTouchAction.GetState(InputDevice);
    }

    private void GetBButton()
    {
        BButton = BButtonAction.GetState(InputDevice);
        BButtonDown = BButtonAction.GetLastStateDown(InputDevice);
        BButtonUp = BButtonAction.GetStateUp(InputDevice);
    }

    private void GetBButtonTouch()
    {
        BButtonTouch = BButtonTouchAction.GetState(InputDevice);
    }
    #endregion

    #region Grip
    private void GetSqueeze()
    {
        Squeeze = SqueezeAction.GetAxis(InputDevice);
    }

    private void GetGrip()
    {
        Grip = GripAction.GetState(InputDevice);
    }

    private void GetPinch()
    {
        Pinch = PinchAction.GetState(InputDevice);
    }

    #endregion

    #endregion

}
