using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    [SerializeField] private ShowX scriptX;

    [SerializeField] private TextMeshProUGUI team1, team2;

    [SerializeField] private Animator anim1, anim2;

    private bool isTeam1Selected;
    // Track which team is active
    private bool play = true;

    void Start()
    {
        anim1 = team1.GetComponent<Animator>();
        anim2 = team2.GetComponent<Animator>();
        play = true;
        isTeam1Selected = true; // Default selection
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && scriptX.isLoaded && !isTeam1Selected)
        {
            SelectTeam1();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && scriptX.isLoaded && isTeam1Selected)
        {
            SelectTeam2();
        }
        if (scriptX.wrongs == 3 && play)
        {
            play = false;
            PlayChangeAnimation();
        }
    }

    private void SelectTeam1()
    {
        anim1.SetBool("isTeam1Selected", true); // Highlight Team 1
        anim2.SetBool("isTeam1Selected", false); // Remove highlight from Team 2
        isTeam1Selected = true; // Now Team 1 is selected
    }

    private void SelectTeam2()
    {
        anim1.SetBool("isTeam1Selected", false); // Remove highlight from Team 1
        anim2.SetBool("isTeam1Selected", true);  // Highlight Team 2
        isTeam1Selected = false; // Now Team 2 is selected
    }

    private void PlayChangeAnimation()
    {
        // Reverse highlight for current team, highlight the other
        if (isTeam1Selected)
        {
            anim1.SetBool("isTeam1Selected", false);
            anim2.SetBool("isTeam1Selected", true);
        }
        else
        {
            anim1.SetBool("isTeam1Selected", true);
            anim2.SetBool("isTeam1Selected", false);
        }
    }
}
