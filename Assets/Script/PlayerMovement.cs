using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rbPlayer;
    public UIScript UIScript;

    // Update is called once per frame
    void Update()
    {
        float inpX = Input.GetAxis("Horizontal");
        float inpY = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(inpX, 0, inpY);

        rbPlayer.velocity = vel * speed * Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ennemies"))
        {
            Death();
        }
        else if(other.CompareTag("Finish"))
        {
            Victoire();
        }
    }

    private void Death()
    {
        Debug.Log("Death");
        UIScript.Death();
    }

    private void Victoire()
    {
        Debug.Log("Victoire");
        UIScript.Victoire();
    }
}
