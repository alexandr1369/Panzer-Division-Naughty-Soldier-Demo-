using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    #endregion

    [Header("Targets")]
    [SerializeField] private ScoreManager scoreTarget; // ref to score
    [SerializeField] private PlayerStats playerTarget; // ref to player

    #region Player

    [Header("Player Data")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text bulletsText;

    #endregion

    #region Deaths Score Data

    [Header("Base Data")]
    [SerializeField] private Text enemiesAAmountText;
    [SerializeField] private Text enemiesBAmountText;
    [SerializeField] private Text enemiesBossAmountText;
    [SerializeField] private Text playersAmountText;

    #endregion

    private void Awake()
    {
        // get instance
        instance = this;

        // sub to actions
        playerTarget.onHealthChanged += OnHealthChanged;
        playerTarget.onBulletsChanged += OnBulletsChanged;
        scoreTarget.onDeadCoresAmountChanged += OnDeadEnemiesAmountChanged;
    }

    #region Actions

    private void OnHealthChanged(int healthAmount)
    {
        healthText.text = healthAmount.ToString();
    }
    private void OnBulletsChanged(int bulletsAmount)
    {
        bulletsText.text = bulletsAmount.ToString();
    }
    private void OnDeadEnemiesAmountChanged(ScoreMemento instance)
    {
        enemiesAAmountText.text = instance.enemiesAAmount.ToString();
        enemiesBAmountText.text = instance.enemiesBAmount.ToString();
        enemiesBossAmountText.text = instance.enemiesBossAmount.ToString();
        playersAmountText.text = instance.playersAmount.ToString();
    }

    #endregion
}
