using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class ShowX : MonoBehaviour
{

    [SerializeField]
    private GameObject oneWrong, twoWrong, threeWrong, backGround,mainGame;

    [SerializeField]
    public int wrongs;

    [SerializeField]
    private AddPoints script;


    [SerializeField]
    private GamePointsManager manager;
 

    [SerializeField]
    private bool warmup;

    [SerializeField]
    private AudioSource audioSource;

    public bool coroutineAllowed;

    public bool gameBreak = false, allowed = false;
  
    
    // Start is called before the first frame update
    void Start()
    {
        wrongs = 0;
        mainGame = GameObject.Find("Main Game");
        oneWrong = mainGame.transform.Find("One Wrong").gameObject;
        twoWrong = mainGame.transform.Find("Two_Wrong").gameObject;
        threeWrong = mainGame.transform.Find("Three_Wrong").gameObject;
        backGround = mainGame.transform.Find("Transparent_background").gameObject;
        script = FindObjectOfType<AddPoints>();
        warmup = true;
        manager = FindObjectOfType<GamePointsManager>();
        coroutineAllowed = false;
        manager.TeamSelected += HandleTeamSelected;
    }

    private IEnumerator HandleOneCoroutine(GameObject go)
    {
        coroutineAllowed = false;
        backGround.SetActive(true);
        go.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        backGround.SetActive(false);
        go.SetActive(false);
        coroutineAllowed = true;
    }
    private void handleWrongs()
    {
        switch(wrongs)
        {
            case 0:
                StartCoroutine(HandleOneCoroutine(oneWrong));
                wrongs++;
                break;
            case 1:
                StartCoroutine(HandleOneCoroutine(twoWrong));
                wrongs++;
                break;
            case 2:
                StartCoroutine(HandleOneCoroutine(threeWrong));
                wrongs++;
                break;
            default:
                break;
        }
    }
    public void HandleTeamSelected(int war)
    {
        warmup = false;
        coroutineAllowed = true;
        return;
    }
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space) && coroutineAllowed)
            {
                if (wrongs < 3)
                    handleWrongs();
                else if (wrongs == 3)
                {
                    StartCoroutine(HandleOneCoroutine(oneWrong));
                    wrongs++;
                    script.noAllowAdd();
                }

            }
            if (Input.GetKeyDown("-") && coroutineAllowed && wrongs > 0 && !warmup)
            {
                wrongs -= 1;
            }

    }
 
   
    
}
