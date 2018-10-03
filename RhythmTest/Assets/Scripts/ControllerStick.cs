using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStick : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private bool attackMode = false;
    private bool isButtonPressed = false;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update() {
        if (Controller.GetHairTriggerDown())
        {
            if (isButtonPressed)
            {
                return;
            }

            isButtonPressed = true;
            if (!attackMode)
            {
                EventManager.TriggerEvent("ToggleOn");
                attackMode = true;
            }
            else {
                EventManager.TriggerEvent("ToggleOff");
                attackMode = false;
            }
        }
        else if (Controller.GetHairTriggerUp()) {
            isButtonPressed = false;
        }
    }
}