using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public static List<Transform> Waypoints;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Waypoints == null)
        {
            Waypoints = new List<Transform>();
        }
        Waypoints.Add(transform);
    }

    private void OnDestroy()
    {
        //Waypoints.Remove(transform);
    }
}
