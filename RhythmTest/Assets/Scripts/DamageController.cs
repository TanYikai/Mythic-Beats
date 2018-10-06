using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public static DamageController instance = null;

    private GameObject player;
    private GameObject enemy;
    private Player playerScript;
    private Enemy enemyScript;

    void Awake() {
        if (instance == null) {
            instance = this;
            instance.Init();
        }
    }

    private void Init() {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        playerScript = player.GetComponent<Player>();
        enemyScript = enemy.GetComponentInChildren<Enemy>();
    }

    public void checkAndDoDamageToPlayer(int i, int j) {
        if (Mathf.RoundToInt(player.transform.position.z) == i && Mathf.RoundToInt(player.transform.position.x) == j) {
            doDamageToPlayer();
        }
    }

    public void checkAndDoDamageToEnemy(int i, int j) {
        if (Mathf.RoundToInt(enemy.transform.position.z) == i && Mathf.RoundToInt(enemy.transform.position.x) == j) {
            doDamageToEnemy();
        }
    }

    private void doDamageToPlayer() {
        playerScript.takeDamage();
    }

    private void doDamageToEnemy() {
        enemyScript.takeDamage();
    }
}
