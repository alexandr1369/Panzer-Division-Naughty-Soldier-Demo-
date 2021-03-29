using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class Weapon : Item
{
    public GameObject weaponPrefab; // panzar head prefab
    public int damage; // damage amount of each spawned bullet
    public float shootDelay; // delay between shooting
    public AudioClip shootSound; // shoot sound
}