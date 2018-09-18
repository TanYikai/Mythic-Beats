using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStick : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update() {
        if (Controller.GetHairTrigger())
        {
            EventManager.TriggerEvent("Toggle");
        }
    }
}