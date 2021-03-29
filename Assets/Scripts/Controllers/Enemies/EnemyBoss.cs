using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    protected override void Die(IEnemyDiesVisitor visiter)
    {
        // score manager informing
        visiter.Visit(this);

        // 'unique action on dying' (just destroy for demo)
        Destroy(gameObject);
    }
}
