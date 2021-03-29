using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// utility class just to host player transform link
// NOT the best idea for ONLINE game (for demo is OK)
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Transform Player { get => player; }
    [SerializeField] private Transform player;
}
