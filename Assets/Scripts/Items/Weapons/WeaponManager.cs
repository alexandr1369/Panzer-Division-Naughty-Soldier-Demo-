using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Singleton

    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    // current equiped weapon data
    [SerializeField] private GameObject currentWeaponObject;
    private Weapon currentWeapon;

    // list of all weapons to equip
    [SerializeField] private List<Weapon> allWeapons;

    // callbacks for when a weapon is equipped
    public delegate void OnWeaponChanged(Weapon newWeapon);
    public event OnWeaponChanged onWeaponChanged;
    public delegate void OnBulletSpawnPointsChanged(AudioClip shootSound);
    public event OnBulletSpawnPointsChanged onBulletsSpawnPointsChanged;

    private void Start()
    {
        EquipDefault();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EquipPrevious();
        if (Input.GetKeyDown(KeyCode.W))
            EquipNext();
    }

    // equip the weapon
    public void Equip(Weapon weapon)
    {
        // load new weapon object
        GameObject newWeaponObject = Instantiate(weapon.weaponPrefab, currentWeaponObject.transform.parent.position, currentWeaponObject.transform.rotation, currentWeaponObject.transform.parent);
        currentWeapon = weapon;

        // update current weapon object
        Destroy(currentWeaponObject);
        currentWeaponObject = newWeaponObject;

        // wepoan has been equipped -> trigger
        if (onWeaponChanged != null)
            onWeaponChanged(currentWeapon);
        if (onBulletsSpawnPointsChanged != null)
            onBulletsSpawnPointsChanged(currentWeapon.shootSound);
    }
    // equip next weapon
    public void EquipNext()
    {
        // get next weapon
        Weapon nextWeapon = allWeapons[0];
        int currentWeaponIndex = allWeapons.IndexOf(currentWeapon);
        if (currentWeaponIndex < allWeapons.Count - 1)
            nextWeapon = allWeapons[currentWeaponIndex + 1];

        // equip the weapon
        Equip(nextWeapon);
    }
    // equip previous weapon
    public void EquipPrevious()
    {
        // get previous weapon
        Weapon nextWeapon = allWeapons[allWeapons.Count - 1];
        int currentWeaponIndex = allWeapons.IndexOf(currentWeapon);
        if (currentWeaponIndex > 0)
            nextWeapon = allWeapons[currentWeaponIndex - 1];

        // equip the weapon
        Equip(nextWeapon);
    }
    // equip default weapon
    private void EquipDefault()
    {
        Equip(allWeapons[0]);
    }
}
