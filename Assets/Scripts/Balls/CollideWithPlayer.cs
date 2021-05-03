using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{
    public GameObject explosionParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            resetPlayerAndBall(collision.gameObject);
        }
    }

    public void resetPlayerAndBall(GameObject player)
    {
        player.GetComponent<PlayerTeleporter>().sendPlayerToSpawn();

        Instantiate(explosionParticle, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
