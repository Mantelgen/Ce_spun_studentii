using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WarmUp : MonoBehaviour
{
    [SerializeField]
    private bool warmup, coroutineAllowed;

    [SerializeField]
    private GamePointsManager endPoints;

    [SerializeField]
    private GameObject oneWrong,backGround,mainGame;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private LoadGame LoadGame;

    [SerializeField]
    int ans = 0,wrongs=0;


    public event Action<bool> OnWarmupModified;

    void Start()
    {
        endPoints = FindObjectOfType<GamePointsManager>();
        LoadGame = FindObjectOfType<LoadGame>();
        mainGame = GameObject.Find("Main Game");
        oneWrong = mainGame.transform.Find("One Wrong").gameObject;
        warmup = true;
        backGround = mainGame.transform.Find("Transparent_background").gameObject;
        endPoints.OnBoolsModified += HandleBoolsModified;
        coroutineAllowed = false;
    }

   

    private void HandleBoolsModified(bool[] updatedBools)
    {
        if (ans < 2 && LoadGame.getLoaded())
        {
            ans++;
        }
    }

    private void handleWarmup()
    {
        if (ans == 2)
        {
            warmup = false;
            OnWarmupModified.Invoke(warmup);
            coroutineAllowed = false;
        }
        if(ans==1 && wrongs!=0)
        {
            warmup = false;
            OnWarmupModified.Invoke(warmup);
            coroutineAllowed = false;
        }
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
    void Update()
    {
        
        if(LoadGame.getLoaded())
            coroutineAllowed = true;
        if (Input.GetKeyDown(KeyCode.Space) && warmup && coroutineAllowed)
        {
            wrongs++;
            StartCoroutine(HandleOneCoroutine(oneWrong));
        }
        handleWarmup();
    }

    public bool getWarmup()
    {
        return warmup;
    }
}
