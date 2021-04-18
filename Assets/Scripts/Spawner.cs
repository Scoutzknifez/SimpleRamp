using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ball to spawn on ramp.")]
    private GameObject ballToSpawn = null;

    [SerializeField]
    [Tooltip("The time inbetween each spawn in seconds.")]
    private float spawnRateInSeconds = 0;

    [SerializeField]
    [Tooltip("The random range where we can spawn balls in this area in accordance to the x direction.")]
    private float xVariance = 0;

    [SerializeField]
    [Tooltip("The random range where we can spawn balls in this area in accordance to the z direction .")]
    private float zVariance = 0;

    private void OnDrawGizmos()
    {
        Vector3 cubeSize = new Vector3(xVariance * 2, 0, zVariance * 2);
        Gizmos.DrawWireCube(transform.position, cubeSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRegularly());
    }

    private IEnumerator SpawnRegularly()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateInSeconds);

            Vector3 spawnLocation = transform.position + new Vector3(xVariance == 0 ? 0 : Random.Range(-xVariance, xVariance), 0, zVariance == 0 ? 0 : Random.Range(-zVariance, zVariance));
            Instantiate(ballToSpawn, spawnLocation, transform.rotation);
        }
    }
}
