using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshScript : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PLAYER").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
