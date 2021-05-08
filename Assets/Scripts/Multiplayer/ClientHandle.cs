using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        int myId = packet.ReadInt();
        string msg = packet.ReadString();

        Client.instance.myId = myId;
        Debug.Log($"Message from server: {msg}");

        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        GameManager.instance.packetCount((int)ServerPackets.welcome);
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
        GameManager.instance.packetCount((int)ServerPackets.spawnPlayer);
    }

    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        // DO NOT TURN A PLAYER THAT DOES NOT EXIST
        if (!GameManager.players.ContainsKey(id))
            return;

        GameManager.players[id].transform.position = position;
        GameManager.instance.packetCount((int) ServerPackets.playerPosition);
    }

    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        // DO NOT TURN A PLAYER THAT DOES NOT EXIST
        if (!GameManager.players.ContainsKey(id))
            return;

        GameManager.players[id].transform.rotation = rotation;
        GameManager.instance.packetCount((int)ServerPackets.playerRotation);
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();

        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
        GameManager.instance.packetCount((int)ServerPackets.playerDisconnected);
    }

    public static void PlayerHealth(Packet packet)
    {
        int id = packet.ReadInt();
        float health = packet.ReadFloat();

        GameManager.players[id].SetHealth(health);
        GameManager.instance.packetCount((int)ServerPackets.playerHealth);
    }

    public static void PlayerRespawned(Packet packet)
    {
        int id = packet.ReadInt();

        GameManager.players[id].Respawn();
        GameManager.instance.packetCount((int)ServerPackets.playerRespawned);
    }

    public static void SpawnLevelPiece(Packet packet)
    {
        Vector3 position = packet.ReadVector3();
        Vector3 size = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        string matName = packet.ReadString();

        LevelPiece piece = new LevelPiece(position, size, rotation);
        piece.materialName = matName;

        LevelLoad.instance.LoadPiece(piece);
        GameManager.instance.packetCount((int)ServerPackets.levelPieceSpawned);
    }

    public static void BallSpawn(Packet packet)
    {
        int id = packet.ReadInt();
        bool active = packet.ReadBool();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Vector3 scale = packet.ReadVector3();

        GameManager.instance.SpawnBall(id, active, position, rotation, scale);
        GameManager.instance.packetCount((int)ServerPackets.ballSpawn);
    }

    public static void BallActive(Packet packet)
    {
        int id = packet.ReadInt();
        bool active = packet.ReadBool();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        Vector3 scale = packet.ReadVector3();

        GameManager.instance.packetCount((int)ServerPackets.ballActive);

        // Do not update a ball that has NOT been spawned
        if (!GameManager.balls.ContainsKey(id))
            return;

        BallManager ball = GameManager.balls[id];
        ball.gameObject.SetActive(active);
        ball.transform.position = position;
        ball.transform.rotation = rotation;
        ball.transform.localScale = scale;
    }

    public static void BallRoll(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.packetCount((int)ServerPackets.ballRoll);

        // Do not update a ball that has NOT been spawned
        if (!GameManager.balls.ContainsKey(id))
            return;

        GameManager.balls[id].transform.position = position;
        GameManager.balls[id].transform.rotation = rotation;
    }

    public static void BallCollided(Packet packet)
    {
        Vector3 position = packet.ReadVector3();

        GameManager.instance.SpawnExplosionParticle(position);
    }
}
