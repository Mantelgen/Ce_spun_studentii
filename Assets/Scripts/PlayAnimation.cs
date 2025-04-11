using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField]
    private new Animation animation;
    private bool isAllowed;
    void Start()
    {
        animation = GetComponent<Animation>();
        isAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            isAllowed = true;
        }
        if (Input.GetKeyDown("p") && isAllowed)
        {
            animation.Play();
            isAllowed = false;
        }
    }
}
