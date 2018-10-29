using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precollider : MonoBehaviour {

    private ControllerDrum drum;

	// Use this for initialization
	void Start () {
        drum = this.GetComponentInParent<ControllerDrum>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("DrumStick")) {
            drum.canHit = true;
        }
    }
}
