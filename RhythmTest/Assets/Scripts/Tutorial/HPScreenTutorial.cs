using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScreenTutorial : MonoBehaviour {

    public PlayerTutorial player;
    //public Enemy enemy;
    public RhythmTutorial rhythm;
    private int playerHP;
    private int enemyHP;
    private int comboCount;
    private TextMesh thisText;
    private string currCombo;


    // Use this for initialization
    void Start() {
        EventManager.StartListening("ReducePlayerHP", ReducePlayerHP);
        EventManager.StartListening("ReduceEnemyHP", ReduceEnemyHP);
        EventManager.StartListening("ClearComboText", ClearComboText);
        EventManager.StartListening("UpdateCounter", UpdateCounter);
        thisText = this.GetComponent<TextMesh>();
        playerHP = player.GetComponent<PlayerTutorial>().playerHealth;
        //enemyHP = enemy.GetComponent<Enemy>().health;
        comboCount = rhythm.GetComponent<RhythmTutorial>().playerComboCount;
        updateText();
    }

    private void ReducePlayerHP() {
        if (playerHP > 0) {
            playerHP--;
        }
        updateText();
    }

    private void ReduceEnemyHP() {
        if (enemyHP > 0) {
            //enemyHP = enemy.GetComponent<Enemy>().health;
        }
        updateText();
    }

    private void UpdateCounter() {
        comboCount = rhythm.GetComponent<RhythmTutorial>().playerComboCount;
        updateText();
    }

    private void updateText() {
        thisText.text = "Your HP: " + playerHP + "\nEnemy HP: " + enemyHP + "\nCounter: " + comboCount + "\nCombo:" + currCombo;
    }

    public void updateCombo(Queue<string> queue) {
        currCombo = "";

        foreach (string key in queue) {
            currCombo = currCombo + " " + key;
        }

        updateText();
    }

    private void ClearComboText() {
        currCombo = "";
        updateText();
    }
}
