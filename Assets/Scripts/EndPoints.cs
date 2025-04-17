using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;


using UnityEngine;

public class EndPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI team1, team2,score;
    [SerializeField]
    private WarmUp warmUp;

    

    [SerializeField]
    private GamePointsManager mainGame;

    [SerializeField]
    private int teamSelected = 0;
    [SerializeField]
    private bool warmupEneded = false, hasExecuted, teamselected, added;

    void Start()
    {
        warmUp = FindObjectOfType<WarmUp>();
        warmUp.OnWarmupModified += HandleEndWarmup;
        mainGame = FindObjectOfType<GamePointsManager>();
        mainGame.OnTeamModfied += MainGame_OnBoolsModified;
    }

    

    private IEnumerator UpdateScoreWithFade(TextMeshProUGUI team, int pointsToAdd)
    {
        float fadeDuration = 0.5f;
        Color originalColor = team.color;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            team.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        team.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // Update text
        int currentPoints = string.IsNullOrEmpty(team.text) ? 0 : int.Parse(team.text);
        currentPoints += pointsToAdd;
        team.text = currentPoints.ToString();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            team.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        team.color = originalColor;
        
    }
    private void MainGame_OnBoolsModified(bool obj)
    {
        
        if (added) return;
        if (obj)
        { 
            AddPoints(team1, int.Parse(score.text));
            mainGame.ModifyTeam1(int.Parse(score.text));
        }
        else
        {
            AddPoints(team2, int.Parse(score.text));
            mainGame.ModifyTeam2(int.Parse(score.text));
        }

        added = true;
        return;

    }

    public void AddPoints(TextMeshProUGUI team, int points)
    {
        StartCoroutine(UpdateScoreWithFade(team, points));
    }

    public void HandleEndWarmup(bool adev)
    {
        if (hasExecuted) return; // Prevent further execution if already executed
        warmupEneded = true;
        hasExecuted = true;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && warmupEneded && !teamselected)
        {
            teamSelected = 1;
            teamselected = true;
            Debug.Log("Team 1");

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && warmupEneded && !teamselected)
        {
            teamSelected = 2;
            teamselected = true;
            Debug.Log("Team 2");
        }
    }
}
