using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . UI;

public class ScoreBoard : MonoBehaviour
{

    public Text scoreText;
    public Text nameText;

    public Text scoreText1;
    public Text nameText1;

    public Text scoreText2;
    public Text nameText2;

    public Text scoreText3;
    public Text nameText3;

    public Text scoreText4;
    public Text nameText4;

    public Text scoreTextYou;
    public Text nameTextYou;

    // Start is called before the first frame update
    void Start()
    {
        scoreText . text =  PlayerPrefs . GetInt ("Score",0) . ToString ("F0");
        nameText . text = PlayerPrefs . GetString ("Name");

        scoreText1 . text = PlayerPrefs . GetInt ("Score1" , 0) . ToString ("F0");
        nameText1 . text = PlayerPrefs . GetString ("Name1");
        scoreText2 . text = PlayerPrefs . GetInt ("Score2" , 0) . ToString ("F0");
        nameText2 . text = PlayerPrefs . GetString ("Name2");
        scoreText3 . text = PlayerPrefs . GetInt ("Score3" , 0) . ToString ("F0");
        nameText3 . text = PlayerPrefs . GetString ("Name3");
        scoreText4 . text = PlayerPrefs . GetInt ("Score4" , 0) . ToString ("F0");
        nameText4 . text = PlayerPrefs . GetString ("Name4");
        scoreTextYou . text = PlayerPrefs . GetInt ("ScoreYou" , 0) . ToString ("F0");
        nameTextYou . text = PlayerPrefs . GetString ("NameYou");







    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
