using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRandSPawn : MonoBehaviour
{
    public bool ghost;
    public GameObject[] myObjects;
    public Transform[] locations;
    public float yModifier;
    public float xModifier;

    public int spawnAmt;

    void Start ( )
    {
        if(ghost)
        {
            spawnAmt += Random.Range(1 + GameManager.instance.level, 3 + GameManager.instance.level);
            for ( int i = 0 ; i < spawnAmt ; i++ )
            {
                // Randomly choose an object and a spawn location
                GameObject selectedObject = myObjects[Random.Range(0, myObjects.Length)];
                Transform spawnLocation = locations[Random.Range(0, locations.Length)];

                // Spawn the selected object at the chosen location
                Instantiate (selectedObject , new Vector2(spawnLocation . position.x + Random . Range (-xModifier , xModifier), spawnLocation . position . y + Random . Range (-yModifier , yModifier)) , Quaternion . identity);
            }
        }
        else
        {
            for ( int i = 0 ; i <= spawnAmt ; i++ )
            {
                int randomIndex = Random.Range(0, myObjects.Length);

                Instantiate (myObjects [ randomIndex ] , new Vector2 (gameObject . transform . position . x + Random . Range (-xModifier , xModifier) , gameObject . transform . position . y + Random . Range (-yModifier , yModifier)) , Quaternion . identity);
            }
        }

      
   
    }



}


