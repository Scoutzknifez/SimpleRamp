using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public float xSize = 1f;
    public float zSize = 1f;

    private void OnDrawGizmos()
    {
        Vector3 spawnSize = new Vector3(xSize, 0, zSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (new Vector3(0, transform.parent.localScale.y / 2, 0)), spawnSize);
    }

    public Vector3 GetSpawnLocation()
    {
        Vector3 spawnLoc = new Vector3(Random.Range(-xSize / 2, xSize / 2), 1, Random.Range(-zSize / 2,zSize / 2));
        return spawnLoc + transform.position;
    }
}
