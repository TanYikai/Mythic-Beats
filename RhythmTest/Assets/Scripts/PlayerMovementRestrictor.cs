using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRestrictor : MonoBehaviour {

    const float epsilon = 0.1f;

    public float leftBoundary; // for x
    public float rightBoundary; // for x
    public float frontBoundary; // for z
    public float backBoundary; // for z

    public PlayerMovementRestrictor() {
        leftBoundary = -4 - epsilon;
        rightBoundary = 4 + epsilon;
        backBoundary = -2 - epsilon;
        frontBoundary = 2 + epsilon;
    }

    public bool checkValidPosition(Vector3 pos) {
        if (pos.x >= leftBoundary && pos.x <= rightBoundary && pos.z >= backBoundary && pos.z <= frontBoundary) {
            return true;
        }
        return false;
    }
}
