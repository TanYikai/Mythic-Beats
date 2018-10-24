using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public static DamageController instance = null;

    private GameObject player;
    private GameObject enemy;
    private GameObject enemyMesh;
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
        enemyMesh = GameObject.Find("EnemyMesh");
        playerScript = player.GetComponent<Player>();
        enemyScript = enemy.GetComponentInChildren<Enemy>();
    }

    public void checkAndDoDamageToPlayer(int i, int j) {
        if (Mathf.RoundToInt(player.transform.position.z) == i && Mathf.RoundToInt(player.transform.position.x) == j) {
            doDamageToPlayer();
        }
    }

    public bool checkIfEnemyIsInRange(int i, int j) {
        //Debug.Log("Enemy location: " + Mathf.RoundToInt(enemyMesh.transform.position.z) + " " + Mathf.RoundToInt(enemyMesh.transform.position.x));
        //Debug.Log("Enemy check: " + i + " " + j);
        if (Mathf.RoundToInt(enemyMesh.transform.position.z) == i && Mathf.RoundToInt(enemyMesh.transform.position.x) == j) {
            return true;
        } else if (Mathf.RoundToInt(enemyMesh.transform.position.z) == i && Mathf.RoundToInt(enemyMesh.transform.position.x-1) == j) {
            return true;
        }
        else if (Mathf.RoundToInt(enemyMesh.transform.position.z) == i && Mathf.RoundToInt(enemyMesh.transform.position.x+1) == j) {
            return true;
        }

        return false;
    }


    public void doDamageToPlayer() {
        playerScript.takeDamage();
    }

    public void doDamageToEnemy(int dmg) {
        if (enemyScript != null) {
            enemyScript.takeDamage(dmg);
        }
    }
}
