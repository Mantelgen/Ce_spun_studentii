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
    private float activationDelay = 0.002f;
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            StartCoroutine(ActivateObjectsWithDelay());
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
