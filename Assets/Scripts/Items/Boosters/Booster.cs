using UnityEngine;

abstract public class Booster : Item
{
    public AudioClip pickUpSound; // sfx on use

    // use booster
    public abstract bool Use(PlayerStats playerStats);
}
