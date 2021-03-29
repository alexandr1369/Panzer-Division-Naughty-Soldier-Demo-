using System.Collections.Generic;
using UnityEngine;

// 'memento' pattern utility
// just an example of data saving
public class GameHistory : MonoBehaviour
{
    // game history data
    private Stack<ScoreMemento> history;

    private void Start()
    {
        LoadHistory();
    }
    public void Push(ScoreMemento memento)
    {
        // add new history unit
        history.Push(memento);

        // update data
        SaveHistory();
    }
    public ScoreMemento Pop()
    {
        // check for empty data
        if (history.Count == 0) return null;

        // get last memento
        ScoreMemento lastMemento = history.Pop();

        // update data
        SaveHistory();

        // return memento
        return lastMemento;
    }

    #region Utils

    private ScoreMemento[] ToArray()
    {
        return history.ToArray();
    }
    private Stack<ScoreMemento> ToStack(ScoreMemento[] array)
    {
        Stack<ScoreMemento> newHistoryMemento = new Stack<ScoreMemento>();
        for (int i = array.Length - 1; i >= 0; i--)
        {
            newHistoryMemento.Push(array[i]);
        }

        return newHistoryMemento;
    }

    // example of saving and loading history from PlayerPrefs (JUST FOR DEMO)
    // of course I'd like to use .json file or DB
    private void LoadHistory()
    {
        if (PlayerPrefs.HasKey("GameHistory"))
            history = ToStack(JsonHelper.FromJson<ScoreMemento>(PlayerPrefs.GetString("GameHistory")));
        else
            history = new Stack<ScoreMemento>();
    }
    private void SaveHistory()
    {
        string gameHistory = JsonHelper.ToJson(ToArray());
        PlayerPrefs.SetString("GameHistory", gameHistory);
    }

    #endregion
}
