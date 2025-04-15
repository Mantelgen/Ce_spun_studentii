using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePointsManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>(8);

    public static int team1Score { get; private set; }
    public static int team2Score { get; private set; }

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private TextMeshProUGUI team1ScoreText, team2ScoreText;

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
    int noOfGameObjects;
    public event Action<bool[]> OnBoolsModified;
    public event Action<bool> OnTeamModfied;
    public event Action<int> TeamSelected;


    [SerializeField]
    public TextMeshProUGUI score;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        noOfGameObjects = GetComponent<LoadGame>().getNoQuestion();
        showX = FindObjectOfType<ShowX>();
        bools = new bool[noOfGameObjects];
        for (int i = 0; i < noOfGameObjects; i++)
        {
            bools[i] = false;
        }
        canFlip = true;
        loaded = FindObjectOfType<LoadGame>();
       
        warmUp = FindObjectOfType<WarmUp>();
        warmUp.OnWarmupModified += HandleEndWarmup;
        team1ScoreText.text = team1Score.ToString();
        team2ScoreText.text = team2Score.ToString();
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

    public void ModifyTeam1(int points)
    {
        team1Score += points;
    }
    public void ModifyTeam2(int points)
    {
        team2Score += points;
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
            nextButton.gameObject.SetActive(true);
        }

    }
    public void OnNextButtonClicked()
    {
        if (gameEneded)
        {
            StartCoroutine(EndGameAndLoadNextQuestion());
        }
    }
    private IEnumerator EndGameAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(1f); // Optional delay
        ReloadScene();
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

    public void ReloadScene()
    {
        Debug.Log("Reloading scene "+ loaded.allQuestions() + " "+ loaded.getCurrentQuestion());

        if (loaded.getCurrentQuestion()==loaded.allQuestions())
        {
            SceneManager.LoadScene("TransitionScene");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
        
            
        
    }

   
    
}
