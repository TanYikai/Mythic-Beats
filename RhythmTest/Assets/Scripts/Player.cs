using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    enum ComboDirection {
        up,
        down,
        left,
        right
    };

    public int lengthOfCombo;
    public int playerHealth;

    private Rhythm rhythmController;

    private string comboStack;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        comboStack = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1")) {
            if (rhythmController.IsTimeForPlayerAction) {
                doCombo();
            }
        }
        else {
            if (rhythmController.IsTimeForPlayerAction) {
                doMovement();
            }
        }
	}

    private bool checkValidPosition(Vector3 position) {
        if (position.x < -4 || position.x > 4 || position.z < -4 || position.z > 4) {
            return false;
        }
        return true;
    }

    private void doMovement() {
        Vector3 newPosition;

        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, 2);
            if (checkValidPosition(newPosition)) {
                transform.position = newPosition;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(-2, 0, 0);
            if (checkValidPosition(newPosition)) {
                transform.position = newPosition;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, -2);
            if (checkValidPosition(newPosition)) {
                transform.position = newPosition;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(2, 0, 0);
            if (checkValidPosition(newPosition)) {
                transform.position = newPosition;
            }
            return;
        }
    }

    private void doCombo() {
        //if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) { // for easier testing
        if (Input.GetKeyDown(KeyCode.Space)) { 
            rhythmController.IsTimeForPlayerAction = false;
            executeCombo();
            return;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            checkAndUpdateIfComboIsTooLong();
            comboStack = comboStack + (int)ComboDirection.up;
            Debug.Log(comboStack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            checkAndUpdateIfComboIsTooLong();
            comboStack = comboStack + (int)ComboDirection.left;
            Debug.Log(comboStack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            checkAndUpdateIfComboIsTooLong();
            comboStack = comboStack + (int)ComboDirection.down;
            Debug.Log(comboStack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            checkAndUpdateIfComboIsTooLong();
            comboStack = comboStack + (int)ComboDirection.right;
            Debug.Log(comboStack);
            return;
        }

    }

    private void executeCombo() {
        Debug.Log("combo executed");
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

}
