using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;
    public Queue<NotificationBaseClass> messages = new Queue<NotificationBaseClass>();
    public GameObject instanceNotification;
    public Transform instanceNotificationPosition;
    public Transform notificationHolder;
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

    public void CreateNotification(string message, DateTime time)
    {
        NotificationData notificationObj = Instantiate(instanceNotification, instanceNotificationPosition.transform).GetComponent<NotificationData>();
        notificationObj.SetupNotification(message, time.ToString());
        messages.Enqueue(new NotificationBaseClass(message, time.ToString()));
        if(notificationObj  != null)
        {
            Destroy(notificationObj, 1f);
        }

        LoadNotifications();
    }

    public void LoadNotifications()
    {
        foreach(var notification in messages)
        {
            var notifObject = Instantiate(instanceNotification, notificationHolder).GetComponent<NotificationData>();
            notifObject.SetupNotification(notification.message, notification.time);
        }
    }
}