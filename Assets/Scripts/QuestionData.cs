using System;
using System.Collections.Generic;

[Serializable]
public class AnswerData
{
    public string text;
    public int points;
}

[Serializable]
public class QuestionData
{
    public int questionNumber;
    public string questionText;
    public int noQuestions;
    public List<AnswerData> answers;
}

[Serializable]
public class QuestionDatabase
{
    public List<QuestionData> questions;
}
