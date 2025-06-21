using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public TMP_Text playerName;

    public void Setup()
    {
        playerName.text = UIManager.instance.nicknameHolder;
    }
}