using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    // this class is not used at the moment

    public static Combo Instance;

	// Use this for initialization
	void Start () {
        if (Instance != null) {
            return;
        }
        else {
            Instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool attackFront(Vector3 attackerPosition, Vector3 targetPosition) {
        if (targetPosition == attackerPosition + new Vector3(0, 0, 2)) {
            return true;
        }
        return false;
    }

    public bool attackBack(Vector3 attackerPosition, Vector3 targetPosition) {
        if (targetPosition == attackerPosition + new Vector3(0, 0, -2)) {
            return true;
        }
        return false;
    }

    public bool attackLeft(Vector3 attackerPosition, Vector3 targetPosition) {
        if (targetPosition == attackerPosition + new Vector3(-2, 0, 0)) {
            return true;
        }
        return false;
    }

    public bool attackRight(Vector3 attackerPosition, Vector3 targetPosition) {
        if (targetPosition == attackerPosition + new Vector3(2, 0, 0)) {
            return true;
        }
        return false;
    }

}
