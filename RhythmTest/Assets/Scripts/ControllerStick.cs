using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerStick : MonoBehaviour
{

    public int id;
    public PlayerControlController playerControl;
    public PlayerControlControllerTutorial playerControlTutorial;

    
    private SteamVR_TrackedObject trackedObj;
    private bool isButtonPressed = false;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start() {
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
            if ((playerControl != null && playerControl.isAttackMode) || (playerControlTutorial != null && playerControlTutorial.isAttackMode))
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

    public void triggerVibration() {
        Controller.TriggerHapticPulse(2000);
    }

}