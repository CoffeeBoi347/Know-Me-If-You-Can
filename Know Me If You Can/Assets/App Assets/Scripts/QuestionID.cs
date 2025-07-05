using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionID : MonoBehaviour
{
    public TMP_Text questionID;
    public TMP_Text questionDesc;
    public Button viewQuestions;

    public GameObject instantiateObj;
    public Transform holder;
    private void Start()
    {
        viewQuestions.onClick.AddListener(ViewQuestion);
    }

    public void Setup(string _questionID, string _questionDesc) 
    {
        questionID.text = _questionID;
        questionDesc.text = _questionDesc;
    }

    public void ViewQuestion()
    {
        GameManager.instance.OpenMenu(13);
        CreateQuestionTemplate.instance.InstantiateQuestions();
    }
}