using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed; // amount of player movement speed
    [SerializeField] private float rotationSpeed; // amount of player rotation speed
    private Rigidbody rb; // physics

    [Header("Attack")]
    [SerializeField] private Transform panzarHead; // an object to easy and fast access to bullet spawn points
    [SerializeField] private GameObject bulletPrefab; // bullet prefab
    private List<Transform> bulletsSpawnPoints; // list of all bullets spawn points

    [SerializeField] private float shootDelay; // delay between shooting
    private float currentShootDelay; // current delay

    [Header("Animation")]
    [SerializeField] private Animator animator; // graphics animator (panzer)

    [Header("SFX")]
    [SerializeField] private AudioClip shootSound; // panzar shoot sound

    private PlayerStats playerStats; // player stats info

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        playerStats.onHealthReachedZero += Die;
        WeaponManager.instance.onBulletsSpawnPointsChanged += OnBulletsSpawnPointsChanged;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            Shoot();

        // continue waiting for the end of shooting cooldown
        if (currentShootDelay > 0)
            currentShootDelay -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        #region Movement

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // move panzer forward
            Vector3 newPosition = transform.position + transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);

            // play forward animation
            animator.SetFloat("Speed", 1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // move panzer backward
            Vector3 newPosition = transform.position - transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);

            // play backward animation
            animator.SetFloat("Speed", -1f);
        }
        else
        {
            // stop panzer movement animation
            animator.SetFloat("Speed", 0);
        }

        #endregion

        #region Rotation

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // rotate panzer to the left
            Vector3 currentRotation = rb.rotation.eulerAngles;
            Quaternion newRotation = Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y - rotationSpeed * Time.deltaTime, currentRotation.z));
            rb.MoveRotation(newRotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // rotate panzer to the right
            Vector3 currentRotation = rb.rotation.eulerAngles;
            Quaternion newRotation = Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y + rotationSpeed * Time.deltaTime, currentRotation.z));
            rb.MoveRotation(newRotation);
        }

        #endregion
    }

    // shoot with a bullet
    private void Shoot()
    {
        // check for shooting
        if(!playerStats.CanShoot())
        {
            print("Not enough bullets to shoot!");
            return;
        }
        else if(currentShootDelay > 0)
        {
            print(string.Format("Shooting cooldown. Wait {0:0.00} seconds", currentShootDelay));
            return;
        }

        // bullet logic is to fly forward and check for collusions
        // until it's lifes time is over or interacting with an object
        for(int i = 0; i < bulletsSpawnPoints.Count; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletsSpawnPoints[i].position, transform.rotation).GetComponent<Bullet>();
            bullet.Shoot(playerStats.GetDamageAmount());
        }

        // 1 weapon shoot (it can be more than one bullet = 1 player stats bullets)
        playerStats.RemoveBullets(1);

        // set shooting cooldown
        currentShootDelay = shootDelay;

        // vfx
        CameraShaker.Instance.ShakeOnce(3f, 3f, .3f, .3f);

        // sfx
        SoundManager.instance.PlaySingle(shootSound);
    }
    // player dies
    private void Die(IEnemyDiesVisitor visitor)
    {
        // score manager informing
        visitor.Visit(this);

        // unique action on dying (just reload the level)
        StartCoroutine(Invoke(GameManager.instance.ReloadScene, .35f));
    }

    #region Utils

    // NOT the best way (for demo is OK)
    private void OnBulletsSpawnPointsChanged(AudioClip shootSound)
    {
        // update shoot sfx
        this.shootSound = shootSound;

        // wait ~ next frame to prevent calculation points amount while destroying old weapon objects (BUG)
        StartCoroutine(Invoke(UpdateBulletsSpawnPointsData, .01f));
    }
    private IEnumerator Invoke(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
    private void UpdateBulletsSpawnPointsData()
    {
        bulletsSpawnPoints = new List<Transform>();
        foreach (var spawnPoint in panzarHead.GetComponentsInChildren<BulletSpawnPoint>())
            bulletsSpawnPoints.Add(spawnPoint.GetBulletSpawnPoint());
    }

    #endregion
}
