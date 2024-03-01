using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int hitPoints = 3;
    public int maxHitPoints = 3;
    public SpriteRenderer sprite;
    public float radius = 1;
    public bool immune;
    public bool item;

    public AudioClip death;
    public AudioClip run;

 
    private void OnDrawGizmosSelected ( )
    {
        Gizmos . color = Color . red;
        Gizmos . DrawWireSphere (transform . position , radius);
    }

    void Start()
    {
        // make hitpoints the maximum amount and if the object isnt an item then add to total ghost count
        hitPoints = maxHitPoints;
        if(!item)
        {
            GameManager . instance . ghostsInHouse += 1;
        }
   
    }

    void Update()
    {
        if(!item)
        {
            // find player
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

            // foreach individual in enemies variable create an enemy in the array
            foreach ( GameObject enemy in enemies )
            {
                // find the distance of an enemy compared to the player and find the difference
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                // if the difference is less than the attack radius and the enemy isnt null, destroy the player
                if ( distance <= radius )
                {
                    if ( enemies != null )
                    {

                        StartCoroutine (GameManager . instance . deathScare ());
                  
                        Destroy (enemy . gameObject);



                    }
                }
            }
        }

        // if the objetc is a ghost then it will take damage, upon reaching 0 it destroys and performs the death sequences
        if (hitPoints <= 0)
        {
            //add to ghosts defeated and other functions
            if(item)
            {
                SoundManager . Instance . PlaySound (death);
                GameManager . instance . bonusPoints += 1;

                Destroy (gameObject);
            }
            else
            {
                // kill the ghost, and if there are 0 ghosts left, reset the level and add to the level
                SoundManager . Instance . PlaySound (death);
                GameManager . instance . ghostsInHouse -= 1;
                GameManager . instance . totalBanished += 1;
                if ( GameManager.instance.ghostsInHouse <= 0 )
                {

                    GameManager . instance. level += 1;
                    GameManager . instance . ResetLevel ();
                }
                Destroy (gameObject);
            }
         
        }
    }

    public IEnumerator TakeDamage()
    {
       
        // if a ghost isnt  immune change the colour to red and negate 1 point from hitpoints, then return colour to white
        if(!immune)
        {

             hitPoints -= 1;
         sprite . color = Color . red;
         yield return new WaitForSeconds (0.4f);
         sprite. color = Color . white;

        }
        else{


            // find behaviour state
            var script = gameObject . GetComponent<StateBehaviorBase> ();
            if ( script != null )
            {
                SoundManager . Instance . PlaySound (run);
                // set running as true to call the retreat function
                script . running = true;
                yield return new WaitForSeconds (10f);
                script . running = false;

            }
      

        }


        
       


    }

    
}
