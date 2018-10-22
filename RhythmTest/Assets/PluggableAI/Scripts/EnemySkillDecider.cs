using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDecider {

    public enum Attacks {
        columnAttack,
        rowAttack,
        randomAttack,
        columnProjectileAttack
    };

    private GameObject enemy;
    private GameObject player;
    private Enemy enemyScript;

    private EnemySkillWeights normalAttackWeights;
    private EnemySkillWeights berserkAttackWeights;

    public EnemySkillDecider(GameObject enemy) {
        this.enemy = enemy;
        enemyScript = enemy.GetComponentInChildren<Enemy>();
        player = enemyScript.getPlayer();
        
        setupAttacksWeightNormal();
        setupAttacksWeightBerserk();
    }

    private void setupAttacksWeightNormal() {
        List<int> attackListWeights = new List<int>{ 0, 1, 1, 1 };  //first weight for columnAttack and so on...
        normalAttackWeights = new EnemySkillWeights(attackListWeights);
    }

    private void setupAttacksWeightBerserk() {
        List<int> attackListWeights = new List<int> { 0, 1, 1, 1 };  //first weight for columnAttack and so on...
        berserkAttackWeights = new EnemySkillWeights(attackListWeights);
    }

    public EnemySkills decideSkill() {

        EnemySkills chosenSkill = null;

        if (!enemyScript.getIsBerserk()) {
            chosenSkill = generateSkillNormal();
        }
        else {
            chosenSkill = generateSkillBerserk();
        }

        return chosenSkill;
    }

    private EnemySkills generateSkillNormal() {
        EnemySkills chosenSkill = null;

        changeWeightsOfAttacksNormal();

        int skillValue = normalAttackWeights.generateSkillSelectorValue();

        switch (skillValue) {
            case 0:
                chosenSkill = new ColumnAttack(4, enemy);
                break;
            case 1:
                chosenSkill = new RowAttack(4, enemy);
                break;
            case 2:
                chosenSkill = new RandomAttack(4, enemy, 2);
                break;
            case 3:
                chosenSkill = new ColumnProjectileAttack(3, enemy);
                break;
        }

        normalAttackWeights.resetWeightsOfAttacks();

        return chosenSkill;
    }

    private void changeWeightsOfAttacksNormal() {
        if (isPlayerInFrontOfEnemy()) {
            normalAttackWeights.changeWeightsOfAttack(Attacks.columnAttack, 3);
        }
    }

    private EnemySkills generateSkillBerserk() {
        EnemySkills chosenSkill = null;

        changeWeightsOfAttacksBerserk();

        int skillValue = berserkAttackWeights.generateSkillSelectorValue();

        switch (skillValue) {
            case 0:
                chosenSkill = new ColumnAttack(3, enemy);
                break;
            case 1:
                chosenSkill = new RowAttack(3, enemy);
                break;
            case 2:
                chosenSkill = new RandomAttack(3, enemy, 4);
                break;
            case 3:
                chosenSkill = new ColumnProjectileAttack(2, enemy);
                break;
        }

        berserkAttackWeights.resetWeightsOfAttacks();

        return chosenSkill;
    }

    private void changeWeightsOfAttacksBerserk() {
        if (isPlayerInFrontOfEnemy()) {
            berserkAttackWeights.changeWeightsOfAttack(Attacks.columnAttack, 3);
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
}

public class EnemySkillWeights {

    private int totalNumberOfAttacks;
    private List<int> attackListWeights;
    private List<int> originalAttackListWeights;

    public EnemySkillWeights(List<int> attackListWeights) {
        this.attackListWeights = new List<int>(attackListWeights);
        originalAttackListWeights = new List<int>(attackListWeights);
        totalNumberOfAttacks = attackListWeights.Count;

    }

    public int generateSkillSelectorValue() {
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

    public void changeWeightsOfAttack(EnemySkillDecider.Attacks attackType, int weight) {
        attackListWeights[(int)attackType] = weight;
    }

    public void resetWeightsOfAttacks() {
        attackListWeights = new List<int>(originalAttackListWeights);
    }
}
