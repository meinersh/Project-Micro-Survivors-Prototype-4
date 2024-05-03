//Changes the text to let the user know if something has happened or not.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Settings_Control : MonoBehaviour
{
    public static bool SaveToText = false;
    public static bool LoadFText = false;

    public TextMeshProUGUI SaveOrLoadText;
    public InputField SaveField;
    public InputField LoadField;


    public void Update()
    {
        if (SaveToText == true)
        {
            SaveField.text = Master_Manager.json;
            SaveToText = false;

            Debug.Log("Settings Save Text triggered");
        }
        if (LoadFText == true)
        {
            SaveOrLoadText.text = "Loaded data from the input field.";
        }
    }

    public void SaveClick()
    {
        SaveOrLoadText.text = "Saved Data to: " + Master_Manager.SavePath; //Displays where the data was saved to.
    }

    public void LoadClick() 
    {
        SaveOrLoadText.text = "Loaded Data from: " + Master_Manager.SavePath; //Displays where the data was loaded from.
    }

    public void SaveToClipboardClick()
    {
        SaveOrLoadText.text = "Saved Data Displayed In Text Box. \nRecomend Pasting in text file to save it for later and better viewing.";

        Master_Manager.SaveToTextTrigger = true;
    }

    public void LoadFromText()
    {
        Master_Manager.json = LoadField.text;

        Master_Manager.LoadFromTextTrigger = true;
    }
    
}
