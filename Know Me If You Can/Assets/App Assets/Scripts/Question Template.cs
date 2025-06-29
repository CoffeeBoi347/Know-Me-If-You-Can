using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class QuestionTemplate : MonoBehaviour
{
    public TMP_Text questionIndex;

    [Header("Input Fields")]

    public TMP_InputField questionName;
    public TMP_InputField optionA;
    public TMP_InputField optionB;
    public TMP_InputField optionC;
    public TMP_InputField optionD;

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
        if(CreateQuestionTemplate.instance._currentIndex >= CreateQuestionTemplate.instance.sliderQuestions)
        {
            CreateQuestionTemplate.instance._currentIndex = 0;
            CreateQuestionTemplate.instance.SwitchQuestions();
        }

        CreateQuestionTemplate.instance._currentIndex++;
        CreateQuestionTemplate.instance.SwitchQuestions();
    }

    public void OnClickPreviousQuestion()
    {
        if (CreateQuestionTemplate.instance._currentIndex < 0)
        {
            CreateQuestionTemplate.instance._currentIndex = CreateQuestionTemplate.instance.sliderQuestions - 1;
            CreateQuestionTemplate.instance.SwitchQuestions();
        }

        CreateQuestionTemplate.instance._currentIndex--;
        CreateQuestionTemplate.instance.SwitchQuestions();

    }

    public void ButtonVal(int index)
    {
        Debug.Log(index);
        foreach (var button in options)
        {
            button.GetComponent<Button>().image.color = new Color32(255, 0, 0, 255);
        }
        options[index].GetComponent<Button>().image.color = new Color32(0, 255, 0, 255);
    }
}