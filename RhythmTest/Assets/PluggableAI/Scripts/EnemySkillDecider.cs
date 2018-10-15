using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDecider {

    private enum Attacks {
        columnAttack,
        rowAttack
    };

    private GameObject enemy;
    private GameObject player;

    private int totalNumberOfAttacks;
    private int[] attackListWeights;
    private int[] originalAttacksListWeights;

    public EnemySkillDecider(GameObject enemy) {
        this.enemy = enemy;
        player = enemy.GetComponentInChildren<Enemy>().getEnemy();
        setupAttacksWeight();
    }

    private void setupAttacksWeight() {
        totalNumberOfAttacks = 2;
        attackListWeights = new int[2]{ 0, 1 };  //first weight for columnAttack and so on...
        originalAttacksListWeights = new int[2] { 0, 1 };
    }

    public EnemySkills decideSkill() {

        EnemySkills chosenSkill = null;

        changeWeightsOfAttacks();

        int skillValue = generateSkillSelectorValue();

        switch (skillValue) {
            case 0:
                chosenSkill = new ColumnAttack(3, enemy);
                break;
            case 1:
                chosenSkill = new RowAttack(3, enemy);
                break;
        }

        resetWeightsOfAttacks();

        return chosenSkill;
    }

    private void changeWeightsOfAttacks() {
        if (isPlayerInFrontOfEnemy()) {
            attackListWeights[(int)Attacks.columnAttack] = 3;
        }
    }

    private void resetWeightsOfAttacks() {
        for (int i = 0; i < attackListWeights.Length; i++) {
            attackListWeights[i] = originalAttacksListWeights[i];
        }
    }

    private bool isPlayerInFrontOfEnemy() {
        int playerX = Mathf.RoundToInt(player.transform.position.x);
        int enemyX = Mathf.RoundToInt(enemy.transform.position.x);
        if (playerX == enemyX - 1 || playerX == enemyX - 1 || playerX == enemyX + 1) {
            return true;
        }
        return false;
    }

    private int generateSkillSelectorValue() {
        int[] cumulativeSum = new int[totalNumberOfAttacks];
        int totalWeightValue = 0;

        for (int i = 0; i < totalNumberOfAttacks; i++) {
            totalWeightValue += attackListWeights[i];
            cumulativeSum[i] = totalWeightValue;
        }

        int randomValue = Random.Range(0, totalWeightValue);

        for (int i = 0; i < cumulativeSum.Length; i++) {
            if (randomValue < cumulativeSum[i]) {
                return i;
            }
        }

        return 0;
    }
}
