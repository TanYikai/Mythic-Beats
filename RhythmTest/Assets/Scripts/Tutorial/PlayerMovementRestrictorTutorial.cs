using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRestrictorTutorial : MonoBehaviour {
    const float epsilon = 0.1f;

    public float leftBoundary; // for x
    public float rightBoundary; // for x
    public float frontBoundary; // for z
    public float backBoundary; // for z

    public PlayerMovementRestrictorTutorial() {
        leftBoundary = -1 - epsilon;
        rightBoundary = 1 + epsilon;
        backBoundary = -1 - epsilon;
        frontBoundary = 1 + epsilon;
    }

    public bool checkValidPosition(Vector3 pos) {
        if (pos.x >= leftBoundary && pos.x <= rightBoundary && pos.z >= backBoundary && pos.z <= frontBoundary) {
            return true;
        }
        return false;
    }

}
