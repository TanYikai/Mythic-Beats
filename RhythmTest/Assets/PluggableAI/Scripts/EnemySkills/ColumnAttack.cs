using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    private GameObject spells;
    private GameObject user;

    public ColumnAttack(int chargeRequired, GameObject user) : base(chargeRequired) {
        spells = Resources.Load<GameObject>("Prefabs/Spells");
        this.user = user;
    }

    public override void doSkill(Vector3 position) {
        Debug.Log("do column attack");
        int columnSelected = Random.Range(-4, 4);

        for (int i = 1; i < 9; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            GameObject spell = GameObject.Instantiate(spells);
            spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        }
    }
}
