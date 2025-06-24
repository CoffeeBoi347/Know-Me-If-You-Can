using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    public static PhotonNetworkManager instance;
    public Button joinRoom;
    [Header("Created Room")]

    public GameObject roomCreatedInstance;
    public Transform roomHolder;

    [Header("Joined Room")]

    public List<PlayerData> playerData;
    public GameObject playerNameData;
    public Transform playerHolder;

    public bool isReadyForRoomOperations = false;
    public Button joinRoomButton;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ConnectedToServer(string playerName)
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = playerName;
    }


    private void Update()
    {
        bool isInRoom = PhotonNetwork.InRoom;
        bool isInLobby = PhotonNetwork.InLobby;
        Debug.Log($"IN ROOM: {isInRoom}. IN LOBBY: {isInLobby}.");  
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("In game server now...");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby!");
        isReadyForRoomOperations = true;
        base.OnJoinedLobby();
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected to server!");
    }

    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        NotificationManager.instance.CreateNotification("Room creation failed!", DateTime.Now);
        Debug.Log(message);
    }

    public void OnCreateRoom(string roomName, RoomOptions roomOptions)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        GameManager.instance.OpenMenu(4);
    }

    public void JoinRoomButton()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        joinRoom.interactable = PhotonNetwork.IsMasterClient;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject obj = Instantiate(playerNameData, playerHolder);
            obj.GetComponent<PlayerData>().Setup(p.NickName);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach(Transform room in roomHolder)
        {
            Destroy(room.gameObject);
        }

        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                continue;
            }

            RoomData roomData = Instantiate(roomCreatedInstance, roomHolder.transform).GetComponent<RoomData>();
            roomData.roomName.text = room.Name.ToString();
            roomData.roomPlayersCount.text = $"{room.PlayerCount} / {room.MaxPlayers}";
            if (room.PlayerCount > room.MaxPlayers / 2)
            {
                roomData.RoomFillingFast();
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        Debug.Log("Master client switched!");
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject obj = Instantiate(playerNameData, playerHolder);
        obj.GetComponent<PlayerData>().Setup(newPlayer.NickName);
        NotificationManager.instance.CreateNotification($"{newPlayer.NickName} just joined the room!", DateTime.Now);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        NotificationManager.instance.CreateNotification($"You were disconnected! {cause}", DateTime.Now);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player left room!");
        NotificationManager.instance.CreateNotification($"{otherPlayer.NickName} just left the room!", DateTime.Now);
    }
}