using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWitch : MonoBehaviour
{

    private float deathTimer;
    public Sprite dead;
    // Start is called before the first frame update
    void Start()
    {
        deathTimer = Random . Range (25f , 60f);
    }

    // Update is called once per frame
    void Update()
    {

        deathTimer -= Time . deltaTime;
        if(deathTimer <= 0)
        {
            var anim = gameObject . GetComponent<Animator> ().enabled = false;

            gameObject . GetComponent<SpriteRenderer> () . sprite = dead;
        }
    }
}
