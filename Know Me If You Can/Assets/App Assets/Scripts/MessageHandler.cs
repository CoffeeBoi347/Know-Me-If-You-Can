using Photon.Pun;
using UnityEngine;

public class MessageHandler : MonoBehaviourPun
{

    [Header("Game Objects")]

    public GameObject chatBoxObj;
    public Transform chatBoxHolder;

    [PunRPC]
    public void SetupMessage(string sender, string message)
    {
        ChatBox chatBox = Instantiate(chatBoxObj, chatBoxHolder.transform).GetComponent<ChatBox>();
        chatBox.playerName.text = sender;
        chatBox.message.text = message;
        Debug.Log("Setup!");
    }
}