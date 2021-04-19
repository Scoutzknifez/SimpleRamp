using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If player falls, put them back here.")]
    private Transform spawnPoint = null;

    public void sendPlayerToSpawn()
    {
        // Move player to spawn
        GetComponent<CharacterController>().enabled = false;
        Vector3 spawnLoc = new Vector3(Random.Range(-spawnPoint.localScale.x / 2, spawnPoint.localScale.x / 2), 1, Random.Range(-spawnPoint.localScale.z / 2, spawnPoint.localScale.z / 2));
        transform.position = spawnPoint.position + spawnLoc;
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        GetComponentInChildren<CharacterController>().enabled = true;
    }
}
