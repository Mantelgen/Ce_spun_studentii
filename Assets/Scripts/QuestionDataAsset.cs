using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionDataAsset", menuName = "Game/Question Data Asset")]
public class QuestionDataAsset : ScriptableObject
{
    public int current_question;
    public List<QuestionData> questions;
}
