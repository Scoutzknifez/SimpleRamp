using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float respawnTimeInSeconds = 3f;

    public MeshRenderer model;
    public GameObject deathRedPanel;
    public Animator animator;
    public GameObject[] deactivateOnDeath;
    public GameObject[] activateOnRespawn;

    public bool isDead = false;

    public void Die()
    {
        model.enabled = false;
        isDead = true;
        gameObject.GetComponent<CharacterController>().enabled = false;

        foreach (GameObject go in deactivateOnDeath)
        {
            go.SetActive(false);
        }

        PlayDeathFade();
        StartCoroutine(Respawn());
    }

    void PlayDeathFade()
    {
        deathRedPanel.SetActive(true);
        animator.Play("DeathRedIn", -1, 0f);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimeInSeconds);

        model.enabled = true;
        gameObject.GetComponent<PlayerTeleporter>().sendPlayerToSpawn();
        deathRedPanel.SetActive(false);

        foreach (GameObject go in activateOnRespawn)
        {
            go.SetActive(true);
        }

        isDead = false;
    }
}
