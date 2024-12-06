using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnim : MonoBehaviour
{
    public Vector3 amount;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        float randomTime =  Random.Range(duration - 0.3f, duration + 0.3f);

        iTween.PunchScale(gameObject, iTween.Hash(
            "amount", amount,
            "time", randomTime,
            "looptype", iTween.LoopType.loop
        ));
    }

}
