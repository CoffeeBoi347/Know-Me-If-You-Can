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
    WaitingRoom
}