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
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        GameManager.players[id].transform.position = position;
    }

    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.players[id].transform.rotation = rotation;
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();

        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
    }

    public static void PlayerHealth(Packet packet)
    {
        int id = packet.ReadInt();
        float health = packet.ReadFloat();

        GameManager.players[id].SetHealth(health);
    }

    public static void PlayerRespawned(Packet packet)
    {
        int id = packet.ReadInt();

        GameManager.players[id].Respawn();
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
    }
}
