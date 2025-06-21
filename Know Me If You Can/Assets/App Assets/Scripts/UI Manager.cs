using Photon.Realtime;
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
        GameManager.instance.OpenMenu(1);
    }

    public void OnCreatedRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = ((int)noOfPlayersSlider.value);
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.PlayerTtl = 10000; //ms
        roomOptions.EmptyRoomTtl = 0;

        PhotonNetworkManager.instance.OnCreateRoom(roomName, roomOptions);
        GameManager.instance.OpenMenu(4);
    }

    public void OpenCreateRoom()
    {
        GameManager.instance.OpenMenu(3);
    }

    public void OpenPublicRoom()
    {
        GameManager.instance.OpenMenu(2);
    }
}