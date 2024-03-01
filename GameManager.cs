using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public int ghostsInHouse;
    public int level;

    public int bonusPoints;
    public int totalScore;
    public int totalBanished ;
    public bool gameOver;

    public GameObject scare;
    public AudioClip scaryAudio;

    void Awake ( )
    {
     
        if ( instance == null )// if there is no gamemanager instance set this script as the instance GameManager
        {
            instance = this;
        }
        else if ( instance != this )// if the instance already exists and isnt this then destroy that instance, assuuring us that theres only one gameManager
        {
            Destroy (gameObject);
        }
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == "Score" )
        {
            Destroy (gameObject);
            
        }
        else
        {
            DontDestroyOnLoad (instance);
        }

        instance.ghostsInHouse = 0;
    }

 
    int oldScore;
    public void ResetGame()
    {
        instance . gameOver = true;


        instance . totalScore = instance . totalBanished + instance . bonusPoints * instance . level;






        PlayerPrefs . SetInt ("ScoreYou" , instance . totalScore);
        PlayerPrefs . SetString ("NameYou" , PlayerPrefs . GetString ("Username"));





        if ( instance . totalScore >= PlayerPrefs . GetInt ("Score") )
        {
            PlayerPrefs . SetString ("Name4" , PlayerPrefs . GetString ("Name3"));
            PlayerPrefs . SetString ("Name3" , PlayerPrefs . GetString ("Name2"));
            PlayerPrefs . SetString ("Name2" , PlayerPrefs . GetString ("Name1"));
            PlayerPrefs . SetString ("Name1" , PlayerPrefs . GetString ("Name"));
            PlayerPrefs . SetString ("Name" , PlayerPrefs . GetString ("NameYou"));
            PlayerPrefs . SetInt ("Score4" , PlayerPrefs . GetInt ("Score3"));
            PlayerPrefs . SetInt ("Score3" , PlayerPrefs . GetInt ("Score2"));
            PlayerPrefs . SetInt ("Score2" , PlayerPrefs . GetInt ("Score1"));
            PlayerPrefs . SetInt ("Score1" , PlayerPrefs . GetInt ("Score"));
            PlayerPrefs . SetInt ("Score" , instance . totalScore);
        }
        else if ( instance . totalScore >= PlayerPrefs . GetInt ("Score1") )
        {
            PlayerPrefs . SetString ("Name4" , PlayerPrefs . GetString ("Name3"));
            PlayerPrefs . SetString ("Name3" , PlayerPrefs . GetString ("Name2"));
            PlayerPrefs . SetString ("Name2" , PlayerPrefs . GetString ("Name1"));
            PlayerPrefs . SetString ("Name1" , PlayerPrefs . GetString ("NameYou"));
            PlayerPrefs . SetInt ("Score4" , PlayerPrefs . GetInt ("Score3"));
            PlayerPrefs . SetInt ("Score3" , PlayerPrefs . GetInt ("Score2"));
            PlayerPrefs . SetInt ("Score2" , PlayerPrefs . GetInt ("Score1"));
            PlayerPrefs . SetInt ("Score1" , instance . totalScore);
        }
        else if ( instance . totalScore >= PlayerPrefs . GetInt ("Score2") )
        {
            PlayerPrefs . SetString ("Name4" , PlayerPrefs . GetString ("Name3"));
            PlayerPrefs . SetString ("Name3" , PlayerPrefs . GetString ("Name2"));
            PlayerPrefs . SetString ("Name2" , PlayerPrefs . GetString ("NameYou"));
            PlayerPrefs . SetInt ("Score4" , PlayerPrefs . GetInt ("Score3"));
            PlayerPrefs . SetInt ("Score3" , PlayerPrefs . GetInt ("Score2"));
            PlayerPrefs . SetInt ("Score2" , instance . totalScore);
        }
        else if ( instance . totalScore >= PlayerPrefs . GetInt ("Score3") )
        {
            PlayerPrefs . SetString ("Name4" , PlayerPrefs . GetString ("Name3"));
            PlayerPrefs . SetString ("Name3" , PlayerPrefs . GetString ("NameYou"));
            PlayerPrefs . SetInt ("Score4" , PlayerPrefs . GetInt ("Score3"));
            PlayerPrefs . SetInt ("Score3" , instance . totalScore);
        }
        else if ( instance . totalScore >= PlayerPrefs . GetInt ("Score4") )
        {
            PlayerPrefs . SetString ("Name4" , PlayerPrefs . GetString ("NameYou"));
            PlayerPrefs . SetInt ("Score4" , instance . totalScore);
        }






        instance . totalBanished = 0;
        instance . totalScore = 0;
        instance . level = 0;
        instance . bonusPoints = 0;

        SceneManager . LoadScene ("Score");
        instance . gameOver = false;
    }


    public IEnumerator deathScare()
    {

        scare . SetActive (true);
        SoundManager . Instance . PlaySound (scaryAudio);
        yield return new WaitForSeconds (3f);
        scare . SetActive (false);
        ResetGame ();
    }
   
    public void ResetLevel()
    {

        instance .ghostsInHouse = 0;

        SceneManager . LoadScene ("House");
    }

    private void Update ( )
    {

  

        
     

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application . Quit();
        }
    }





}


