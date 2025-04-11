using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>(8);
    [SerializeField]
    private int noOfGameObjects;
    [SerializeField]
    private float activationDelay;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] private TextAsset questionJson;
    private QuestionDatabase questionDB;

    [SerializeField] private TextMeshProUGUI questionTextUI;


    public bool coroutineAllowed,loadQuestion;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coroutineAllowed = true;
        loadQuestion = false;
        activationDelay = 0.24f;
        LoadQuestionDatabase();
        LoadQuestion(1);
    }
    public int getNoQuestion()
    {
        return noOfGameObjects;
    }

   public bool getLoaded()
    {
        return loadQuestion;
    }
    public List<GameObject> GetObjects()
    {
        return gameObjects;
    }
    void LoadQuestionDatabase()
    {
        if (questionJson != null)
        {
            questionDB = JsonUtility.FromJson<QuestionDatabase>(questionJson.text);
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }
    }

    public void LoadQuestion(int questionNumber)
    {
        if (questionDB == null) return;

        var question = questionDB.questions.Find(q => q.questionNumber == questionNumber);
        if (question == null)
        {
            Debug.LogError($"Question #{questionNumber} not found!");
            return;
        }

        // Set question text
        questionTextUI.text = question.questionText;
        Debug.Log(question.noQuestions.ToString());
        noOfGameObjects = int.Parse(question.noQuestions.ToString());

        // Instantiate only up to noQuestions
        for (int i = 0; i < question.noQuestions && i < question.answers.Count; i++)
        {
            var display = gameObjects[i].GetComponent<AnswerDisplay>();
            if (display != null)
            {
                display.SetAnswer(question.answers[i].text, question.answers[i].points);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("l") && coroutineAllowed)
        {
            StartCoroutine(ActivateObjectsWithDelay());
            coroutineAllowed = false;
           
        }
        if(Input.GetKeyDown("p") && !coroutineAllowed)
        {
            loadQuestion = true;
        }
    }

    IEnumerator ActivateObjectsWithDelay()
    {
        for (int i = 0; i < noOfGameObjects; i++)
        {
            if (i < gameObjects.Count) // Prevent out-of-range errors
            {
                gameObjects[i].SetActive(true);
                if (i == noOfGameObjects - 1 || activationDelay >= 0.01f)
                {
                    audioSource.Play();
                }

                yield return new WaitForSeconds(activationDelay);
            }
        }
    }
}
