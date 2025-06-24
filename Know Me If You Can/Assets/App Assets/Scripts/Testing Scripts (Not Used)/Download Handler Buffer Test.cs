using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadHandlerBufferTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetRequest("https://guthib.com/"));
    }

    // <summary>
    // this returns the HTML of the particular URL

    private IEnumerator GetRequest(string url)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "GET");
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Error: {webRequest.responseCode} | {webRequest.error}");
        }

        else
        {
            string json = webRequest.downloadHandler.text;
            Debug.Log(json);
        }
    }
}