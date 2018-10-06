using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private int firstRowSelected;
    private int secondRowSelected;
    private int rowStartIndex;
    private int rowEndIndex;

    public RowAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        firstRowSelected = undefinedValue;
        rowStartIndex = -4;
        rowEndIndex = 4;
    }

    public override void doSkill() {
        Debug.Log("do row attack");

        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(firstRowSelected, i);
            DamageController.instance.checkAndDoDamageToPlayer(secondRowSelected, i);
        }
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (firstRowSelected == undefinedValue) {
            firstRowSelected = Random.Range(-2, 3);
            setOtherRow();
        }

        switch (stage) {
            case 0:
                changeToOrignalMaterial(position);
                break;
            case 1:
                changeToFirstMaterial(position);
                break;
            case 2:
                changeToSecondMaterial(position);
                break;
        }
    }

    private void setOtherRow() {
        if (user.GetComponentInChildren<Enemy>().checkValidGeneralPosition(new Vector3(0, 0, firstRowSelected - 1))) {
            secondRowSelected = firstRowSelected - 1;
        }
        else {
            secondRowSelected = firstRowSelected + 1;
        }
    }

    private void changeToFirstMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToFirstMaterial(firstRowSelected, i);
            Grid.instance.changeToFirstMaterial(secondRowSelected, i);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(firstRowSelected, i);
            Grid.instance.changeToSecondMaterial(secondRowSelected, i);
        }
    }

    private void changeToOrignalMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(firstRowSelected, i);
            Grid.instance.changeToOriginalMaterial(secondRowSelected, i);
        }
    }
}
