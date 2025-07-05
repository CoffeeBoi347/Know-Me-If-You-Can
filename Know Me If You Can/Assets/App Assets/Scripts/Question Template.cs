using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionTemplate : MonoBehaviour
{
    public TMP_Text questionIndex;

    [Header("Input Fields")]

    public TMP_InputField questionName;
    public TMP_InputField optionA;
    public TMP_InputField optionB;
    public TMP_InputField optionC;
    public TMP_InputField optionD;
    public List<TMP_InputField> optionInputs;

    [Header("Main Button")]

    public Button nextQues;
    public Button prevQues;
    public List<Button> options = new List<Button> ();
    private void Start()
    {
        questionIndex.text = $"# QUESTION #";
        nextQues.onClick.AddListener(OnClickNextQuestion);
        prevQues.onClick.AddListener(OnClickPreviousQuestion);
        for(int i = 0; i < options.Count; i++)
        {
            int currentIndex = i;
            Debug.Log(i);
            options[currentIndex].onClick.AddListener(() => ButtonVal(currentIndex));
        }
    }

    public void OnClickNextQuestion()
    {
        int getIndex = CreateQuestionTemplate.instance._currentIndex;

        if(getIndex <= CreateQuestionTemplate.instance.sliderQuestions - 1)
        {
            Debug.Log(getIndex);
            CreateQuestionTemplate.instance.SaveData(optionA.text, optionB.text, optionC.text, optionD.text, questionName.text, options[getIndex], optionInputs[getIndex]);
            CreateQuestionTemplate.instance._currentIndex++;
        }

        if (CreateQuestionTemplate.instance._currentIndex >= CreateQuestionTemplate.instance.sliderQuestions - 1)
        {
            CreateQuestionTemplate.instance.OnSuccessPage();
            return;
        }
        CreateQuestionTemplate.instance.SwitchQuestions();
    }

    public void SaveQuestion()
    {
        List<string> stringList = new List<string>();

        foreach(var item in  optionInputs)
        {
            stringList.Add(item.text);
        }

        CreateQuestionTemplate.instance.AddQuestion(questionName.text, stringList);
    }

    public void OnClickPreviousQuestion()
    {
        CreateQuestionTemplate.instance._currentIndex--;

        if (CreateQuestionTemplate.instance._currentIndex < 0)
        {
            CreateQuestionTemplate.instance._currentIndex = CreateQuestionTemplate.instance.sliderQuestions - 1;
        }

        CreateQuestionTemplate.instance.SwitchQuestions();
    }

    public void ButtonVal(int index)
    {
        foreach (var button in options)
        {
            button.GetComponent<Button>().image.color = new Color32(255, 0, 0, 255);
        }
        options[index].GetComponent<Button>().image.color = new Color32(0, 255, 0, 255);

    }
}