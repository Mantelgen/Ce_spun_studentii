using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class JsonLoader : MonoBehaviour
{

    public QuestionDatabase questionDataAsset;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //void LoadQuestionDatabase()
    //{
    //    if (questionJson != null)
    //    {
    //        questionDB = JsonUtility.FromJson<QuestionDatabase>(questionJson.text);
    //    }
    //    else
    //    {
    //        Debug.LogError("JSON file not assigned!");
    //    }
    //}

    public void LoadJson(string path)
    {
        string json = File.ReadAllText(path);
        QuestionDatabase db = JsonUtility.FromJson<QuestionDatabase>(json);

        if (db != null)
        {
            questionDataAsset.current_question = db.current_question;
            questionDataAsset.questions = db.questions;
            Debug.Log("ScriptableObject populated with JSON data.");
        }
    }
}
