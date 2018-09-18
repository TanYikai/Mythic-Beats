using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public KeyCode keyCodeMovement;
    public KeyCode keyCodeAttack;
    private bool movementMode = true;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private Player playerRef;
    private UnityAction toggleListener;

    void Start () {
        EventManager.StartListening("Toggle", toggleListener);
        playerRef = player.GetComponent<Player>();
    }


    private void Awake()
    {
        toggleListener = new UnityAction(ToggleMode);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Colldiing");
        if (movementMode)
        {
            if (collider.CompareTag("DrumStick"))
            {
                Debug.Log("Moving");
                playerRef.ExecuteKey(keyCodeMovement);
            }
        }
        else {
            if (collider.CompareTag("DrumStick"))
            {
                Debug.Log("Attacking");
                playerRef.ExecuteKey(keyCodeAttack);
            }
        }
    }

    private void ToggleMode() {
        movementMode = !movementMode;
        if (movementMode)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

}
