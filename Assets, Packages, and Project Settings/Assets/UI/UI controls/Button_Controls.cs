using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Controls : MonoBehaviour
{
    public string scene_name;
    public void Click_Start()
    {
        Debug.Log(this.name + " Button Clicked");
        SceneManager.LoadScene(scene_name);
    }
}
