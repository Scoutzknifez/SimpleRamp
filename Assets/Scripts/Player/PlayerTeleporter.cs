using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If player falls, put them back here.")]
    private GameObject spawnPoint = null;

    [SerializeField]
    private Timer timer = null;

    public Vector3 resetRotation = Vector3.zero;

    public void sendPlayerToSpawn()
    {
        // Move player to spawn
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.GetComponent<SpawnArea>().GetSpawnLocation();
        transform.rotation = Quaternion.Euler(resetRotation);
        GetComponent<CharacterController>().enabled = true;

        timer.resetTimer();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.collider.gameObject.CompareTag("Ball"))
        {
            return;
        }

        // We are hitting a ball
        GameObject ball = hit.collider.gameObject;
        ball.GetComponent<CollideWithPlayer>().resetPlayerAndBall(gameObject);
    }
}
