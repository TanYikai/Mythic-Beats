using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private GameObject spells;

    private int firstRowSelected;
    private int secondRowSelected;
    private int rowStartIndex;
    private int rowEndIndex;

    public RowAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        spells = Resources.Load<GameObject>("Prefabs/Spells");

        firstRowSelected = undefinedValue;
        rowStartIndex = -4;
        rowEndIndex = 4;
    }

    private void setupAndCastSpell() {
        // first row
        Vector3 firstTargetPosition = new Vector3(0, user.transform.position.y, firstRowSelected);
        GameObject spell = GameObject.Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, firstTargetPosition, "enemyRowAttack");

        // second row
        Vector3 secondTargetPosition = new Vector3(0, user.transform.position.y, secondRowSelected);
        spell = GameObject.Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, secondTargetPosition, "enemyRowAttack");
    }

    public override void doSkill() {
        Debug.Log("do row attack");

        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(firstRowSelected, i);
            DamageController.instance.checkAndDoDamageToPlayer(secondRowSelected, i);
        }

        setupAndCastSpell();
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (firstRowSelected == undefinedValue) {
            firstRowSelected = generateRandomPositionNearPlayer();
            setOtherRow();
        }

        changeSelectedTileMaterial(stage);
    }

    private int generateRandomPositionNearPlayer() {
        int playerZ = Mathf.RoundToInt(user.GetComponentInChildren<Enemy>().getPlayer().transform.position.z);
        return Random.Range(Mathf.Max(-2, playerZ - 1), Mathf.Min(3, playerZ + 1));
    }

    private void setOtherRow() {
        if (user.GetComponentInChildren<Enemy>().checkValidGeneralPosition(new Vector3(0, 0, firstRowSelected - 1))) {
            secondRowSelected = firstRowSelected - 1;
        }
        else {
            secondRowSelected = firstRowSelected + 1;
        }
    }

    private void changeSelectedTileMaterial(int stage) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.decideAndChangeMaterial(firstRowSelected, i, stage);
            Grid.instance.decideAndChangeMaterial(secondRowSelected, i, stage);
        }
    }
}
