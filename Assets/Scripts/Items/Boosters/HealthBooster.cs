using UnityEngine;

[CreateAssetMenu(fileName = "New Health Booster", menuName = "Boosters/Health Booster")]
public class HealthBooster : Booster
{
    public int healthAmount; // health gain amount

    public override bool Use(PlayerStats playerStats)
    {
        // heal the player
        bool hasUsed = playerStats.Heal(healthAmount);
        if (hasUsed)
            SoundManager.instance.PlaySingle(pickUpSound);

        return hasUsed;
    }
}
