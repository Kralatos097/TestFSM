using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EnnemieMoveScript : MonoBehaviour
{
    public ChampDeVisionScript champDeVision;
    public Transform player;
    public NavMeshAgent nMagent;
    //public WaypointScript WaypointScript;

    private CurrentState _currentState;
    private CurrentState cCurrentState;
    private List<Transform> WaypointList;
    private int lastPoint = 0;
    private int preLastPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = CurrentState.Idle;
        cCurrentState = CurrentState.Search;
        WaypointList = WaypointScript.Waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case CurrentState.Idle :
                Idle();
                if (DetectPlayer() == true)
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
                if (DetectPlayer() == true)
                {
                    _currentState = CurrentState.Chase;
                }
                Invoke("SwitchToIdle", 20);
                break;
            
            case CurrentState.Guard :
                Guard();
                if (DetectPlayer() == true)
                {
                    _currentState = CurrentState.Chase;
                }
                break;
        }

        if (cCurrentState != _currentState)
        {
            Debug.Log(_currentState);
            cCurrentState = _currentState;
        }
    }

    private bool DetectPlayer()
    {
        if (champDeVision.triggered == true)
        {
            if (!Physics.Linecast(transform.position, player.position, layerMask:3))
            {
                return true;
            }
        }
        return false;
    }

    private void Idle()
    {
        nMagent.speed = 7.5f;

        MoveAround();
        _currentState = CurrentState.Guard;
    }

    private void Chase()
    {
        CancelInvoke();
        nMagent.speed = 15;
        GetComponent<MoveTo>().MoveToGoal();
    }

    private void Search()
    {
        nMagent.speed = 15;
        
        MoveAround();
    }

    private void Guard()
    {
        if (!nMagent.hasPath)
        {
            gameObject.transform.Rotate(0, 0.3f, 0);
            Invoke("SwitchToIdle", 3);
        }
        else
        {
            CancelInvoke();
        }
    }

    private void MoveAround()
    {
        if (!nMagent.hasPath)
        {
            int ind = Random.Range(0, WaypointList.Count);
            while(lastPoint == ind || preLastPoint == ind)
            {
                ind = Random.Range(0, WaypointList.Count);
            }
            preLastPoint = lastPoint;
            lastPoint = ind;
            
            //Debug.Log("-> " + preLastPoint + " ---- " + lastPoint);
                    
            Transform dest = WaypointScript.Waypoints[ind];
                    
            //Debug.Log("--> " + dest.position);
            GetComponent<MoveTo>().MoveToPoint(dest.position);
        }
    }

    private void SwitchToIdle()
    {
        if(_currentState == CurrentState.Search || _currentState == CurrentState.Guard) _currentState = CurrentState.Idle;
    }
}

enum CurrentState
    {
        Idle,
        Chase,
        Search,
        Guard,
    }