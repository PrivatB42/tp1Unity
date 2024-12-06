using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //gameObject.transform.parent.gameObject.tag
        if (other.gameObject.tag == "checkpoint")
        {
            lastPosition = transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }
    }

    public void respawn()
    {
        transform.position = lastPosition;
        PlayerInfos.pi.setHealth(3);
    }
}
