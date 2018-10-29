using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StoneManagerTutorial : MonoBehaviour
{

    public PlayerTutorial playerRef;
    //public EnemyTutorial enemyRef;
    public ComboTutorial playerComboRef;
    public RhythmTutorial rhythmRef;
    public TextMeshProUGUI textMeshProUGUI;

    public GameObject[] comboStones;
    public GameObject[] healthStones;

    public List<Material[]> comboStonesMat;
    public List<Material[]> healthStonesMat;

    private UnityAction updatePlayerHealthListener;
    private UnityAction updateEnemyHealthListener;
    private UnityAction updateComboStatusListener;
    private UnityAction updateCounterListener;


    // Use this for initialization
    void Start()
    {
        comboStonesMat = new List<Material[]>();
        healthStonesMat = new List<Material[]>();

        foreach (GameObject stone in comboStones)
        {
            comboStonesMat.Add(stone.GetComponent<Renderer>().materials);
        }

        foreach (GameObject stone in healthStones)
        {
            healthStonesMat.Add(stone.GetComponent<Renderer>().materials);
        }

        UpdatePlayerHealth();
        UpdateEnemyHealth();

        EventManager.StartListening("UpdatePlayerHealth", updatePlayerHealthListener);
        EventManager.StartListening("UpdateEnemyHealth", updateEnemyHealthListener);
        EventManager.StartListening("UpdateComboStatus", updateComboStatusListener);
        EventManager.StartListening("UpdateCounter", updateCounterListener);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        updatePlayerHealthListener = new UnityAction(UpdatePlayerHealth);
        updateEnemyHealthListener = new UnityAction(UpdateEnemyHealth);
        updateComboStatusListener = new UnityAction(UpdateComboStatus);
        updateCounterListener = new UnityAction(UpdateCounter);
    }

    private void UpdatePlayerHealth()
    {
        int playerCurrHealth = playerRef.playerHealth;


        for (int i = 0; i < playerCurrHealth; i++)
        {
            healthStonesMat[0][12 - i].color = Color.green;
        }

        for (int i = 1; i < (12 - playerCurrHealth); i++)
        {
            healthStonesMat[0][i + 1].color = Color.red;
        }
    }

    private void UpdateEnemyHealth()
    {
        /*int enemyCurrHealth = (int)Mathf.Ceil((float)(enemyRef.health / 10));

        for (int i = 0; i < enemyCurrHealth; i++)
        {
            healthStonesMat[1][11 - i].color = Color.green;
        }

        for (int i = 1; i < (11 - enemyCurrHealth); i++)
        {
            healthStonesMat[1][i + 1].color = Color.red;
        }*/
    }

    private void UpdateComboStatus()
    {
        Queue<string> queue = playerComboRef.getQueue();

        if (queue.Count == 0)
        {
            resetColor();
            return;
        }

        string[] array = queue.ToArray();

        if (array.Length == 1)
        {
            for (int i = 0; i < comboStonesMat.Count; i++)
            {
                comboStonesMat[i][1].color = Color.green;
            }
            foreach (Material[] mat in comboStonesMat)
            {
                for (int i = 2; i < 5; i++)
                {
                    mat[i].color = Color.red;
                }
            }

        }

        if (array.Length > 1)
        {
            switch (array[1])
            {
                case "Right":
                    comboStonesMat[1][2].color = Color.green;
                    comboStonesMat[2][2].color = Color.green;
                    for (int i = 0; i < comboStonesMat.Count; i++)
                    {
                        if (i == 1 || i == 2) continue;
                        for (int j = 1; j < 5; j++)
                        {
                            comboStonesMat[i][j].color = Color.red;
                        }
                    }
                    break;
                case "Up":
                    comboStonesMat[0][2].color = Color.green;
                    resetOtherColor(0);
                    break;
                case "Down":
                    comboStonesMat[3][2].color = Color.green;
                    resetOtherColor(3);
                    break;
                default:
                    break;
            }
        }

        if (array.Length > 2)
        {
            switch (array[2])
            {
                case "Up":
                    if (array[1] == "Down")
                    {
                        comboStonesMat[3][3].color = Color.green;
                    }
                    else
                    {
                        comboStonesMat[1][3].color = Color.green;
                        resetOtherColor(1);
                    }
                    break;
                case "Left":
                    comboStonesMat[2][3].color = Color.green;
                    resetOtherColor(2);
                    break;
                case "Down":
                    comboStonesMat[0][3].color = Color.green;
                    break;
                default:
                    break;
            }
        }

        if (array.Length > 3)
        {
            switch (array[3])
            {
                case "Right":
                    if (array[2] == "Down")
                    {
                        comboStonesMat[0][4].color = Color.green;
                    }
                    else if (array[2] == "Up")
                    {
                        comboStonesMat[3][4].color = Color.green;
                    }
                    else
                    {
                        comboStonesMat[2][4].color = Color.green;
                    }
                    break;
                case "Down":
                    comboStonesMat[1][4].color = Color.green;
                    break;
                default:
                    break;
            }
        }

        changeRemainingColor();
    }

    private void UpdateCounter()
    {
        textMeshProUGUI.text = rhythmRef.playerComboCount.ToString();
    }

    private void changeRemainingColor()
    {
        foreach (Material[] mat in comboStonesMat)
        {
            for (int i = 1; i < 5; i++)
            {
                if (mat[i].color != Color.green)
                {
                    mat[i].color = Color.red;
                }
            }
        }
    }

    private void resetColor()
    {
        foreach (Material[] mat in comboStonesMat)
        {
            for (int i = 1; i < 5; i++)
            {
                mat[i].color = Color.red;
            }
        }
    }

    private void resetOtherColor(int id)
    {
        for (int i = 0; i < comboStonesMat.Count; i++)
        {
            if (i == id) continue;
            for (int j = 1; j < 5; j++)
            {
                comboStonesMat[i][j].color = Color.red;
            }
        }
    }


}
