//This script loads when the game starts and should never unload. It stores important data for other scripts to reference even when the scene changes.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Master_Manager : MonoBehaviour
{
    public static Master_Manager Instance;

    //Runs when the object is first created.
    private void Awake()
    {
        //Singleton so that there is only ever one Instance of this object.
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate Master_Manager deleted");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

    }

    //-----Variables------------------------------------------
    //Notes: Maybe turn some of these in arrays?
    //The only problem I see with that would be having to memorize the index values or make a bunch of notes saying what each index should be.

    //SaveData
    private StoredPlayerData StoredPlayerData = new();
    public static string json;
    public static string SavePath;
    public static bool SaveToTextTrigger = false;
    public static bool LoadFromTextTrigger = false;

    //Game Settings

    //Money Saved Up
    public static float Money_Saved = 0;

    //Shop Upgrade Levels
    public static int S_Health_Upgrade_Level = 0;
    public static int S_Speed_Upgrade_Level = 0;
    public static int S_Luck_Upgrade_Level = 0;
    public static int S_Attack_Damage_Upgrade_Level = 0;
    public static int S_Attack_Speed_Upgrade_Level = 0;
    public static int S_Attack_Size_Upgrade_Level = 0;

    //Shop upgrade costs
    public static float S_Health_Upgrade_Cost;
    public static int S_Speed_Upgrade_Cost;
    public static int S_Luck_Upgrade_Cost;
    public static int S_Attack_Damage_Upgrade_Cost;
    public static int S_Attack_Speed_Upgrade_Cost;
    public static int S_Attack_Size_Upgrade_Cost;

    //Stats For Log Book
    public static float L_Total_Money;
    public static int L_Total_Kills;
    public static int L_Highest_Game_Kills;
    public static float L_Total_Time_Open; //Tracks how long the application has been open for.
    public static float L_Longest_Time_Survived;
    public static float L_Highest_Single_Damage;
    public static float L_Total_Damage;


    //-----Functions------------------------------------------
    private void Start()
    {
        SavePath = Path.Combine(Application.persistentDataPath, "Save.json");
        Debug.Log(SavePath);
    }
    private void Update()
    {
        L_Total_Time_Open += Time.unscaledDeltaTime; //Still updates even if the game is paused.

        //Triggered from fuctions in the settings control.
        if (SceneManager.GetActiveScene().name == "Settings Menu")
        {
            if (SaveToTextTrigger == true)
            {
                SaveDataToClipboard(); //Triggers the Save

                Settings_Control.SaveToText = true; //Tells the settings control the data was saved
                Debug.Log("Save Triggered");
                SaveToTextTrigger = false; //Turns it's self off
            }
            //Triggered from fuctions in the settings control.
            if (LoadFromTextTrigger == true)
            {
                LoadDataFromInputField(); //Triggers the Load

                Settings_Control.LoadFText = true; //Tells the settings the data was loaded.
                Debug.Log("Load Triggered");

                LoadFromTextTrigger = false; //Turns it's self off
            }
        }
    }

    //When this is called it will update the prices of the shop upgrades.
    //This is usually called when entering the shop and after clicking an upgrade button.
    static public void Update_Shop_Prices()
    {
        S_Health_Upgrade_Cost = (S_Health_Upgrade_Level * 10) + 10;
        S_Speed_Upgrade_Cost = (S_Speed_Upgrade_Level * 10) + 10;
        S_Luck_Upgrade_Cost = (S_Luck_Upgrade_Level * 10) + 10;
        S_Attack_Damage_Upgrade_Cost = (S_Attack_Damage_Upgrade_Level * 10) + 10;
        S_Attack_Speed_Upgrade_Cost = (S_Attack_Speed_Upgrade_Level * 10) + 10;
        S_Attack_Size_Upgrade_Cost = (S_Attack_Size_Upgrade_Level * 10) + 10;
    }

    //Resets the Shop Upgrade Levels to 0
    static public void Reset_Shop_Levels()
    {
        S_Health_Upgrade_Level = 0;
        S_Speed_Upgrade_Level = 0;
        S_Luck_Upgrade_Level = 0;
        S_Attack_Damage_Upgrade_Level = 0;
        S_Attack_Speed_Upgrade_Level = 0;
        S_Attack_Size_Upgrade_Level = 0;
    }
    public void LoadData()
    {
        LoadDataFromFile();

        JsonToData();
        Debug.Log("Loaded data to StoredPlayerData from: " + SavePath);

        StoreLoadData();

        Debug.Log("StoredPlayerData is now Loaded to Master Manager");
    }

    public void SaveData()
    {
        StoreSaveData();

        DataToJson();

        WriteDataToFile();

        Debug.Log("Master Manager Data now stored in StoredPlayerData");
    }

    public void SaveDataToClipboard()
    {
        StoreSaveData(); //Stores the Current Data
        DataToJson(); //Converts the Data to Json so it can be copied to the clipboard
    }

    public void LoadDataFromInputField()
    {
        JsonToData();
        StoreLoadData();
    }

    public void DataToJson()
    {
        json = JsonUtility.ToJson(StoredPlayerData); //takes the data from DataObject and turns it in json format and save it to a string
        SavePath = Path.Combine(Application.persistentDataPath, "Save.json"); //SavePath gets set to null for some reason unless I do this hear as well.
    }

    public void JsonToData()
    {
        StoredPlayerData = JsonUtility.FromJson<StoredPlayerData>(json); //Takes the data and loads it into the data storing object
    }

    private void LoadDataFromFile()
    {
        SavePath = Path.Combine(Application.persistentDataPath, "Save.json"); //SavePath gets set to null for some reason unless I do this hear as well.
        json = File.ReadAllText(SavePath); //Gets the File and stores it in a string json
        Debug.Log(json);
    }

    private void StoreLoadData()
    {
        //Added a try test so the game doesn't crash when trying to load anything that is not the valid Json data
        try
        {
            //Takes the Data from StoredPlayerData and loads it into the master manager.
            //Money the Player has saved
            Money_Saved = StoredPlayerData.Money_Saved;

            //Shop Upgrade Levels
            S_Health_Upgrade_Level = StoredPlayerData.S_Health_Upgrade_Level;
            S_Speed_Upgrade_Level = StoredPlayerData.S_Speed_Upgrade_Level;
            S_Luck_Upgrade_Level = StoredPlayerData.S_Luck_Upgrade_Level;
            S_Attack_Damage_Upgrade_Level = StoredPlayerData.S_Attack_Damage_Upgrade_Level;
            S_Attack_Speed_Upgrade_Level = StoredPlayerData.S_Attack_Speed_Upgrade_Level;
            S_Attack_Size_Upgrade_Level = StoredPlayerData.S_Attack_Size_Upgrade_Level;

            //Stats For Log Book
            L_Total_Money = StoredPlayerData.L_Total_Money;
            L_Total_Kills = StoredPlayerData.L_Total_Kills;
            L_Highest_Game_Kills = StoredPlayerData.L_Highest_Game_Kills;
            L_Total_Time_Open = StoredPlayerData.L_Total_Time_Open;
            L_Highest_Single_Damage = StoredPlayerData.L_Highest_Single_Damage;
            L_Longest_Time_Survived = StoredPlayerData.L_Longest_Time_Survived;
            L_Total_Damage = StoredPlayerData.L_Total_Damage;
        }
        catch
        {
            Debug.Log("Error Loading Data into the game");
        }

    }

    private void StoreSaveData()
    {
        //Takes the data stored in the Master manager and stores it in StoredPlayerData
        //Money the Player has saved
        StoredPlayerData.Money_Saved = Money_Saved;

        //Shop Upgrade Levels
        StoredPlayerData.S_Health_Upgrade_Level = S_Health_Upgrade_Level;
        StoredPlayerData.S_Speed_Upgrade_Level = S_Speed_Upgrade_Level;
        StoredPlayerData.S_Luck_Upgrade_Level = S_Luck_Upgrade_Level;
        StoredPlayerData.S_Attack_Damage_Upgrade_Level = S_Attack_Damage_Upgrade_Level;
        StoredPlayerData.S_Attack_Speed_Upgrade_Level = S_Attack_Speed_Upgrade_Level;
        StoredPlayerData.S_Attack_Size_Upgrade_Level = S_Attack_Size_Upgrade_Level;

        //Stats For Log Book
        StoredPlayerData.L_Total_Money = L_Total_Money;
        StoredPlayerData.L_Total_Kills = L_Total_Kills;
        StoredPlayerData.L_Highest_Game_Kills = L_Highest_Game_Kills;
        StoredPlayerData.L_Total_Time_Open = L_Total_Time_Open;
        StoredPlayerData.L_Highest_Single_Damage = L_Highest_Single_Damage;
        StoredPlayerData.L_Longest_Time_Survived = L_Longest_Time_Survived;
        StoredPlayerData.L_Total_Damage = L_Total_Damage;
    }

    private void WriteDataToFile()
    {
        using StreamWriter writer = new(SavePath); //Finds the json file. If there is not one it creates one.
        {
            writer.WriteLine(json); //Writes the json to a json file
        }
        Debug.Log("Saved StoredPlayerData to: " + SavePath);
    }


}


//This stores the data that is needed for a save.
//The data is either saved into it or is loaded from it
[Serializable]
public class StoredPlayerData
{
    //Money Saved Up
    public float Money_Saved;

    //Shop Upgrade Levels
    public int S_Health_Upgrade_Level;
    public int S_Speed_Upgrade_Level;
    public int S_Luck_Upgrade_Level;
    public int S_Attack_Damage_Upgrade_Level;
    public int S_Attack_Speed_Upgrade_Level;
    public int S_Attack_Size_Upgrade_Level;

    //Stats For Log Book
    public float L_Total_Money;
    public int L_Total_Kills;
    public int L_Highest_Game_Kills;
    public float L_Total_Time_Open; //Tracks how long the application has been open for.
    public float L_Longest_Time_Survived;
    public float L_Highest_Single_Damage;
    public float L_Total_Damage;
}
