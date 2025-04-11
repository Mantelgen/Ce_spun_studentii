using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>(8);
    [SerializeField]
    private int noOfGameObjects;
    [SerializeField]
    private float activationDelay;
    [SerializeField]
    private AudioSource audioSource;

    


    public bool coroutineAllowed,loadQuestion;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coroutineAllowed = true;
        loadQuestion = false;
        activationDelay = 0.24f;
    }
    public int getNoQuestion()
    {
        return noOfGameObjects;
    }

   public bool getLoaded()
    {
        return loadQuestion;
    }
    public List<GameObject> GetObjects()
    {
        return gameObjects;
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
}
