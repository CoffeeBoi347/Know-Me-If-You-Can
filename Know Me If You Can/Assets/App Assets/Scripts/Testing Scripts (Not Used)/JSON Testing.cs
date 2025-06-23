using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class JSONTesting : MonoBehaviour
{
    private void Start()
    {
        TestData testData = new TestData();
        testData.name = "aayush";
        testData.age = 18;
        testData.power = 100;

        string convertToJson = JsonUtility.ToJson(testData); // converting to json
        File.WriteAllText(Application.dataPath + "/convertToJson.json", convertToJson); // writing to json
        Debug.Log("IT EXISTS!");
        LoadData();
    }

    public void LoadData()
     // <summary>
     // PostRequest asks for a string which means object must be in STRING FORMAT, 
    {
        string fromJson = File.ReadAllText(Application.dataPath + "/convertToJson.json"); // reading the json text
        TestData testData = JsonUtility.FromJson<TestData>(fromJson); // accepting from json
        Debug.Log(testData.name);
        Debug.Log(testData.age);
        Debug.Log(testData.power);

        string toJson = JsonUtility.ToJson(testData);
        StartCoroutine(GetRequest("https://webhook.site/c8cfb964-b44c-4159-a5d5-764e517f8aaf"));
        StartCoroutine(PostRequest("https://webhook.site/c8cfb964-b44c-4159-a5d5-764e517f8aaf", toJson));
   //     DeleteData(Application.dataPath + "/convertToJson.json");
    }

    private IEnumerator GetRequest(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"GETREQUEST | ERROR {request.responseCode} : {request.error}");
        }

        else
        {
            Debug.Log($"GETREQUEST | RESULT: {request.result}");
        }
    }

    private IEnumerator PostRequest(string path, string jsonBody)
    {
        byte[] rawBytes = Encoding.UTF8.GetBytes(jsonBody);
        UnityWebRequest request = new UnityWebRequest(path, "POST");
        request.uploadHandler = new UploadHandlerRaw(rawBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"POSTREQUEST | ERROR {request.responseCode} : {request.error}");
        }

        else
        {
            Debug.Log($"POSTREQUEST | RESULT: {request.result}");
        }
    }

    public void DeleteData(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Deleted path!");
        }
    }
}