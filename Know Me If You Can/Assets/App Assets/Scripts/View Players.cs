using Photon.Pun;
using TMPro;

public class ViewPlayers : MonoBehaviourPunCallbacks
{
    public TMP_Text roomPlayersList;
    private void Start()
    {
        Setup();
    }
    void Setup()
    {
        foreach(var player in PhotonNetwork.PlayerList)
        {
            int count = 0;
            count++;
            roomPlayersList.text = $"{count}: {player.NickName}";
        }
    }
}
