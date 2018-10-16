using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerStick : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private bool attackMode = false;
    private bool isButtonPressed = false;
    private UnityAction vibrationListener;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start() {
        EventManager.StartListening("TriggerVibration", vibrationListener);
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        vibrationListener = new UnityAction(TriggerVibration);

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

    private void TriggerVibration() {
        Controller.TriggerHapticPulse(500);
    }
}