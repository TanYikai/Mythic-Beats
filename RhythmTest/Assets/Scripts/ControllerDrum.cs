using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public KeyCode keyCode;


    private Player playerRef;

	// Use this for initialization
	void Start () {
        playerRef = player.GetComponent<Player>();
        Debug.Log("Done");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Colldiing");
        if (collider.CompareTag("DrumStick")) {
            Debug.Log("Sneding");
            playerRef.ExecuteKey(keyCode);
        }
    }


}
