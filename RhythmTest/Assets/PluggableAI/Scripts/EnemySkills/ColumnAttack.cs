using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private int range;
    private int columnSelected;

    public ColumnAttack(int chargeRequired, GameObject user, int range) : base(chargeRequired) {
        this.user = user;
        this.range = range;
        columnSelected = undefinedValue;
    }

    public override void doSkill(Vector3 position) {
        Debug.Log("do column attack");

        for (int i = 1; i < range+1; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            DamageController.instance.checkAndDoDamageToPlayer(Mathf.RoundToInt(targetPosition.z), Mathf.RoundToInt(targetPosition.x));
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
        for (int i = 1; i < range + 1; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            Grid.instance.changeToFirstMaterial((int)targetPosition.z, (int)targetPosition.x);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = 1; i < range + 1; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            Grid.instance.changeToSecondMaterial((int)targetPosition.z, (int)targetPosition.x);
        }
    }

    private void changeToOrignalMaterial(Vector3 position) {
        for (int i = 1; i < range + 1; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            Grid.instance.changeToOriginalMaterial((int)targetPosition.z, (int)targetPosition.x);
        }
    }
}
