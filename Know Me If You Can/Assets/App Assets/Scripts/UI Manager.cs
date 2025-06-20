using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]

    public Button createNicknameButton;

    [Header("Values")]

    public TMP_InputField nickname;

    [Header("Holders")]

    public string nicknameHolder;

    private void Start()
    {
        createNicknameButton.onClick.AddListener(OpenHomePage);
    }

    void OpenHomePage()
    {
        GameManager.instance.OpenMenu(1);
    }
}