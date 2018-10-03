using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public GameObject rhythm;
    public KeyCode keyCode;
    private bool attackMode = false;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private Player playerRef;
    private Rhythm rhythmRef;
    private UnityAction toggleOnListener;
    private UnityAction toggleOffListener;

    void Start () {
        EventManager.StartListening("ToggleOn", toggleOnListener);
        EventManager.StartListening("ToggleOff", toggleOffListener);
        playerRef = player.GetComponent<Player>();
        rhythmRef = rhythm.GetComponent<Rhythm>();
    }


    private void Awake() {
        toggleOnListener = new UnityAction(ToggleOn);
        toggleOffListener = new UnityAction(ToggleOff);
    }

    private void Update() {
        if (rhythmRef.IsNextWindowDisabled || rhythmRef.IsSpecialOccurring) {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (attackMode) {
            if (collider.CompareTag("DrumStick")) {
                Debug.Log("Sending " + keyCode + " (combo)");
                playerRef.ExecuteKey(keyCode, true);
            }
        }
        else {
            if (collider.CompareTag("DrumStick")) {
                Debug.Log("Sending " + keyCode + " (movement)");
                playerRef.ExecuteKey(keyCode, false);
            }
        }
    }

    private void ToggleOn() {
        attackMode = true;
        Debug.Log("attackMode ON");
    }

    private void ToggleOff() {
        attackMode = false;
        Debug.Log("attackMode OFF");
    }

}
