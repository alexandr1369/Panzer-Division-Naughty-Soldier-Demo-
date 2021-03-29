using UnityEngine;
using Pixelplacement;

public class EnemyPortal : MonoBehaviour
{
    private Vector3 initPosition; // start position

    private void Awake()
    {
        // save start position (to return back after huge animation influence)
        initPosition = transform.position;
    }
    public void SpawnEnemy(GameObject enemyPrefab)
    {
        // spawn enemy (rotate 180 on OY is just because of damn enemy prefab)
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180f, 0));

        // do unique action (some vfx like smoke or smth)
        // (shake animation -> It'd be a cool idea to use DOTS but it OK for DEMO)
        Tween.Shake(transform, transform.position, new Vector3(1f, 0, 1f), .5f, 0,
        completeCallback: () => 
        {
            Tween.Position(transform, initPosition, .15f, 0, Tween.EaseOut);
        });
    }
}
