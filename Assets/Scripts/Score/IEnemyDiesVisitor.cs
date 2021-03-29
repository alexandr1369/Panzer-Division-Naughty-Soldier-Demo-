using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pattern 'Visitor' demonstration
/// </summary>
public interface IEnemyDiesVisitor
{
    public void Visit(EnemyA enemyA);
    public void Visit(EnemyB enemyB);
    public void Visit(EnemyBoss enemyBoss);
    public void Visit(Player player);
}
