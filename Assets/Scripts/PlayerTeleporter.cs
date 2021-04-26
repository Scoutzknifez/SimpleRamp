using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If player falls, put them back here.")]
    private GameObject spawnPoint = null;

    [SerializeField]
    private Text timeKeeper = null;

    public Vector3 resetRotation = Vector3.zero;

    public void sendPlayerToSpawn()
    {
        // Move player to spawn
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.GetComponent<SpawnArea>().GetSpawnLocation();
        transform.rotation = Quaternion.Euler(resetRotation);
        GetComponent<CharacterController>().enabled = true;

        timeKeeper.GetComponent<UpdateTimeDisplay>().resetTimer();
    }
}
