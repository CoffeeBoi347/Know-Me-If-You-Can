using Photon.Pun;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineChat : MonoBehaviourPun
{
    public GameObject onlineChatCanvas;
    public static event Action OnHandler;
    public MessageHandler messageHandler;
    public TMP_InputField chatBoxText;

    [Header("Buttons")]

    public Button closeButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            onlineChatCanvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("Attempting to send message...");
            Debug.Log("PhotonView is null: " + (messageHandler.photonView == null));
            messageHandler.photonView.RPC("SetupMessage", RpcTarget.All, PhotonNetwork.NickName, chatBoxText.text);
            chatBoxText.text = string.Empty;
        }

    }

    public void OnClickCloseButton()
    {
        onlineChatCanvas.SetActive(false);
    }
}