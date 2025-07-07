using System.Collections.Generic;

[System.Serializable]
public class QuestionsHolder
{
    public List<QuestionData> questions = new List<QuestionData>();
    public string quesID;
    public string quesDesc;
}