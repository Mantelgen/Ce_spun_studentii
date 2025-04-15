using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>(8);
    [SerializeField]
    public static int current_question=0,multiplication=1;
    [SerializeField]
    private int noOfGameObjects;
    [SerializeField]
    private float activationDelay;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] private TextAsset questionJson;
    public QuestionDataAsset questionDB;
   

    [SerializeField] private TextMeshProUGUI questionTextUI;


    public bool coroutineAllowed,loadQuestion;

    public int getCurrentQuestion()
    {
        return current_question;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitGame();
    }
    public void InitGame()
    {
        setLevel();
       
        Debug.Log("Current Question: " + current_question);
        
        coroutineAllowed = true;
        loadQuestion = false;
        activationDelay = 0.24f;
        //LoadQuestionDatabase();
        Debug.Log("Current Question number: " + questionDB.questions.Count);
        multiplication = questionDB.questions[current_question-1].multiplication;
        if (current_question <= questionDB.questions.Count)
        {
            LoadQuestion(current_question);
        }
        else
        {
            current_question = -1;
        }
        
    }
    public int getNoQuestion()
    {
        return noOfGameObjects;
    }

   public bool getLoaded()
    {
        return loadQuestion;
    }

    public int allQuestions()
    {
        return questionDB.questions.Count;
    }
    public List<GameObject> GetObjects()
    {
        return gameObjects;
    }
    

    public void LoadQuestion(int questionNumber)
    {
        if (questionDB == null) return;
        if (questionNumber < 0)
        { 
            return;
        }

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
                display.SetAnswer(question.answers[i].text, question.answers[i].points*question.multiplication );
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
    public void setLevel()
    {
        current_question++;
        
    }

   

}
