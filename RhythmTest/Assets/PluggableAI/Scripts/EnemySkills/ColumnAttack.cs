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
            DamageController.instance.checkAndDoDamageToPlayer(i, columnSelected);
        }
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (columnSelected == undefinedValue) {

            columnSelected = Random.Range(-4, 5);
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
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            //Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            Grid.instance.changeToFirstMaterial(i, columnSelected);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(i, columnSelected);
        }
    }

    private void changeToOrignalMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(i, columnSelected);
        }
    }
}
