using UnityEngine;

[CreateAssetMenu(fileName = "New Bullets Booster", menuName = "Boosters/Bullets Booster")]
public class BulletsBooster : Booster
{
    public int bulletsAmount; // bullets gain amount

    public override bool Use(PlayerStats playerStats)
    {
        // add bullets
        bool hasUsed = playerStats.AddBullets(bulletsAmount);
        if (hasUsed)
            SoundManager.instance.PlaySingle(pickUpSound);

        return hasUsed;
    }
}
