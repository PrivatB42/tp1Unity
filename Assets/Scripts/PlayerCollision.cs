using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject mobEffect;
    public GameObject loot;
    public GameObject cam1;
    public GameObject cam2;
    private bool canInstantiate = true;
    private bool isInvisible = false;
    public AudioClip hitsound;
    public AudioClip coinSound;
    AudioSource audioSource;
    public SkinnedMeshRenderer rend;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")//On touche la piece 
        {
            audioSource.PlayOneShot(coinSound);
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            PlayerInfos.pi.GetCoins();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Fin")
            PlayerInfos.pi.GetScore();

        //gestion des cameras
        if (other.gameObject.tag == "cam1")
        {
           cam1.SetActive(true);
        }

        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //gestion des cameras
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
        }

        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
        }
    }

    /*private void OnCollisionEnter(Collision collision)*/
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        //Si le montre me touche 
        if (collision.gameObject.tag == "hurt" && !isInvisible)//le monstre nous touche 
        {
            isInvisible = true;
            PlayerInfos.pi.setHealth(-1);
            iTween.PunchPosition(gameObject, Vector3.back * 3, .5f);
            StartCoroutine("ResetInvisible");
        }

        if (collision.gameObject.tag == "mob" && canInstantiate)//On elimine le monstre
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            audioSource.PlayOneShot(hitsound);
            canInstantiate = false;
            iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50, 50, 50), .6f);
            //Je saute sur le montre
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Instantiate(loot, collision.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90,0,0));
            Destroy(go, 0.6f);
            Destroy(collision.gameObject.transform.parent.gameObject, 0.5f);
            StartCoroutine("ResetInstantiate");
        }

        if(collision.gameObject.tag == "fall")
            StartCoroutine("Reloadscene");
    }

    IEnumerator Reloadscene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //On reinitialise canInstantiate apres 0.8 sec
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }

    IEnumerator ResetInvisible()
    {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.8f);
        isInvisible = false;
    }
}
