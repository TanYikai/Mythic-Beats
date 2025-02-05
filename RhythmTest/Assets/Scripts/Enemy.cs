﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

    private Rhythm rhythmController;
    private StateController controller;
    private EnemyBoundaryChecker boundaryChecker;
    private GameObject enemy;
    private GameObject player;
    private bool isBerserk;
    private bool isDead;
    private int initialHealth;
    private SceneChanger sceneChanger;

    public Animator anim;
    private AudioSource enemyDamagedSound;
    public GameObject floatingText;

    // Use this for initialization
    void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        rhythmController.onEnemyBeat += doEnemyAction;

        controller = this.gameObject.GetComponent<StateController>();

        boundaryChecker = new EnemyBoundaryChecker();

        enemy = this.gameObject.transform.parent.gameObject;
        player = GameObject.Find("Player").gameObject;

        isBerserk = false;
        isDead = false;
        initialHealth = health;

        anim.SetBool("B_Died", false);

        enemyDamagedSound = GetComponent<AudioSource>();

        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    private void doEnemyAction() {
        anim.SetBool("B_Left", false);
        anim.SetBool("B_Right", false);
        anim.SetBool("B_Hit", false);
        anim.SetBool("B_Atk", false);
        if (controller != null) {
            controller.UpdateState();
        }
        if (isDead) {
            Grid.instance.resetAllGridMaterial();
        }
    }

    public void takeDamage(int dmg) {
        if (!isDead) {
            handleDamageTaken(dmg);
        }
    }

    private void handleDamageTaken(int dmg) {
        dmg = (int) ((Mathf.Floor(Mathf.Pow(2,rhythmController.playerComboCount/2f))*3f + dmg*7f)/10f);
        enemyDamagedSound.Play();

        Debug.Log("enemy damage taken");
        anim.SetBool("B_Hit", true);
        if (health - dmg >= 0) {
            health -= dmg;

            if (floatingText) {
                showFloatingText(dmg);
            }
        }
        else {
            anim.SetBool("B_Died", true);
            health = 0;
        }
        EventManager.TriggerEvent("UpdateEnemyHealth");
        checkAndDestroyIfDead();
        checkAndSetEnemyBerserk();
    }

    private void showFloatingText(int dmg) {
        var text = Instantiate(floatingText, transform.position + new Vector3(0, 3, 0), Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = dmg.ToString();
    }

    private void checkAndSetEnemyBerserk() {
        if (health < initialHealth / 2) {
            isBerserk = true;
        }
    }

    private void checkAndDestroyIfDead() {
        if (health <= 0) {
            Debug.Log("dead");
            //Create a delay here for the death animation
            StartCoroutine(doDeathAnimationAndDestroyObject(4.0f));
        }
    }

    private IEnumerator doDeathAnimationAndDestroyObject(float duration) {
        isDead = true;
        controller = null;
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject.transform.parent.gameObject);
        sceneChanger.waitAndFadeToScene("PlayerWin", 2.0f);
    }

    public bool decideMoveOrCharge() {
        int value = Random.Range(0, 10);
        if (value < 7) {
            return false;
        }
        return true;
    }

    public void doMovementORStayIdle() {
        int rand = Random.Range(0, 3);
        if (rand < 2) {
            stayIdle();
        }
        else {
            doMovement();
        }
    }

    private void stayIdle() {
        // does nothing
    }

    private void doMovement() {
        Vector3 newPosition;
        int movementNumber = Random.Range(0, 2);

        switch (movementNumber) {
            //left but right if at left boundary
            case 0:
                newPosition = enemy.transform.position + new Vector3(-1, 0, 0);
                if (checkValidMovement(newPosition)) {
                    anim.SetBool("B_Right", true);
                    anim.SetBool("B_Left", false);
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }
                //temporary code
                else {
                    anim.SetBool("B_Right", false);
                    anim.SetBool("B_Left", true);
                    newPosition = enemy.transform.position + new Vector3(1, 0, 0);
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }

                break;
            //right but left if at right boundary
            case 1:
                newPosition = enemy.transform.position + new Vector3(1, 0, 0);
                if (checkValidMovement(newPosition)) {
                    anim.SetBool("B_Right", false);
                    anim.SetBool("B_Left", true);
                    StartCoroutine(locationTransition(enemy.transform.position, newPosition));
                }
                //temporary code
                else {
                    newPosition = enemy.transform.position + new Vector3(-1, 0, 0);
                    anim.SetBool("B_Right", true);
                    anim.SetBool("B_Left", false);
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

    public GameObject getPlayer() {
        return player;
    }

    public bool getIsBerserk() {
        return isBerserk;
    }

}
