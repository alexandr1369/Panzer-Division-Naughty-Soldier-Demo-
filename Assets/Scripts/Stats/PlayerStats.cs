using UnityEngine;

public class PlayerStats : CoreStats
{
    [Header("Bullets")]
    [SerializeField] protected Stat currentBullets; // current amount of bullets
    [SerializeField] protected int maxBullets; // maximun amount of bullets

    public delegate void OnHealthChanged(int healthAmount);
    public event OnHealthChanged onHealthChanged;

    public delegate void OnBulletsChanged(int bulletsAmount);
    public event OnBulletsChanged onBulletsChanged;

    protected override void Start()
    {
        base.Start();
        WeaponManager.instance.onWeaponChanged += OnWeaponChanged;
        SetBullets(maxBullets);
        SetHealth(maxHealth);
    }

    private void SetHealth(int healthAmount)
    {
        currentHealth.baseValue = healthAmount;
        if (onHealthChanged != null)
            onHealthChanged(currentHealth.baseValue);
    }
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (onHealthChanged != null)
            onHealthChanged(currentHealth.baseValue);
    }
    public override bool Heal(int healAmount)
    {
        bool hasHealed = base.Heal(healAmount);
        if (hasHealed)
        {
            if (onHealthChanged != null)
                onHealthChanged(currentHealth.baseValue);
        }

        return hasHealed;
    }

    // get availability to shoot
    public bool CanShoot() => currentBullets.baseValue > 0;

    // add / remove bullets
    private void SetBullets(int bulletsAmount)
    {
        currentBullets.baseValue = Mathf.Clamp(Mathf.Clamp(bulletsAmount, 0, int.MaxValue), 0, maxBullets);
        if (onBulletsChanged != null)
            onBulletsChanged(currentBullets.baseValue);
    }
    public bool AddBullets(int bulletsAmount)
    {
        if (currentBullets.baseValue == maxBullets) return false;
        else
        {
            int newBulletsAmount = currentBullets.baseValue + bulletsAmount;
            SetBullets(newBulletsAmount);
            print("You've got " + bulletsAmount + " bullet" + (bulletsAmount > 1 ? "s" : " "));
            return true;
        }
    }
    public void RemoveBullets(int bulletsAmount)
    {
        int newBulletsAmount = currentBullets.baseValue - bulletsAmount;
        SetBullets(newBulletsAmount);
    }

    // update weapon damage callback
    private void OnWeaponChanged(Weapon newWeapon)
    {
        damage.baseValue = newWeapon.damage;
    }
}
