using System.Collections.Generic;
using UnityEngine;

public class CoreStats : MonoBehaviour
{
    private bool isKilled; // killing state (prevent multi bullet hitting bug)

    [Header("SFX")]
    [SerializeField] private List<AudioClip> hitSounds; // hit sfx
    [SerializeField] private AudioClip dyingSound; // dying sfx

    [Header("Health")]
    [SerializeField] protected Stat currentHealth; // current amount of health
    [Range(0, 100)]
    [SerializeField] protected int maxHealth; // maximum amount of health

    [Header("Armor")]
    [SerializeField] protected Stat armor; // current amount of armor (0..100)
    protected int maxArmor = 100;

    [Header("Damage")]
    [SerializeField] protected Stat damage; // current amount of damage

    public delegate void OnHealthReachedZero(IEnemyDiesVisitor visitor);
    public event OnHealthReachedZero onHealthReachedZero;

    protected virtual void Awake()
    {
        armor.baseValue = Mathf.Clamp(armor.baseValue, 0, maxArmor);
    }
    protected virtual void Start() { }

    // damage the core
    public virtual void TakeDamage(int damageAmount)
    {
        // check for being killed already
        if (isKilled) return;

        // decrease damage amount according to the armor amount
        float armorIndex = armor.baseValue / 100f;
        damageAmount = Mathf.Clamp(Mathf.CeilToInt(damageAmount * (1f - armorIndex)), 0, int.MaxValue);

        // subtract damage from health
        currentHealth.baseValue -= damageAmount;
        print(transform.name + " takes " + damageAmount + " damage.");

        // check for death
        if (currentHealth.baseValue <= 0)
        {
            if (onHealthReachedZero != null)
            {
                // toggle killing state
                isKilled = true;

                // invoke an action
                onHealthReachedZero.Invoke(GameManager.instance.GetScoreManager());

                // play dying sfx
                SoundManager.instance.PlaySingle(dyingSound);
            }
        }
        else
        {
            // play hit sfx
            SoundManager.instance.PlaySingle(hitSounds[Random.Range(0, hitSounds.Count)]);
        }
    }
    // heal the core
    public virtual bool Heal(int healAmount)
    {
        if (currentHealth.baseValue == maxHealth) return false;
        else
        {
            currentHealth.baseValue = Mathf.Clamp(currentHealth.baseValue + Mathf.Clamp(healAmount, 0, int.MaxValue), 0, maxHealth);
            print(transform.name + " is being healed for " + healAmount + " HP");
            return true;
        }
    }

    #region Utils

    public int GetDamageAmount() => damage.baseValue;

    #endregion
}
