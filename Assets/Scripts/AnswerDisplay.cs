using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[System.Serializable]
public class AnswerDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro answerText;
    [SerializeField]
    private TextMeshPro pointText;

    private void Start()
    {
        // Assumes first child = answer, second child = points
        answerText = transform.GetChild(0).GetComponent<TextMeshPro>();
        pointText = transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    public void SetAnswer(string answer, int points)
    {
        if (answerText != null) answerText.text = answer;
        if (pointText != null) pointText.text = points.ToString();
    }
    public void ResetAnswer()
    {
        this.answerText.text = "";
        this.pointText.text = "";
    }
}

