using TMPro;
using UnityEngine;

public class QuestionsData : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    public TMP_Text optionD;
    public int correctAnswer;

    public void Setup(string question, string _optionA, string _optionB, string _optionC, string _optionD, int correctIndex)
    {
        questionText.text = question;
        optionA.text = _optionA;
        optionB.text = _optionB;
        optionC.text = _optionC;
        optionD.text = _optionD;
        correctAnswer = correctIndex;   
    }
}
