using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTP : MonoBehaviour
{
    public Transform newStair;
    public GameObject player;

    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if(collision.tag == "Detector" )
        {
            player . transform . position = newStair . position;
        }
    }
}
