using TMPro;
using UnityEngine;

public class NotificationData : MonoBehaviour
{
    public TMP_Text notificationMessage;
    public TMP_Text notificationTime;

    public NotificationData SetupNotification(string message, string time)
    {
        notificationMessage.text = message;
        notificationTime.text = time;
        return this;
    }
}