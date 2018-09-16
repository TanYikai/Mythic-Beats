using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    enum ComboDirection {
        up,
        down,
        left,
        right
    };

    public int health;

    private Rhythm rhythmController;
    private string comboStack;
    private int lengthOfCombo = 1;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        rhythmController.onBeat += doEnemyAction;
        comboStack = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void doEnemyAction() {
        int actionNumber = Random.Range(0, 2);
        switch (actionNumber) {
            case 0:
                doMovement();
                break;
            case 1:
                doCombo();
                break;
        }
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
                    transform.position = newPosition;
                }
                break;
            case 1:
                newPosition = transform.position + new Vector3(-2, 0, 0);
                if (checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                break;
            case 2:
                newPosition = transform.position + new Vector3(0, 0, -2);
                if (checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                break;
            case 3:
                newPosition = transform.position + new Vector3(2, 0, 0);
                if (checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                break;
        }
    }

    private void doCombo() {
        if (comboStack.Length > 0) {
            executeCombo();
            return;
        }

        int comboNumber = Random.Range(0, 4);

        switch (comboNumber) {
            case 0:
                checkAndUpdateIfComboIsTooLong();
                comboStack = comboStack + (int)ComboDirection.up;
                break;
            case 1:
                checkAndUpdateIfComboIsTooLong();
                comboStack = comboStack + (int)ComboDirection.left;
                break;
            case 2:
                checkAndUpdateIfComboIsTooLong();
                comboStack = comboStack + (int)ComboDirection.down;
                break;
            case 3:
                checkAndUpdateIfComboIsTooLong();
                comboStack = comboStack + (int)ComboDirection.right;
                break;
        }
    }

    private void executeCombo() {
        switch (comboStack) {
            case "0":
                StartCoroutine(tackleAnimation(new Vector3(0, 0, 1)));
                break;
            case "1":
                StartCoroutine(tackleAnimation(new Vector3(0, 0, -1)));
                break;
            case "2":
                StartCoroutine(tackleAnimation(new Vector3(-1, 0, 0)));
                break;
            case "3":
                StartCoroutine(tackleAnimation(new Vector3(1, 0, 0)));
                break;
        }
        comboStack = "";
    }

    IEnumerator tackleAnimation(Vector3 directionVector) {
        float animationTime = 0;
        int startEndDistance = 2;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + (directionVector * startEndDistance);
        while (animationTime < 0.2) {
            animationTime += Time.deltaTime;
            if (animationTime < 0.1) {
                transform.position = Vector3.Lerp(startPosition, endPosition, animationTime / 0.1f);
            }
            else {
                transform.position = Vector3.Lerp(endPosition, startPosition, animationTime / 0.1f);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void checkAndUpdateIfComboIsTooLong() {
        if (comboStack.Length >= lengthOfCombo) {
            comboStack = "";
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().takeDamage();
        }
    }

    private bool checkValidPosition(Vector3 position) {
        if (position.x < -4 || position.x > 4 || position.z < -4 || position.z > 4) {
            return false;
        }
        return true;
    }
}
