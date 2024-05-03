using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Log_Book_Display : MonoBehaviour
{
    public TextMeshProUGUI Game_Stats;
    public TextMeshProUGUI High_Scores;

    private int Hours, Minutes, Seconds;
    private int L_Hours, L_Minutes, L_Seconds;

    public GameObject[] Log_Book_Items;
    /* Index Reference
     0. Microplastics
     1. Rhinovirus
     2. Mimivirus
     3. Influenze Virus
     */

    // Update is called once per frame
    void Update()
    {
        //Converts the total game time to hours and minutes
        Hours = (int)Master_Manager.L_Total_Time_Open / 3600;
        Minutes = ((int)Master_Manager.L_Total_Time_Open / 60) - (Hours * 60);
        Seconds = (int)Master_Manager.L_Total_Time_Open - (Minutes * 60) - (Hours * 3600);

        Game_Stats.text = "Game Stats\nTotal Microplastics Collected: " + Master_Manager.L_Total_Money +
                        "\nTotal Kills: " + Master_Manager.L_Total_Kills +
                        "\nTotal Time Open: " + Hours + ":" + Minutes + ":" + Seconds +
                        "\nTotal Damage Dealt: " + Master_Manager.L_Total_Damage;

        L_Hours = (int)Master_Manager.L_Longest_Time_Survived / 3600;
        L_Minutes = ((int)Master_Manager.L_Longest_Time_Survived / 60) - (L_Hours * 60);
        L_Seconds = (int)Master_Manager.L_Longest_Time_Survived - (L_Minutes * 60) - (L_Hours * 3600);

        High_Scores.text = "\nLongest Time Survived: " + L_Hours + ":" + L_Minutes + ":" + L_Seconds +
                        "\nHighest Number of Kills: " + Master_Manager.L_Highest_Game_Kills +
                        "\nHighest Single Damage: " + Master_Manager.L_Highest_Single_Damage;
    }

    //When the button is clicked it will send a number the references the requested index.
    public void Log_Item_Clicked(int value)
    {
        //Turns off all the item displays
        for(int i = 0; i < Log_Book_Items.Length; i++)
        {
            Log_Book_Items[i].SetActive(false);
        }

        //Turns on the selected item display
        Log_Book_Items[value].SetActive(true);
        Debug.Log("Log book now displaying " + Log_Book_Items[value].name);
    }
}
