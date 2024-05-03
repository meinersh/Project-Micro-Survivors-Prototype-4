using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Game_UI : MonoBehaviour
{
    private Game_Control Game_Control;

    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (Game_Control == null) //Checks if the variable is empty
        {
            Game_Control = GameObject.Find("Game Controller").GetComponent<Game_Control>(); //Finds the Game_Control Script
        }
    }

    public void Unpause_Click()
    {
        Game_Control.ResumeGame();

        this.gameObject.SetActive(false);
    }

    public void Main_Menu_Click()
    {
        Game_Control.StoreGameStats();
        SceneManager.LoadScene("Title Screen");
    }
}
