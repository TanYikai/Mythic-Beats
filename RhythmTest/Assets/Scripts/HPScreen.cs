using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScreen : MonoBehaviour {

    public Player player;
    public Enemy enemy;
    public float playerHP;
    public float enemyHP;
    private TextMesh thisText;
    private string currCombo;


	// Use this for initialization
	void Start () {
        EventManager.StartListening("ReducePlayerHP", ReducePlayerHP);
        EventManager.StartListening("ReduceEnemyHP", ReduceEnemyHP);
        EventManager.StartListening("ClearComboText", ClearComboText);
        thisText = this.GetComponent<TextMesh>();
        playerHP = player.GetComponent<Player>().playerHealth;
        enemyHP = enemy.GetComponent<Enemy>().health;
        updateText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ReducePlayerHP() {
        if (playerHP > 0) {
            playerHP--;
        }
        updateText();
    }

    private void ReduceEnemyHP() {
        if (enemyHP > 0) {
            enemyHP = enemy.GetComponent<Enemy>().health;
        }
        updateText();
    }

    private void updateText() {
        thisText.text = "Your HP: " + playerHP + "\nEnemy HP: " + enemyHP + "\nCombo:" + currCombo;
    }

    public void updateCombo(Queue<string> queue) {
        currCombo = "";

        foreach (string key in queue) {
            currCombo = currCombo + " " + key ;
        }

        updateText();
    }

    private void ClearComboText() {
        currCombo = "";
        updateText();
    }
}
