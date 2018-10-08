using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

    private Rhythm rhythmController;
    private StateController controller;
    private EnemyBoundaryChecker boundaryChecker;
    private GameObject enemy;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        rhythmController.onEnemyBeat += doEnemyAction;

        controller = this.gameObject.GetComponent<StateController>();

        boundaryChecker = new EnemyBoundaryChecker();

        enemy = this.gameObject.transform.parent.gameObject;
    }

    private void doEnemyAction() {
        controller.UpdateState();
    }

    public void takeDamage(int dmg) {
        Debug.Log("enemy damage taken");
        if (health - dmg >= 0) {
            health -= dmg;
        }
        else {
            health = 0;
        }
        EventManager.TriggerEvent("ReduceEnemyHP");
        checkAndDestroyIfDead();
    }

    private void checkAndDestroyIfDead() {
        if (health <= 0) {
            Debug.Log("dead");
            Destroy(this.gameObject);
        }
    }

    public void doMovement() {
        Vector3 newPosition;
        int movementNumber = Random.Range(0, 2);

        switch (movementNumber) {
            //left but right if at left boundary
            case 0: 
                newPosition = enemy.transform.position + new Vector3(-1, 0, 0);
                if (checkValidMovement(newPosition)) {
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }
                //temporary code
                else {
                    newPosition = enemy.transform.position + new Vector3(1, 0, 0);
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }

                break;
            //right but left if at right boundary
            case 1:
                newPosition = enemy.transform.position + new Vector3(1, 0, 0);
                if (checkValidMovement(newPosition)) {
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }
                //temporary code
                else {
                    newPosition = enemy.transform.position + new Vector3(-1, 0, 0);
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }
                break;
        }
    }

    IEnumerator locationTransition(Vector3 startPosition, Vector3 endPosition) {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.05f;
        while (currentAnimationTime < totalAnimationTime) {
            currentAnimationTime += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(startPosition, endPosition, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private bool checkValidMovement(Vector3 pos) {
        return boundaryChecker.checkValidMovement(pos);
    }

    public bool checkValidGeneralPosition(Vector3 pos) {
        return boundaryChecker.checkValidGeneralPosition(pos);
    }

}
