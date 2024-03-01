using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . UI;

public class UiTracker : MonoBehaviour
{
    public Text ghostsKilled;
    public Text level;
    public Text score;
   

 
    // Update is called once per frame
    void Update()
    {
        // track Manager vars that i want to display to player for scoring
        ghostsKilled.text = "Ghosts: " + GameManager . instance . ghostsInHouse. ToString (); 
        level.text = "Level: " + GameManager . instance . level . ToString ();
        score. text = "Score: " + (GameManager . instance . totalBanished + GameManager . instance . bonusPoints * GameManager . instance . level). ToString ();

    }
}
