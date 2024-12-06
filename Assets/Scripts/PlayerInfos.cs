using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;

    public int playerHealth = 3;
    public int nbCoins = 0;

    public Image[] hearts;
    public Text coinTxt;
    public Text scoreText;
    public CheckpointMgr CheckpointMgr;

    private void Awake()
    {
        pi = this;
    }

    public void setHealth(int val)
    {
        playerHealth += val;
        if(playerHealth >3)
            playerHealth = 3;
        if(playerHealth <= 0)
        {
            playerHealth = 0;
            CheckpointMgr.respawn();
        }
           
        setHealthBar();
    }

    public void GetCoins()
    {
        nbCoins ++;
        coinTxt.text = nbCoins.ToString();
    }

    public void setHealthBar()
    {
        //on vide la bar de vie
        foreach (Image img in hearts) { 
            img.enabled = false;
        }

        //on met le bon nombre de vie a l'ecran
        for (int i = 0; i < playerHealth; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public int GetScore()
    {
        int scoreFinal = (nbCoins * 5) + (playerHealth * 5);
        scoreText.text = "score = "+ scoreFinal;
        return scoreFinal;
    }
}
