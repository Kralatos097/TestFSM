using System;
using UnityEngine;
    using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class MoveTo : MonoBehaviour {
       
       public Transform goal;
       private NavMeshAgent _agent;
       
       void Start ()
       {
          _agent = GetComponent<NavMeshAgent>();
       }

       public void Chase()
       {
           _agent.destination = goal.position;
       }
}
