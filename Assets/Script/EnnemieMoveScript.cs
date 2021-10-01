using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class EnnemieMoveScript : MonoBehaviour
{
    public ChampDeVisionScript champDeVision;
    public Transform player;
    public NavMeshAgent nMagent;
    
    private CurrentState _currentState;
    private CurrentState cCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = CurrentState.Idle;
        cCurrentState = CurrentState.Search;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case CurrentState.Idle :
                Idle();
                if (champDeVision.triggered == true)
                {
                    _currentState = CurrentState.Chase;
                }
                break;
            
            case CurrentState.Chase :
                Chase();
                if (Vector3.Distance(transform.position, player.position) > 17)
                {
                    _currentState = CurrentState.Search;
                }
                break;
            
            case CurrentState.Search :
                Search();
                if (champDeVision.triggered == true)
                {
                    _currentState = CurrentState.Chase;
                }
                Invoke("SwitchToIdle", 10);
                break;
        }

        if (cCurrentState != _currentState)
        {
            Debug.Log(_currentState);
            cCurrentState = _currentState;
        }
    }

    private void Idle()
    {
        nMagent.speed = 7.5f;
        gameObject.transform.Rotate(0, 0.1f,0);
    }

    private void Chase()
    {
        nMagent.speed = 20;
        GetComponent<MoveTo>().Chase();
    }

    private void Search()
    {
        nMagent.speed = 15;
        
    }

    private void SwitchToIdle()
    {
        _currentState = CurrentState.Idle;
    }
}

enum CurrentState
    {
        Idle,
        Chase,
        Search,
    }