using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

    private Rhythm rhythmController;
    private StateController controller;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        rhythmController.onBeat += doEnemyAction;

        controller = this.gameObject.GetComponent<StateController>();
    }

    private void doEnemyAction() {
        controller.UpdateState();
    }

    public void takeDamage() {
        Debug.Log("damage taken");
        health--;
        checkAndDestroyIfDead();
    }

    private void checkAndDestroyIfDead() {
        if (health <= 0) {
            Debug.Log("dead");
            Destroy(this.gameObject);
        }
    }

    private void doMovement() {
        Vector3 newPosition;
        int movementNumber = Random.Range(0, 4);
        switch (movementNumber) {
            case 0:
                newPosition = transform.position + new Vector3(0, 0, 2);
                if (checkValidPosition(newPosition)) {
                    StartCoroutine(locationTransition(transform.position, newPosition));
                }
                break;
            case 1:
                newPosition = transform.position + new Vector3(-2, 0, 0);
                if (checkValidPosition(newPosition)) {
                    StartCoroutine(locationTransition(transform.position, newPosition));
                }
                break;
            case 2:
                newPosition = transform.position + new Vector3(0, 0, -2);
                if (checkValidPosition(newPosition)) {
                    StartCoroutine(locationTransition(transform.position, newPosition));
                }
                break;
            case 3:
                newPosition = transform.position + new Vector3(2, 0, 0);
                if (checkValidPosition(newPosition)) {
                    StartCoroutine(locationTransition(transform.position, newPosition));
                }
                break;
        }
    }

    IEnumerator locationTransition(Vector3 startPosition, Vector3 endPosition) {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.1f;
        while (currentAnimationTime < totalAnimationTime) {
            currentAnimationTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private bool checkValidPosition(Vector3 position) {
        if (position.x < -4 || position.x > 4 || position.z < -4 || position.z > 4) {
            return false;
        }
        return true;
    }
}
