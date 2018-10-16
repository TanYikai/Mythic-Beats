using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnProjectileAttack : EnemySkills {

    const int undefinedValue = -100;

    private GameObject user;
    private GameObject spells;

    private int columnSelected;
    private int columnStartIndex;
    private int columnEndIndex;

    public ColumnProjectileAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        this.user = user;
        spells = Resources.Load<GameObject>("Prefabs/Spells");

        columnSelected = undefinedValue;
        columnStartIndex = -2;
        columnEndIndex = 2;
    }

    private void setupAndCastSpell() {
        Vector3 targetPosition = new Vector3(columnSelected, user.transform.position.y, user.transform.position.z - 1f);
        GameObject spell = GameObject.Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "enemyColumnProjectile");
    }

    public override void doSkill() {
        Debug.Log("do column projectile attack");

        setupAndCastSpell();
    }

    public override void handleTelegraphAttack(Vector3 position, int stage) {
        if (columnSelected == undefinedValue) {
            columnSelected = generateRandomPositionNearPlayer();
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

    private int generateRandomPositionNearPlayer() {
        int playerX = Mathf.RoundToInt(user.GetComponentInChildren<Enemy>().getPlayer().transform.position.x);
        return Random.Range(Mathf.Max(-4, playerX - 1), Mathf.Min(4, playerX + 1));
    }

    private void changeToFirstMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToFirstMaterial(i, columnSelected);
        }
    }

    private void changeToSecondMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToSecondMaterial(i, columnSelected);
        }
    }

    private void changeToOriginalMaterial(Vector3 position) {
        for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
            Grid.instance.changeToOriginalMaterial(i, columnSelected);
        }
    }
}
