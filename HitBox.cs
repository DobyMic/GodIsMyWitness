using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    public float hitRadius;
    public float hitSpeed;
    private float lastHitTime = 1f;
    private void OnDrawGizmosSelected ( )
    {
        Gizmos . color = Color . red;
        Gizmos . DrawWireSphere (transform . position , hitRadius);
    }
    // Start is called before the first frame update
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ghost");

        foreach ( GameObject enemy in enemies )
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= hitRadius )
            {
                if ( enemies != null )
                {
                    Attack (enemy);

                }
            }
        }
    }

    public void Attack ( GameObject enemy )
    {

        if ( Time . time - lastHitTime >= hitSpeed )
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= hitRadius )
            {

                var enemyHealth = enemy . GetComponent<Health> ();
                if(enemyHealth != null)
                {
                    enemyHealth . StartCoroutine (enemyHealth.TakeDamage ());
                }

                lastHitTime = Time . time;
            }
        }
    }
}
