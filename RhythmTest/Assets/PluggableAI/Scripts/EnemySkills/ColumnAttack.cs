using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private GameObject spells;

    private int columnSelected;
    private int columnStartIndex;
    private int columnEndIndex;

    public ColumnAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        spells = Resources.Load<GameObject>("Prefabs/Spells");

        columnSelected = undefinedValue;
        columnStartIndex = -2;
        columnEndIndex = 2;
    }

    private void setupAndCastSpell() {
        Vector3 targetPosition = new Vector3(columnSelected, user.transform.position.y, user.transform.position.z - 1f);
        GameObject spell = GameObject.Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "enemyColumnAttack");
    }

    public override void doSkill() {
        Debug.Log("do column attack");
        
        for (int i = columnStartIndex; i < columnEndIndex+1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected - 1);
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected);
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected + 1);
        }

        setupAndCastSpell();
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (columnSelected == undefinedValue) {
            columnSelected = Mathf.RoundToInt(user.transform.position.x);
        }

        switch (stage) {
            case 0:
                changeToOriginalMaterial(position);
                break;
            case 1:
                changeToFirstMaterial(position);
                break;
            case 2:
                changeToSecondMaterial(position);
                break;
        }
    }

    private void changeToFirstMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            //Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            Grid.instance.changeToFirstMaterial(i, columnSelected - 1);
            Grid.instance.changeToFirstMaterial(i, columnSelected);
            Grid.instance.changeToFirstMaterial(i, columnSelected + 1);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(i, columnSelected - 1);
            Grid.instance.changeToSecondMaterial(i, columnSelected);
            Grid.instance.changeToSecondMaterial(i, columnSelected + 1);
        }
    }

    private void changeToOriginalMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(i, columnSelected - 1);
            Grid.instance.changeToOriginalMaterial(i, columnSelected);
            Grid.instance.changeToOriginalMaterial(i, columnSelected + 1);
        }
    }
}
