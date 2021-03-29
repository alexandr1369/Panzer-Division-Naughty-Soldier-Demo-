using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// just fast access to bullet spawn points
[RequireComponent(typeof(Transform))]
public class BulletSpawnPoint : MonoBehaviour
{
    public Transform GetBulletSpawnPoint() => transform;
}
