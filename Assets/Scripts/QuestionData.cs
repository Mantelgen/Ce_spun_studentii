using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnswerData
{
    public string text;
    public int points;
}

[Serializable]
public class QuestionData
{
    public int multiplication;
    public int questionNumber;
    public string questionText;
    public int noQuestions;
    public List<AnswerData> answers;
}

[Serializable]
public class QuestionDatabase
{
    
    public int current_question;
    public List<QuestionData> questions;
}



