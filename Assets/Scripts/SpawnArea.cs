using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 spawnSize = new Vector3(transform.localScale.x, 0, transform.localScale.z);

        Gizmos.DrawWireCube(transform.position + (new Vector3(0, transform.parent.localScale.y / 2, 0)), spawnSize);
    }
}
