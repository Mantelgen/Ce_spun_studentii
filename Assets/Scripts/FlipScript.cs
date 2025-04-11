using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



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
    

    [SerializeField]
    private bool coroutineAllowed, facedUp;
    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = backSprite;
        coroutineAllowed = false;
        facedUp = false;
        number = gameObject.name;
        number=number.Split(' ')[1];
        score.enabled = false;
        answer.enabled = false;
        
        
    }

 
    public IEnumerator Flip()
    {
        coroutineAllowed = false;
        if (!facedUp)
        {
            if(audioSource.isActiveAndEnabled)
                audioSource.Play();
            
           
            for (float i = 0f; i <= 180f; i += 3f)
            {
                transform.rotation = Quaternion.Euler(i, 0, 0);
                
                if (i == 90f)
                {

                    answer.enabled = true;
                    score.enabled = true;
                    spriteRenderer.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        } 
    }
   
    void Update()
    {
        
       
    }
}
