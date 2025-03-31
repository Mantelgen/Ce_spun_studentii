using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowX : MonoBehaviour
{

    [SerializeField]
    private GameObject oneWrong, twoWrong, threeWrong,backGround;

    [SerializeField]
    public int wrongs { get; set; }

    [SerializeField]
    private AddPoints script;

    private bool warmup;

    [SerializeField]
    private AudioSource audioSource;

    bool coroutineAllowed,isLoaded;
    // Start is called before the first frame update
    void Start()
    {
        wrongs = 0;
        audioSource = GetComponent<AudioSource>();
        coroutineAllowed = false;
        warmup = true;
        isLoaded = false;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && warmup && isLoaded)
        {
            StartCoroutine(HandleOneCoroutine(oneWrong));
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && warmup && isLoaded)
        {
            warmup = false;
            coroutineAllowed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && coroutineAllowed && isLoaded)
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
        if(Input.GetKeyDown("-") && coroutineAllowed && wrongs>0 && isLoaded)
        {
            wrongs-=1;
        }
        if (Input.GetKeyDown("p"))
        {
            isLoaded = true;
        }
    }

    
}
