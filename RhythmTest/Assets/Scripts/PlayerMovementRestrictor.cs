using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRestrictor : MonoBehaviour {

    public int leftBoundary; // for x
    public int rightBoundary; // for x
    public int frontBoundary; // for z
    public int backBoundary; // for z
    private GameObject enemy;

    public PlayerMovementRestrictor(GameObject enemy) {
        this.enemy = enemy;
        leftBoundary = -4;
        rightBoundary = 4;
        backBoundary = -2;
        frontBoundary = (int) (enemy.transform.position.z - 1);
    }

    public bool checkValidPosition(Vector3 position) {
        if (position.x < leftBoundary || position.x > rightBoundary || position.z < backBoundary || position.z > frontBoundary) {
            return false;
        }
        return true;
    }
}
