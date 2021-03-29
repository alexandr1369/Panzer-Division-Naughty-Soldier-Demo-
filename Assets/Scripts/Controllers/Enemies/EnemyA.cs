using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    protected override void Die(IEnemyDiesVisitor visitor)
    {
        // score manager informing
        visitor.Visit(this);

        // unique action on dying (just destroy for demo)
        Destroy(gameObject);
    }
}
