using System.Collections;
using System.Collections.Generic;
using TMPro;

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
    private bool facedUp;
    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = backSprite;
     
        facedUp = false;
        number = gameObject.name;
        number=number.Split(' ')[1];
        score.enabled = false;
        answer.enabled = false;
        
        
    }

 
    public IEnumerator Flip()
    {
        
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

    public void ResetState()
    {
        spriteRenderer.sprite = backSprite;
        answer.enabled = false;
        score.enabled = false;
        
        facedUp = false;
        transform.rotation = Quaternion.Euler(-180, 0, 0);
    }


    void Update()
    {
        
       
    }
}
