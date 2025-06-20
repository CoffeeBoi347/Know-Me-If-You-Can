using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<MenuItem> menuItems = new List<MenuItem>();

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
        OpenMenu(0);
    }

    public void OpenMenu(int index)
    {
        CloseMenu();

        foreach (var item in menuItems)
        {
            if(item == menuItems[index])
            {
                var itemCanvasGroup = GetCanvasGroup(item);
                itemCanvasGroup.alpha = 1;
                itemCanvasGroup.interactable = true;
                itemCanvasGroup.blocksRaycasts = true;
            }
        }
    }

    public void CloseMenu()
    {
        foreach(var item in menuItems)
        {
            var itemCanvasGroup = GetCanvasGroup(item);
            itemCanvasGroup.alpha = 0;
            itemCanvasGroup.interactable = false;
            itemCanvasGroup.blocksRaycasts = false;
        }
    }

    public CanvasGroup GetCanvasGroup(MenuItem item)
    {
        if(item.GetComponent<CanvasGroup>() == null)
        {
            item.AddComponent<CanvasGroup>();
        }

        var itemCanvasGroup = item.GetComponent<CanvasGroup>();
        return itemCanvasGroup;
    }
}