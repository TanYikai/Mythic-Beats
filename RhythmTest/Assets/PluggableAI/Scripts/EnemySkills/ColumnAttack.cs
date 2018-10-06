using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAttack : EnemySkills {

    private GameObject spells;
    private GameObject user;
    private int range;

    public ColumnAttack(int chargeRequired, GameObject user, int range) : base(chargeRequired) {
        spells = Resources.Load<GameObject>("Prefabs/Spells");
        this.user = user;
        this.range = range;
    }

    public override void doSkill(Vector3 position) {
        Debug.Log("do column attack");
        int columnSelected = Random.Range(-4, 5);

        for (int i = 1; i < range+1; i++) {
            Vector3 targetPosition = position + new Vector3(columnSelected, 0, -i);
            GameObject spell = GameObject.Instantiate(spells);
            spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        }
    }
}
