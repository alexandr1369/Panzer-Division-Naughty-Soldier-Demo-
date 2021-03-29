using UnityEngine;

// static booster spawner (every N seconds)
// should have child graphics object to visual inform player about availability to be picked up
public class BoosterController : MonoBehaviour
{
    private bool isActive; // is booster shown right now

    [SerializeField] private Booster booster; // booster item ref

    [SerializeField] private GameObject graphics; // child graphics obj

    [SerializeField] private float spawnCooldown; // spawning delay
    private float currentSpawnCooldown; // current spawning delay

    private void Start()
    {
        ToggleActiveState(true);
        currentSpawnCooldown = spawnCooldown;
    }
    public void Update()
    {
        // check for being already shown
        if (isActive) return;

        // show booster
        if(currentSpawnCooldown <= 0)
        {
            ToggleActiveState(true);
            currentSpawnCooldown = spawnCooldown;
        }
        // continue waiting for showing
        else
        {
            currentSpawnCooldown -= Time.deltaTime;
        }
    }
    public void OnTriggerStay(Collider collider)
    {
        if (isActive)
        {
            PlayerStats playerStats = collider.GetComponentInParent<PlayerStats>();
            if (playerStats)
            {
                // try to use booster
                bool hasUsed = booster.Use(playerStats);

                // update data
                if(hasUsed)
                    ToggleActiveState(false);
            }
        }
    }

    // activate / deactivate current booster
    private void ToggleActiveState(bool state)
    {
        isActive = state;
        graphics.SetActive(state);
    }
}   
