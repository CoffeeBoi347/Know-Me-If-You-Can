using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateQuestionTemplate : MonoBehaviour
{
    public Dictionary<string, List<string>> questionTemplates = new Dictionary<string, List<string>>();
    [Header("Instance")]

    public static CreateQuestionTemplate instance;
    [Header("Strings")]

    public string questionIDTxtStr;
    public string questionDescStr;
    public int sliderQuestions;

    [Header("Holders")]

    [SerializeField] private Canvas canvasHolder;
    [SerializeField] private Transform canvasHolderParent;
    [SerializeField] private List<GameObject> queueHolder;
    public int _currentIndex;
    public List<QuestionData> questionDataHolder = new List<QuestionData>();
    public Transform holder;
    public GameObject instantiateObj;

    public string[] questions;

    [Header("UI Elements")]

    [SerializeField] private Button onSuccessButton;
    [SerializeField] private TMP_InputField questionsID;
    [SerializeField] private TMP_InputField questionsDesc;
    [SerializeField] private Slider questionsSlider;

    [Header("Instantiate Questions")]

    public GameObject questionIDObj;
    public Transform questionsHolder;
//  public QuestionsHolder questionsHolderVal;

//  [Header("Options")]
//  public List<string> questionsData = new List<string>();
//  public List<string> optionAData = new List<string>();
//  public List<string> optionBData = new List<string>();
//  public List<string> optionCData = new List<string>();
//  public List<string> optionDData = new List<string>();
//  public Dictionary<Button, TMP_InputField> correctOptions = new Dictionary<Button, TMP_InputField>();
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(questionsSlider != null)
        {
            questionsSlider.minValue = 5;
            questionsSlider.maxValue = 15;
        }

        onSuccessButton.onClick.AddListener(OnHomePage);
        InstantiateQuestion();
    }

    public void AddQuestion(string question, List<string> options)
    {
        questionTemplates.Add(question, options);
    }
    #region setup

    // <summary> sets up the question template page and goes to create question
    public void Setup()
    {
        questionIDTxtStr = questionsID.text + Random.Range(0, 9999);
        questionDescStr = questionsDesc.text;
        sliderQuestions = ((int)questionsSlider.value);

      //GameManager.instance.OpenMenu(7);
        InstantiateCanvas(canvasHolder);
        this.gameObject.SetActive(false);
    }

    public void InstantiateQuestion()
    {
        GameObject questionObj = Instantiate(questionIDObj, questionsHolder);
        questionObj.GetComponent<QuestionID>().Setup(questionIDTxtStr, questionDescStr);

        if(questionObj.GetComponent<QuestionID>() == null)
        {
            NotificationManager.instance.CreateNotification("Question ID - NOT FOUND!", System.DateTime.Now);
        }
    }

    #endregion

    // <summary> on the value of slider questions, instantiate each canvas. 
    private void InstantiateCanvas(Canvas CanvasObj)
    {
        foreach(var obj in queueHolder)
        {
            Destroy(obj);
        }

        for(int i = 0; i < questionsSlider.value; i++)
        {
            GameObject InstantiateObj = Instantiate(CanvasObj.gameObject, canvasHolderParent.transform);
            queueHolder.Add(InstantiateObj);
        }

        SwitchQuestions();
    }

    #region saving and loading

//    public void SaveQuestions()
//    {
//        DataHolder dataHolder = new DataHolder();
//        QuestionsHolder holder = new QuestionsHolder();
//        holder.questions = questionDataHolder;
//        dataHolder.questionsHolder.Add(holder);
//        string json = JsonUtility.ToJson(holder, true);
//        System.IO.File.WriteAllText(Application.persistentDataPath + "/dataHolder.json", json);
//    }

    #endregion

    // <summary> switches questions everytime index changes
    public void SwitchQuestions()
    {
        foreach (var obj in queueHolder)
        {
            var getCanvasGroup = obj.gameObject.GetComponent<CanvasGroup>();
            getCanvasGroup.alpha = 0;
            getCanvasGroup.interactable = false;
            getCanvasGroup.blocksRaycasts = false;

            var getCanvasGroupIndex = queueHolder[_currentIndex].gameObject.GetComponent<CanvasGroup>();
            getCanvasGroupIndex.alpha = 1;
            getCanvasGroupIndex.interactable = true;
            getCanvasGroupIndex.blocksRaycasts = true;
        }
    }

    private void OnHomePage()
    {
        GameManager.instance.OpenMenu(0);

        foreach(var obj in queueHolder)
        {
            Destroy(obj);
        }
    }

    public void OnSuccessPage()
    {
        GameManager.instance.OpenMenu(9);
        foreach(var ques in queueHolder)
        {
            ques.SetActive(false);
        }
        InstantiateQuestion();
    }

    public void InstantiateQuestions()
    {
        for (int i = 0; i < sliderQuestions - 1; i++)
        {
            GameObject question = Instantiate(instantiateObj, holder);
            QuestionsData questionData = question.GetComponent<QuestionsData>();
            if (questionData == null)
            {
                Debug.LogError("Component not found!");
                return;
            }

            var questionVal = questionDataHolder[i].question;
            var optionA = questionDataHolder[i].optionA;
            var optionB = questionDataHolder[i].optionB;
            var optionCData = questionDataHolder[i].optionC;
            var optionDData = questionDataHolder[i].optionD;
            var correctIndex = questionDataHolder[i].correctIndex;
            questionData.Setup(questionVal, optionA, optionB, optionCData, optionDData, correctIndex);
        }
    }

    public void SaveData(string item1, string item2, string item3, string item4, string questionInput, int index)
    {
        QuestionData questionData = new QuestionData()
        {
            question = questionInput,
            optionA = item1,
            optionB = item2,
            optionC = item3,
            optionD = item4,
            correctIndex = index,
        };

        questionDataHolder.Add(questionData);
        QuestionsHolder questionHolder_ = new QuestionsHolder();
        questionHolder_.questions.Add(questionData);
        questionHolder_.quesID = questionIDTxtStr;
        questionHolder_.quesDesc = questionDescStr;
    }

}