using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    public TMP_Text roomName;
    public TMP_Text roomMasterClient;
    public TMP_Text roomPlayersCount;
    public Image roomImage;
    public Button roomJoin;

    private void Start()
    {
        roomJoin.onClick.AddListener(OnJoinRoom);
    }

    public void Setup(string _roomName, string _roomMasterClientOwner, int currentPlayers, int maxPlayers)
    {
        roomName.text = _roomName;
        roomMasterClient.text = _roomMasterClientOwner;
        roomPlayersCount.text = currentPlayers + "/" + maxPlayers;
    }

    // <summary> if we click on it, remember this is a prefab. So we need to JOIN THE ROOM!
    public void OnJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void RoomFillingFast()
    {
        roomImage.color = new Color32(255, 0, 0, 255);
    }
}