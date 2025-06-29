using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public ItemClass classItem;
}

public enum ItemClass
{
    CreateNickname,
    HomeMenu,
    CreateRoom,
    Public,
    WaitingRoom,
    MainHomeMenu,
    CreateQuestionsPage,
    QuestionsPage,
    ViewPosts,
    SuccessScreen,
    Notifications
}