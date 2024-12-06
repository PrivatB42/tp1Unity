using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    bool isPause = false;
    public GameObject menuPause;
    public Text objectifs;
    public static int amisRestants = 3;
    public GameObject miniMap;

    public void SetObjectifText()
    {
        objectifs.text = "Il reste "+ amisRestants +" amis à liberer...";
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: ajouter un son de pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) 
            {
                isPause = false;
                menuPause.SetActive(isPause);
                Time.timeScale = 1f;
            }
            else
            {
                SetObjectifText();
                isPause = true;
                menuPause.SetActive(isPause);
                Time.timeScale = 0f;
            }
                     
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            miniMap.SetActive(!miniMap.activeSelf);
        }
    }
}
