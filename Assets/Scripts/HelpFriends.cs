using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    public Text infoText;
    bool canOpen =  false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cage") 
        {
            cage = other.gameObject;
            infoText.text = "Appuyez sur E pour ouvrir la cage";
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cage")
        {
            cage = null;
            infoText.text = "";
            canOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen) 
        {
            PauseScript.amisRestants--;
            iTween.ShakeRotation(cage, new Vector3(150,150,150), 1f);
            Destroy(cage, 1.2f);
            infoText.text = "";
        }
    }

}
