using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PacketDebug : MonoBehaviour
{
    public TMP_Text packetField;
    public GameObject packetPanel;

    // Update is called once per frame
    void Update()
    {
        ListenForToggle();

        UpdatePacketField();
    }

    void ListenForToggle()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            packetPanel.SetActive(!packetPanel.activeSelf);
        }
    }

    void UpdatePacketField()
    {
        if (!packetPanel.activeSelf)
        {
            return;
        }

        string packets = "";

        foreach (int packetId in Client.packetsPerSecond.Keys)
        {
            packets += $"{Enum.GetName(typeof(ServerPackets), packetId)}: {Client.packetsPerSecond[packetId]}\n";
        }

        packetField.text = packets;
    }
}
