using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonToScriptableObjectLoader : MonoBehaviour
{
    public QuestionDataAsset questionDataAsset; // Assign in Inspector
    public string fileName;  // Path inside StreamingAssets

    void Start()
    {
        LoadJsonIntoAsset();
    }

    public void LoadJsonIntoAsset()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            QuestionDatabase tempData = JsonUtility.FromJson<QuestionDatabase>(json);

            if (tempData != null && tempData.questions != null)
            {
                questionDataAsset.current_question = tempData.current_question;
                questionDataAsset.questions = tempData.questions;
                Debug.Log("Loaded JSON into ScriptableObject: " + questionDataAsset.questions.Count + " questions.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found at: " + path);
        }
    }
}
