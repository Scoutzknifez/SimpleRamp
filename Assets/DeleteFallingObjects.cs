using UnityEngine;

public class DeleteFallingObjects : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If player falls, put them back here.")]
    private Transform spawnPoint = null;

    [SerializeField]
    [Tooltip("If a player touches this, should they go back to spawn.")]
    private bool sendToSpawn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!sendToSpawn)
                return;

            // Move player to spawn
            other.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = spawnPoint.position;
            other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            other.GetComponentInChildren<CharacterController>().enabled = true;
        } else {
            Destroy(other.gameObject);
        }
    }
}
