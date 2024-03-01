using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public GameObject panel;

    public void StartGame()
    {
        SceneManager . LoadScene ("House");
    }

    public void EnablePanel()
    {
        panel .SetActive (!panel . activeInHierarchy);
    }

    public void QuitApp()
    {
        Application . Quit ();
    }


}
