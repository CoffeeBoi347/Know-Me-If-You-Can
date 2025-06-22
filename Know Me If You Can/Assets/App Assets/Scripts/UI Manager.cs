using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Buttons")]

    public Button createNicknameButton;
    public Button createRoomButton;

    [Header("Values")]

    public TMP_InputField nickname;
    public TMP_InputField roomName;
    public Slider noOfPlayersSlider;
    public Slider timeSlider;

    [Header("Holders")]

    public string nicknameHolder;
    public bool hasCreatedRoom = false;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("playerNickname"))
        {
            nickname.text = PlayerPrefs.GetString("playerNickname");
        }
        else
        {
            nickname.text = "Player_" + Random.Range(0000, 9999).ToString();
            PlayerPrefs.SetString("playerNickname", nickname.text);
        }
    }

    private void Start()
    {
        createNicknameButton.onClick.AddListener(OpenHomePage);
        createRoomButton.onClick.AddListener(() => OnCreatedRoom(roomName.text));
        SetupSliders();
    }

    public void SetupSliders()
    {
        noOfPlayersSlider.minValue = 2;
        noOfPlayersSlider.maxValue = 5;

        timeSlider.minValue = 1;
        timeSlider.maxValue = 5;
    }
    void OpenHomePage()
    {
        nicknameHolder = nickname.text;
        PhotonNetwork.NickName = nicknameHolder;
        GameManager.instance.OpenMenu(1);
        PhotonNetworkManager.instance.ConnectedToServer(PhotonNetwork.NickName);
    }

    public void OnCreatedRoom(string roomName)
    {
        if(PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            Debug.Log("Wait for client to perform the operation.");
            return;
        }

        StartCoroutine(CreatingRoom(roomName));
    }

    public void OpenCreateRoom()
    {
        GameManager.instance.OpenMenu(3);
    }

    public void OpenPublicRoom()
    {
        GameManager.instance.OpenMenu(2);
    }

    private IEnumerator CreatingRoom(string _roomName)
    {
        float timeout = 5f;
        float timer = 0f;

        while (!PhotonNetwork.InLobby && !hasCreatedRoom && timer < timeout)
        {
            timer += Time.deltaTime;
            Debug.Log("Out of lobby!");
            yield return null;
        }

        while (PhotonNetwork.InLobby)
        {
            Debug.Log("In lobby. Creating room now!");

            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = (byte)noOfPlayersSlider.value,
                IsVisible = true,
                IsOpen = true,
                PlayerTtl = 10000,
            };

            PhotonNetworkManager.instance.OnCreateRoom(_roomName, roomOptions);
            hasCreatedRoom = true;
            yield break;
        }
    }
}