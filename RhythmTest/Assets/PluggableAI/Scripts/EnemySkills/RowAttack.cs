using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private int rowSelected;
    private int rowStartIndex;
    private int rowEndIndex;

    public RowAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        rowSelected = undefinedValue;
        rowStartIndex = -4;
        rowEndIndex = 4;
    }

    public override void doSkill() {
        Debug.Log("do row attack");

        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            DamageController.instance.checkAndDoDamageToPlayer(rowSelected, i);
        }
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (rowSelected == undefinedValue) {
            rowSelected = Random.Range(-2, 3);
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

    private void changeToFirstMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToFirstMaterial(rowSelected, i);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(rowSelected, i);
        }
    }

    private void changeToOrignalMaterial(Vector3 position) {
        for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(rowSelected, i);
        }
    }
}
