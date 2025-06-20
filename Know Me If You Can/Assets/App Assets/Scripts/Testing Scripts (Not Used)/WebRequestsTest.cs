using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WebRequestsTest : MonoBehaviour
{
    public TMP_Text verdictText;

    private void Start()
    {
        StartCoroutine(RequestService());
    }

    private IEnumerator RequestService()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://www.google.com/");

        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Failed to catch result");
            verdictText.text = webRequest.error;
        }

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Successfully catched!");
            Debug.Log(webRequest.result);
            verdictText.text = webRequest.downloadHandler.text.Length.ToString(); // download manager manages the body data received from the server when we send the request
        }
    }
}