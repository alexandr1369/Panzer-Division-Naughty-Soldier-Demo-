// 'memento' pattern utility
// saves current score manager instance state
[System.Serializable]
public class ScoreMemento
{
    public int enemiesAAmount;
    public int enemiesBAmount;
    public int enemiesBossAmount;
    public int playersAmount;
    public ScoreMemento (int enemiesA, int enemiesB, int enemiesBoss, int players)
    {
        enemiesAAmount = enemiesA;
        enemiesBAmount = enemiesB;
        enemiesBossAmount = enemiesBoss;
        playersAmount = players;
    }
    public void GetData(out int enemiesA, out int enemiesB, out int enemiesBoss, out int players)
    {
        enemiesA = enemiesAAmount;
        enemiesB = enemiesBAmount;
        enemiesBoss = enemiesBossAmount;
        players = playersAmount;
    }
}
