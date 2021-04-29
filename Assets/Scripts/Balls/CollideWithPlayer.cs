using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{
    public GameObject explosionParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerTeleporter>().sendPlayerToSpawn();

            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
