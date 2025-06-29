using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateQuestionTemplate : MonoBehaviour
{
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

    public string[] questions;

    [Header("UI Elements")]

    [SerializeField] private Button onSuccessButton;
    [SerializeField] private TMP_InputField questionsID;
    [SerializeField] private TMP_InputField questionsDesc;
    [SerializeField] private Slider questionsSlider;

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

    // <summary> switches questions everytime index changes
    public void SwitchQuestions()
    {
        if (_currentIndex >= queueHolder.Count)
        {
            GameManager.instance.OpenMenu(9);
            return;
        }

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

}