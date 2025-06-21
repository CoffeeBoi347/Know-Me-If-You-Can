using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    public static PhotonNetworkManager instance;

    [Header("Created Room")]

    public GameObject roomCreatedInstance;
    public Transform roomHolder;

    [Header("Joined Room")]

    public List<PlayerData> playerData;
    public GameObject playerNameData;
    public Transform playerHolder;

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

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby(); // join the master lobby
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public void OnCreateRoom(string roomName, RoomOptions roomOptions)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject roomObj = PhotonNetwork.Instantiate(roomCreatedInstance.name, roomHolder.transform.position, Quaternion.identity);
            var roomObj_roomData = roomObj.GetComponent<RoomData>();
            roomObj.transform.SetParent(roomHolder, false); // <summary> false for world position
            if (roomObj_roomData != null)
            {
                var masterClientOwner = PhotonNetwork.MasterClient.NickName;
                roomObj_roomData.Setup(UIManager.instance.roomName.text, masterClientOwner, PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);
            }

            if(PhotonNetwork.CurrentRoom.PlayerCount > PhotonNetwork.CurrentRoom.MaxPlayers / 2)
            {
                roomObj_roomData.RoomFillingFast();
            }
        }

        GameObject playerObject = PhotonNetwork.Instantiate(playerNameData.name, playerHolder.transform.position, Quaternion.identity);
        playerObject.transform.SetParent(playerHolder, false); // <summary> false for world position
        var playerObject_playerData = playerObject.GetComponent<PlayerData>();

        if(playerObject_playerData != null)
        {
            playerObject_playerData.Setup();
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        Debug.Log("Master client switched!");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Player entered room!");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player left room!");
    }
}