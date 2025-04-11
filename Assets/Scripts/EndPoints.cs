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
    private int teamSelected = 0;
    [SerializeField]
    private bool warmupEneded = false, hasExecuted, teamselected;

    void Start()
    {
        warmUp = FindObjectOfType<WarmUp>();
        warmUp.OnWarmupModified += HandleEndWarmup;

    }
    public void AddPoints(TextMeshProUGUI team, int points)
    {
        if (team.text == "")
        {
            team.SetText(points.ToString());
        }
        int currentPoints = int.Parse(team.text);
        currentPoints += points;
        team.text = currentPoints.ToString();
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
