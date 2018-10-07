using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundaryChecker : MonoBehaviour {

    const float epsilon = 0.1f;

    public float leftMovementBoundary; 
    public float rightMovementBoundary;

    public float leftGeneralBoundary;
    public float rightGeneralBoundary;
    public float frontGeneralBoundary;
    public float backGeneralBoundary;

    public EnemyBoundaryChecker() {
        leftMovementBoundary = -3 - epsilon;
        rightMovementBoundary = 3 + epsilon;

        leftGeneralBoundary = -4 - epsilon;
        rightGeneralBoundary = 4 + epsilon;
        frontGeneralBoundary = 2 + epsilon;
        backGeneralBoundary = -2 - epsilon;
    }

    public bool checkValidMovement(Vector3 pos) {
        if (pos.x >= leftMovementBoundary && pos.x <= rightMovementBoundary) {
            return true;
        }
        return false;
    }

    public bool checkValidGeneralPosition(Vector3 pos) {
        if (pos.x >= leftGeneralBoundary && pos.x <= rightGeneralBoundary && pos.z >= backGeneralBoundary && pos.z <= frontGeneralBoundary) {
            return true;
        }
        return false;
    }
}
