using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    [SerializeField]
    private TextMeshPro score;
    [SerializeField]
    private TextMeshPro answer;

    private string number;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private KeyCode key;

    private bool coroutineAllowed, facedUp;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
        number = gameObject.name;
        number=number.Split(' ')[1];
        score.enabled = false;
        answer.enabled = false;
    }

 
    private IEnumerator Flip()
    {
        coroutineAllowed = false;
        if (!facedUp)
        {
            if(audioSource.isActiveAndEnabled)
                audioSource.Play();
            for (float i = 0f; i <= 180f; i += 5f)
            {
                transform.rotation = Quaternion.Euler(i, 0, 0);
                if (i == 70f)
                {

                    answer.enabled = true;
                    answer.ForceMeshUpdate();
                    score.enabled = true;


                }

                if (i == 80f)
                {
                    spriteRenderer.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        } 
    }
   
    void Update()
    {
        if(Input.GetKeyDown(number) && coroutineAllowed)
        {
                StartCoroutine(Flip());
     
        }
    }
}
