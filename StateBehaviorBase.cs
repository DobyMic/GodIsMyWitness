using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviorBase : MonoBehaviour
{


    public GameObject hostObj;
    private Health health;
    private EnemyAI ai;
    private float eventTime;
    [SerializeField] public int eventProgress = 1;
    public int type;
    public int attackType;

    public float hitRadius;

    private float patrolTimer;
    [SerializeField] public bool running;
    public Transform moveSpot;

    public AudioClip randomAudio;
  

    bool didMoveAlready;

    private void OnDrawGizmosSelected ( )
    {

        // display in scene the radius of the hitbox
        Gizmos . color = Color . red;
        Gizmos . DrawWireSphere (transform . position , hitRadius);



    }

    void Start ( )
    {
        // reference Components to variables and set variables
        health = gameObject . GetComponent<Health> ();
        ai = gameObject . GetComponent<EnemyAI> ();
        //spawn new empty gameobject as the home location for a ghost to retreat to 
        hostObj = Instantiate (new GameObject () , gameObject . transform . position , Quaternion . identity);
        // set patrol center point as the hostObj
        moveSpot . position = new Vector2 (hostObj . transform . position . x + Random . Range (-3 , 3) , hostObj . transform . position . y + Random . Range (-2 , 2));
        eventTime = Random . Range (5 , 20);

    }


    

    void Update ()
    {
        // if enemy  is not running
        if ( !running)
        {

            //based on what type a ghost is selected in the editor the behaviour is different
            if ( type == 1 )
            {

                // type one will periodically switch through states determined by the timer
                eventTime -= Time . deltaTime;
                // if timer reaches 0 reset it to a random number and change the ghosts state
                if ( eventTime <= 0 )
                {
                    eventProgress += 1;
                    eventTime = Random . Range (10 , 30);

                }

                // based on the eventProgress change the updating state function active
                switch ( eventProgress )
                {
                   
                    // remain idle and disable any immunity
                    case 1:
                        health . immune = false;

                        swappedState = false;
                        Idle ();
      
                        break;

                        // attack the player depending on what attack type a ghost has
                    case 2:

                        if(!swappedState)
                        {
                            SwappedState ();
                        }
                        health . immune = true;
                        Attack ();
                        break;
                    case 3:
                        // reset to state 1 to continue state flow
                        swappedState = false;
                        eventTime = 1;
                        break;
                }

                if ( eventProgress >= 4 )
                {
                    eventProgress = 1;
                }

            }

            if ( type == 2 )
            {
                // if the player is within the ghosts detection radius it chases/ attacks him, otherwise patrol the area
                float distance = Vector2.Distance(transform.position, GameObject.Find("Joe").transform.position);
                if ( distance <= hitRadius )
                {
                    if ( !swappedState )
                    {
                        SwappedState ();
                    }

                    Attack ();


                }
                else
                {
                    Patrol ();
                    swappedState = false;
                }


            }
            if ( type == 3 )
            {
                // if the player comes into range attack, otherwise remain idle
                float distance = Vector2.Distance(transform.position, GameObject.Find("Joe").transform.position);
                if ( distance <= hitRadius )
                {
                    if ( !swappedState )
                    {
                        SwappedState ();
                    }
                    Attack ();



                }
                else
                {
                    swappedState = false;
                }
            }

            // remain hovering in circles idle
            if ( type == 4 )
            {

                HoverIdle ();
            }
      
        }
        else
        {
            // if a ghost is running then it returns to its hostObj 
            Recall ();
        }
   



    }

    Transform joe;
    bool swappedState;
    void SwappedState()
    {
        SoundManager . Instance . PlaySound (randomAudio);
        swappedState = true;


    }
    void Attack()
    {
        // Find player
        joe = GameObject . Find ("Joe") . transform;
        // based on the attack type in editor perform attacks based on the case
        switch ( attackType)
        {
            case 1:
                // chase the player by settings its pathfinding route as the player
                ai . target = joe;

                break;
            case 2:

               
                ai . target = joe;
                // if the enemy hasnt tped yet then perform the Ienum
                if (didMoveAlready != true)
                {
                    StartCoroutine (TpDash ());
                }
                break;

            case 3:
                ai . target = joe;
                // if the ghost hasnt cloned in time then clone
                if ( didMoveAlready != true )
                {
                    StartCoroutine (Clone ());
                }
                
                break;
        }

 
    }
    
    IEnumerator TpDash()
    {
        didMoveAlready = true;
        // randomly pick a number 2 or -2
        int randomValue = (Random.Range(0, 2) == 0) ? 2 : -2;
        // move the ghost to a random position that is 2 or -2 x and y from the player
        gameObject . transform . position = new Vector2 (joe . transform . position . x + randomValue , joe . transform . position . y + randomValue);

        yield return new WaitForSeconds (2.5f);
        // after waiting allow the ghost to perform the move again
        didMoveAlready = false;

    }

    IEnumerator Clone ( )
    {

        didMoveAlready = true;
        // randomly picks a number between the 2 random . range params
            float randomValue = (Random.Range(0,2) == 0) ? Random.Range(-2,-0.5f): Random.Range(0.5f,2);
           

           // instantiates the GFX gameobject onto the player and sets its parent as this so it moves with the ghost
            GameObject clone = Instantiate(ai.enemyGFX.gameObject, new Vector2 (joe . transform . position . x + randomValue , joe . transform . position . y + randomValue),Quaternion .identity);
            clone . transform . SetParent (gameObject . transform);
            clone . transform . localScale = new Vector2 (1 , 1);






        yield return new WaitForSeconds (2f);
        // allows for move to happen again
        didMoveAlready = false;



    }

    void Idle()
    {
       // set target as null so chose just stays idle
        ai .target = null;
        eventTime -= Time . deltaTime * 2;
    }
    float angle;

    void HoverIdle()
    {

        ai . target = null;
        // set the angle which you circle
        angle += 2f * Time . deltaTime;

        // find radius of gameobject and multiply by cos and tan along with the angle you set
        float x = health.radius * Mathf.Cos(angle);
        float y = health.radius * Mathf.Sin(angle);

      // move ghost based onthe new Vector and / the x and y by 18 so it isnt absurdly big
        transform . position += new Vector3 (x /18, y/18 , 0);
    }


    public void Recall()
    {
        // remove immunity and pathfind to the hostObj pos
        health . immune = false;
        ai .target = hostObj.transform;
     
    }

    void Patrol()
    {
        // ai finds the movespot
        ai . target = moveSpot;




        // every few seconds move the movespot so that the ghosts follows it again
            if ( patrolTimer <= 0 )
            {
                moveSpot . position = new Vector2 (hostObj . transform . position . x + Random . Range (-6 , 6) , hostObj . transform . position . y + Random . Range (-6 , 6));
           
                patrolTimer = 4.5f;
            }
            else
            {
              patrolTimer -= Time . deltaTime;
            }
          


   
    }

 

    
}
