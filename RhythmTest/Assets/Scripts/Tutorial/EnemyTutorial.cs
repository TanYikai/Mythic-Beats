using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour {

    public int health;

    private bool isDead;
    private AudioSource enemyDamagedSound;
    public GameObject floatingText;

    // Use this for initialization
    void Start() {
        isDead = false;
        enemyDamagedSound = GetComponent<AudioSource>();
    }

    public void takeDamage(int dmg) {
        handleDamageTaken(dmg);
    }

    private void handleDamageTaken(int dmg) {
        if (health - dmg >= 0) {
            health -= dmg;
            enemyDamagedSound.Play();

            if (floatingText) {
                showFloatingText(dmg);
            }
        }
        else {
            health = 0;
        }
        EventManager.TriggerEvent("UpdateEnemyHealth");
        checkAndSetIfDead();
    }

    private void showFloatingText(int dmg) {
        var text = Instantiate(floatingText, transform.position + new Vector3(0, 2, 0), Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = dmg.ToString();
    }

    private void checkAndSetIfDead() {
        if (health <= 0) {
            isDead = true;
        }
    }

    public bool getIsDead() {
        return isDead;
    }
}
