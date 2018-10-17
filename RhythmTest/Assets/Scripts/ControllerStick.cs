using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerStick : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private bool isButtonPressed = false;
    private UnityAction vibrationListener;
    private bool isAttackMode = false;

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
            if (isAttackMode)
            {
                EventManager.TriggerEvent("ToggleDrumToMovement");
            }
            else {
                EventManager.TriggerEvent("ToggleDrumToAttack");
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