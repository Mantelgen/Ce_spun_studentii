using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GamePointsManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>(8);

    [SerializeReference]
    private ShowX showX;

    [SerializeField]
    private WarmUp warmUp;

    [SerializeField]
    private LoadGame loaded;

    [SerializeField]
    private bool[] bools;

    [SerializeField]
    private bool allowAdd = false, warmupEneded = false,canFlip=true,gameEneded=false,teamOneSelected;

    [SerializeField]
    int noOfGameObjects,wrongs;
    public event Action<bool[]> OnBoolsModified;
    public event Action<bool> OnTeamModfied;
    public event Action<int> TeamSelected;


    [SerializeField]
    public TextMeshProUGUI score;
    void Start()
    {
        noOfGameObjects = GetComponent<LoadGame>().getNoQuestion();
        showX = FindObjectOfType<ShowX>();
        bools = new bool[noOfGameObjects];
        for (int i = 0; i < noOfGameObjects; i++)
        {
            bools[i] = false;
        }
        canFlip = true;
        loaded = FindObjectOfType<LoadGame>();
        wrongs = 0;
        warmUp = FindObjectOfType<WarmUp>();
        warmUp.OnWarmupModified += HandleEndWarmup;
    }
    private bool hasExecuted = false;
    public void HandleEndWarmup(bool adev)
    {
        if (hasExecuted) return; // Prevent further execution if already executed
       warmupEneded = true;
       canFlip = false;
       
        Debug.Log("Selecteaza acum o echip1");
        hasExecuted = true;

    }
    private bool checkIfAllTrue()
    {
        for (int i = 0; i < noOfGameObjects; i++)
        {
            if (!bools[i])
            {
                return false;
            }
        }
        return true;
    }
    
    void Update()
    {
        if (Input.anyKeyDown && loaded.getLoaded() )
        {
            if(warmupEneded)
            {
                for (int i = 1; i <= noOfGameObjects; i++)
                {
                    if (Input.GetKeyDown(i.ToString()) && canFlip && !bools[i - 1])
                    {
                      
                        if (allowAdd)
                        {

                            addPoints(gameObjects[i - 1]);
                            OnBoolsModified?.Invoke(bools);
                        }
                        bools[i - 1] = true;
                        flip(gameObjects[i - 1]);
                        if (showX.wrongs == 3)
                        {
                            allowAdd = false;
                            teamOneSelected=!teamOneSelected;
                            gameEneded = true;
                        }
                        break;
                    }
                }
            }
            else if(!warmupEneded && canFlip)
            {
                for (int i = 1; i <= noOfGameObjects; i++)
                {
                    if (Input.GetKeyDown(i.ToString())  && !bools[i - 1])
                    {
                        
                         addPoints(gameObjects[i - 1]);
                         OnBoolsModified?.Invoke(bools);
                         bools[i - 1] = true;
                         flip(gameObjects[i - 1]);
                        break;
                    }
                }
            }
           
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)&& warmupEneded)
        {
            allowAdd = true;
            canFlip = true;
            teamOneSelected = true;
            TeamSelected.Invoke(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && warmupEneded)
        {
            allowAdd = true;
            canFlip = true;
            teamOneSelected = false;
            TeamSelected.Invoke(1);
        }
        if (checkIfAllTrue())
        {

            gameEneded = true;
        }
        if (showX.wrongs == 4)
        {
            gameEneded = true;
            //teamOneSelected = !teamOneSelected;
        }
        if (gameEneded)
        {
            Debug.Log("Game ended");
            OnTeamModfied.Invoke(teamOneSelected);
        }
       
    }
    private void flip(GameObject gameObject)
    {

        var script = gameObject.GetComponent<FlipScript>();
        StartCoroutine(script.Flip());
    }
    private void addPoints(GameObject gameObject)
    {

        
        // Find the TextMeshPro component in the child GameObjects
        TextMeshPro[] textComponents = gameObject.GetComponentsInChildren<TextMeshPro>();
        string points = "";
        foreach (TextMeshPro textComponent in textComponents)
        {
            if (textComponent.name == "Score")
            {
                points = textComponent.text.Trim();
                break;
            }
        }
        int currentPoints = int.Parse(points);
        if (score.text == "")
        {

            score.SetText(currentPoints.ToString());
        }
        else
        {
            int currentScore = int.Parse(score.text);
            currentScore += currentPoints;
            score.SetText(currentScore.ToString());
        }
    }
}
