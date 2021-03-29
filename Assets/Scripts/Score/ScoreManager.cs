using UnityEngine;

[RequireComponent(typeof(GameHistory))]
public class ScoreManager : MonoBehaviour, IEnemyDiesVisitor
{
    private GameHistory gameHistory; // ref to game history

    #region Deaths Score Data

    private int enemiesAAmount;
    private int enemiesBAmount;
    private int enemiesBossAmount;
    private int playersAmount;

    #endregion

    public delegate void OnDeadCoresAmountChanched(ScoreMemento instance);
    public event OnDeadCoresAmountChanched onDeadCoresAmountChanged;

    private void Start()
    {
        gameHistory = GetComponent<GameHistory>();
        enemiesAAmount = enemiesBAmount = enemiesBossAmount = playersAmount = 0;
        UpdateData();
    }

    public void Visit(EnemyA enemyA)
    {
        // increase enemiesA deaths amount
        print("Enemy A dies!");
        enemiesAAmount++;

        // update data
        UpdateData();
    }
    public void Visit(EnemyB enemyB)
    {
        // increase enemiesB deaths amount
        print("Enemy B dies!");
        enemiesBAmount++;

        // update data
        UpdateData();
    }
    public void Visit(EnemyBoss enemyBoss)
    {
        // increase enemiesBoss deaths amount
        print("Enemy Boss dies!!!");
        enemiesBossAmount++;

        // update data
        UpdateData();
    }
    public void Visit(Player player)
    {
        // increase players deaths amount
        print("You died!\nGood Game.");
        playersAmount++;

        // update data
        UpdateData();
    }

    // update UI data
    private void UpdateData()
    {
        if (onDeadCoresAmountChanged != null)
            onDeadCoresAmountChanged(new ScoreMemento(enemiesAAmount, enemiesBAmount, enemiesBossAmount, playersAmount));
    }

    #region Game History

    public void SaveData()
    {
        if (enemiesAAmount != 0 || enemiesBAmount != 0 || enemiesBossAmount != 0 || playersAmount != 0)
        {
            gameHistory.Push(SaveState());
            print("Game has been saved.");
        }
        else
        {
            print("Nothing to save!");
        }
    }
    public void LoadData()
    {
        ScoreMemento loadedData = gameHistory.Pop();
        if (loadedData != null)
        {
            RestoreState(loadedData);
            UpdateData();
            print("Game was loaded.");
        }
        else
        {
            print("You have saved nothing!");
        }
    }

    #endregion

    #region Memento

    private ScoreMemento SaveState() => new ScoreMemento(enemiesAAmount, enemiesBAmount, enemiesBossAmount, playersAmount);
    private void RestoreState(ScoreMemento memento)
    {
        enemiesAAmount = memento.enemiesAAmount;
        enemiesBAmount = memento.enemiesBAmount;
        enemiesBossAmount = memento.enemiesBossAmount;
        playersAmount = memento.playersAmount;
    }

    #endregion
}
