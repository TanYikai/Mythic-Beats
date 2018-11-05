using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerStick : MonoBehaviour
{

    public int id;
    public PlayerControlController playerControl;
    public PlayerControlControllerTutorial playerControlTutorial;

    private bool downPressed = false;
    private bool upPressed = false;

    
    private SteamVR_TrackedObject trackedObj;


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
        if (Controller.GetHairTriggerDown()) {
            if (downPressed)
                return;

            downPressed = true;
            upPressed = false;

            if ((playerControl != null && !playerControl.isAttackMode) || (playerControlTutorial != null && !playerControlTutorial.isAttackMode))
                EventManager.TriggerEvent("ToggleDrumToAttack");
        }
        else if (Controller.GetHairTriggerUp()) {
            if (upPressed)
                return;

            downPressed = false;
            upPressed = true;

            if ((playerControl != null && playerControl.isAttackMode) || (playerControlTutorial != null && playerControlTutorial.isAttackMode))
                EventManager.TriggerEvent("ToggleDrumToMovement");
        }
    }

    public void triggerVibration() {
        Controller.TriggerHapticPulse(2000);
    }

}