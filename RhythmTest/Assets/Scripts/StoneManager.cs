using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneManager : MonoBehaviour {

    public Player playerRef;
    public Enemy enemyRef;

    public GameObject[] comboStones;
    public GameObject[] healthStones;

    public List<Material[]> comboStonesMat;
    public List<Material[]> healthStonesMat;

    private UnityAction updatePlayerHealthListener;
    private UnityAction updateEnemyHealthListener;
    private UnityAction updateComboStatusListener;

    // Use this for initialization
    void Start () {
        comboStonesMat = new List<Material[]>();
        healthStonesMat = new List<Material[]>();

        foreach (GameObject stone in comboStones) {
            comboStonesMat.Add(stone.GetComponent<Renderer>().materials);
        }

        foreach (GameObject stone in healthStones)
        {
            healthStonesMat.Add(stone.GetComponent<Renderer>().materials);
        }

        UpdatePlayerHealth();
        UpdateEnemyHealth();

        EventManager.StartListening("UpdatePlayerHealth", updatePlayerHealthListener);
        EventManager.StartListening("UpdatePlayerHealth", updateEnemyHealthListener);
        EventManager.StartListening("UpdatePlayerHealth", updateComboStatusListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        updatePlayerHealthListener = new UnityAction(UpdatePlayerHealth);
        updateEnemyHealthListener = new UnityAction(UpdateEnemyHealth);
        updateComboStatusListener = new UnityAction(UpdateComboStatus);
}

    private void UpdatePlayerHealth() {
        int playerCurrHealth = playerRef.playerHealth;


        for (int i = 0; i < playerCurrHealth; i++) {
            healthStonesMat[0][12 - i].color = Color.green;
        }

        for (int i = 1; i < (12 - playerCurrHealth); i++)
        {
            healthStonesMat[0][i + 1].color = Color.red;
        }
    }

    private void UpdateEnemyHealth()
    {
        int enemyCurrHealth = (int) Mathf.Ceil((float) (enemyRef.health / 10));

        for (int i = 0; i < enemyCurrHealth; i++)
        {
            healthStonesMat[1][11 - i].color = Color.green;
        }

        for (int i = 1; i < (11 - enemyCurrHealth); i++)
        {
            healthStonesMat[1][i + 1].color = Color.red;
        }
    }

    private void UpdateComboStatus()
    {

    }
}
