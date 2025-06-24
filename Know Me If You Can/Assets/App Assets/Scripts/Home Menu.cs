using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] private Button playButton;
    [SerializeField] private Button createButton;
    [SerializeField] private Button viewButton;

    private void Start()
    {
        playButton.onClick.AddListener(ClickPlayButton);
        createButton.onClick.AddListener(ClickCreateButton);
        viewButton.onClick.AddListener(ClickViewButton);    
    }

    void ClickPlayButton()
    {
        GameManager.instance.OpenMenu(0);
        NotificationManager.instance.CreateNotification("Opened Main Home Page!", DateTime.Now);
    }

    void ClickCreateButton()
    {
        GameManager.instance.OpenMenu(6);
        NotificationManager.instance.CreateNotification("Clicked On Creating Questions", DateTime.Now);
    }

    void ClickViewButton()
    {
        GameManager.instance.OpenMenu(8);
    }
}