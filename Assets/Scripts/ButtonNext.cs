using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNext : MonoBehaviour
{
    // Start is called before the first frame update
   public  void OnButtonClick()
    {
        SceneManager.LoadScene("Qestions");
    }
}
