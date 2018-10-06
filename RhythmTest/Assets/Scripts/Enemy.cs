using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

    private Rhythm rhythmController;
    private StateController controller;
    private EnemyBoundaryChecker boundaryChecker;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        rhythmController.onBeat += doEnemyAction;

        controller = this.gameObject.GetComponent<StateController>();

        boundaryChecker = new EnemyBoundaryChecker();
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

    public void doMovement(int movementNumber) {
        Vector3 newPosition;
        switch (movementNumber) {
            //left
            case 0: 
                newPosition = transform.position + new Vector3(-1, 0, 0);
                if (checkValidMovement(newPosition)) {
                    StartCoroutine(locationTransition(transform.position, newPosition));
                }
                break;
            //right
            case 1:
                newPosition = transform.position + new Vector3(1, 0, 0);
                if (checkValidMovement(newPosition)) {
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

    private bool checkValidMovement(Vector3 pos) {
        return boundaryChecker.checkValidMovement(pos);
    }

    public bool checkValidGeneralPosition(Vector3 pos) {
        return boundaryChecker.checkValidGeneralPosition(pos);
    }

}
