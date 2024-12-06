using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    [Range(.5f, 50)]
    public float detectDistance = 3;
    public Transform[] points;
    int destinationIndex = 0;
    public NavMeshAgent agent;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null )
            agent.destination = points[destinationIndex].position;
    }

    private void Update()
    {
        Walk();
        SearchPlayer();
    }

    public void Walk()
    {
        float dist = agent.remainingDistance;
        if (dist < 0.05f)
        {
            destinationIndex = Random.Range(0, 3);
            if (destinationIndex > points.Length - 1)
                destinationIndex = 0;
            agent.destination = points[destinationIndex].position;
        }
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectDistance)
        {
            //le joueur est detecté
            agent.destination = player.position;
            agent.speed = 2f;
        }
        else
        {
            agent.destination = points[destinationIndex].position;
            agent.speed = 1.5f;
        }
    }

    public void setMobSize()
    {
        if(Vector3.Distance(transform.position, player.position) <= detectDistance + 1)
        {
            iTween.ScaleTo(gameObject, Vector3.one, .5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
