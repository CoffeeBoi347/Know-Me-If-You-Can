using TMPro;
using Photon.Pun;

public class PlayerData : MonoBehaviourPun 
{
    public TMP_Text playerName;
    private void Start()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Setup", RpcTarget.AllBuffered, PhotonNetwork.NickName);
        }
    }

    [PunRPC]
    public void Setup(string nickname)
    {
        playerName.text = nickname;

    }
}
