using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, BallManager> balls = new Dictionary<int, BallManager>();

    public GameObject clientManager;

    [Header("Player Prefabs")]
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    [Header("Game Prefabs")]
    public GameObject ballPrefab;
    public GameObject explosionPrefab;

    [Header("Death Fade")]
    public GameObject deathRedPanel;
    public Animator animator;
    public GameObject[] deactivateOnDeath;
    public GameObject[] activateOnRespawn;

    [Header("Misc")]
    public GameObject toDisableOnStart;

    private void Awake()
    {
        if (instance == null)
        {
            Instantiate(clientManager);
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            Globals.localPlayer = _player;
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().usernameDisplay.text = _username;
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);

        toDisableOnStart.SetActive(false);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SpawnBall(int id, bool active, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        GameObject ball;
        ball = Instantiate(ballPrefab, pos, rot);

        ball.SetActive(active);
        ball.transform.localScale = scale;
        ball.GetComponent<BallManager>().Initialize(id);

        balls.Add(id, ball.GetComponent<BallManager>());
    }

    public void SpawnExplosionParticle(Vector3 position)
    {
        Instantiate(explosionPrefab, position, Quaternion.identity);
    }

    public void PlayDeathFade()
    {
        foreach (GameObject go in deactivateOnDeath)
        {
            go.SetActive(false);
        }

        deathRedPanel.SetActive(true);
        animator.Play("DeathRedIn", -1, 0f);
    }

    public void ClearDeathFade()
    {
        deathRedPanel.SetActive(false);

        foreach (GameObject go in activateOnRespawn)
        {
            go.SetActive(true);
        }
    }

    public void packetCount(int id)
    {
        Client.packetsPerSecond[id]++; 

        StartCoroutine(decrementPacketCount(id));
    }

    IEnumerator decrementPacketCount(int id)
    {
        yield return new WaitForSeconds(1);

        Client.packetsPerSecond[id]--;
    }
}
