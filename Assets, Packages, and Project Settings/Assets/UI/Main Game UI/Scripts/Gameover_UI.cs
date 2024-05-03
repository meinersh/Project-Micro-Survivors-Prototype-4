using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gameover_UI : MonoBehaviour
{
    public GameObject[] Gameover_Text;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Game_Control.Game_Over_UI_Enabled = true;
        Master_Manager.Update_Shop_Prices(); //Runs to make sure that shop prices are updated for later if statement
        Gameover_Text[2].SetActive(false);

        Gameover_Text[0].GetComponent<TextMeshProUGUI>().text = "Microplastics Collected: " + Game_Control.Player_Money;
        Gameover_Text[1].GetComponent<TextMeshProUGUI>().text = "Total Kills: " + Game_Control.Game_Kills;

        //Checks to see if the player has enough money saved up to buy an upgrade from the shop.
        //I would like to condense this but I am not sure how.
        if (Master_Manager.Money_Saved >= Master_Manager.S_Health_Upgrade_Cost || Master_Manager.Money_Saved >= Master_Manager.S_Speed_Upgrade_Cost || Master_Manager.Money_Saved >= Master_Manager.S_Luck_Upgrade_Cost || Master_Manager.Money_Saved >= Master_Manager.S_Attack_Damage_Upgrade_Cost || Master_Manager.Money_Saved >= Master_Manager.S_Attack_Speed_Upgrade_Cost || Master_Manager.Money_Saved >= Master_Manager.S_Attack_Size_Upgrade_Cost)
        {
            Gameover_Text[2].SetActive(true);
        }
        
    }

    private void OnDisable()
    {
        Game_Control.Game_Over_UI_Enabled = false;
    }
}
