using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDecider {

    public EnemySkills decideSkill(GameObject user) {
        //int skill = Random.Range(0, 2);
        int skill = 0;
        switch (skill) {
            case 0:
                return new ColumnAttack(3, user, 5);
        }

        return null;
    }
}
