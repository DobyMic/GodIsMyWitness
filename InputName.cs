using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputName : MonoBehaviour
{

    private string input;
    // Update is called once per frame



    public void ReadStringInput(string s)
    {
        input = s;
        PlayerPrefs . SetString ("Username", input);
    }
}
