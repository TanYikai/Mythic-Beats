using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private int firstColumnSelected;
    private int secondColumnSelected;
    private int columnStartIndex;
    private int columnEndIndex;

    public ColumnAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        firstColumnSelected = undefinedValue;
        columnStartIndex = -2;
        columnEndIndex = 2;
    }

    public override void doSkill() {
        Debug.Log("do column attack");

        for (int i = columnStartIndex; i < columnEndIndex+1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(i, firstColumnSelected);
            DamageController.instance.checkAndDoDamageToPlayer(i, secondColumnSelected);
        }
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (firstColumnSelected == undefinedValue) {
            firstColumnSelected = Random.Range(-4, 5);
            setOtherColumn();
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

    private void setOtherColumn() {
        if (user.GetComponentInChildren<Enemy>().checkValidGeneralPosition(new Vector3(firstColumnSelected - 1, 0, 0))) {
            secondColumnSelected = firstColumnSelected - 1;
        }
        else {
            secondColumnSelected = firstColumnSelected + 1;
        }
    }

    private void changeToFirstMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            //Vector3 targetPosition = position + new Vector3(firstColumnSelected, 0, -i);
            Grid.instance.changeToFirstMaterial(i, firstColumnSelected);
            Grid.instance.changeToFirstMaterial(i, secondColumnSelected);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(i, firstColumnSelected);
            Grid.instance.changeToSecondMaterial(i, secondColumnSelected);
        }
    }

    private void changeToOriginalMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(i, firstColumnSelected);
            Grid.instance.changeToOriginalMaterial(i, secondColumnSelected);
        }
    }
}
