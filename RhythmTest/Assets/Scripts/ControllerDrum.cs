using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public KeyCode keyCode;
    private bool comboMode = false;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private Player playerRef;
    private UnityAction toggleOnListener;
    private UnityAction toggleOffListener;

    void Start () {
        EventManager.StartListening("ToggleOn", toggleOnListener);
        EventManager.StartListening("ToggleOff", toggleOffListener);
        playerRef = player.GetComponent<Player>();
    }


    private void Awake()
    {
        toggleOnListener = new UnityAction(ToggleOn);
        toggleOffListener = new UnityAction(ToggleOff);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (comboMode) {
            if (collider.CompareTag("DrumStick"))
            {
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
        comboMode = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void ToggleOff() {
        comboMode = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

}
