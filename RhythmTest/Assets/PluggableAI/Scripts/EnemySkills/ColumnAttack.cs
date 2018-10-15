using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;

    private int columnSelected;
    private int columnStartIndex;
    private int columnEndIndex;

    public ColumnAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        columnSelected = undefinedValue;
        columnStartIndex = -2;
        columnEndIndex = 2;
    }

    public override void doSkill() {
        Debug.Log("do column attack");
        
        for (int i = columnStartIndex; i < columnEndIndex+1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected - 1);
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected);
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected + 1);
        }
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
