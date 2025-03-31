using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AddPoints : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    public static bool allowAdd;
    void Start()
    {
       //score.text = "";
       allowAdd = true;
    }

    public void addScore(string points)
    {
        try
        {
            if (!allowAdd)
            {
                return;
            }
            int pointsInt = int.Parse(points);
            if (score.text == "")
            {
                score.text = points;
            }
            else
            {
                int scoreInt = int.Parse(score.text);
                scoreInt += pointsInt;
                score.text = scoreInt.ToString();
            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void noAllowAdd()
    {
        allowAdd = false;
    }
    
}
