//This Script Manages The Enemy's stats like health and damage.
//Detects when it collides with the player and then lowers their health.
//Detects when it's health is below 0 spawns XP and then deletes itself.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    //Enemy Stats
    public float Enemy_damage;
    public float Enemy_health;

    public GameObject XP_Pickup;
    public GameObject[] Rand_Pickup;

    public float Base_XP_Value;
    public float Base_Money_Value;
    public float Base_Money_Spawn_Chance;

    private void Awake()
    {
        Enemy_health *= 1 + (Game_Control.Player_Level / 50);
        Game_Control.Total_Enemies++;
    }

    // Update is called once per frame
    void Update()
    {
        //If health is less than or equal to zero
        if(Enemy_health <= 0)
        {
            //Increase the amount of kills this game
            Game_Control.Game_Kills++;

            //Creates an XP pick up where the enemy died.
            SpawnXP();
            //Creates a Money Pick up
            SpawnPickup(2);
            //Deletes itself
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player Collision
        if (collision.gameObject.CompareTag("Player"))
        {
            //Lowers the player's health by the enemies damage
            Game_Control.Player_Health -= Enemy_damage;
        }
    }

    private void OnDestroy()
    {
        //Lowers the amount of total enemies
        Game_Control.Total_Enemies--;
    }

    private void SpawnXP()
    {
        var XP = Instantiate(XP_Pickup, gameObject.transform.position, gameObject.transform.rotation);
        XP.GetComponent<XP_Collision>().XP_Amount = Base_XP_Value;
    }

    private void SpawnPickup(int amount)
    {
        //Always a chance to sapwn money
        float _temp_value = Random.Range(1, 101); //Picks a random number between 1 and 100

        if (_temp_value >= Base_Money_Spawn_Chance / Game_Control.Player_Luck)
        {
            var Money = Instantiate(Rand_Pickup[0], gameObject.transform.position, gameObject.transform.rotation);
            Money.GetComponent<MoneyPickupControl>().Value = Base_Money_Value;
        }

        amount--;

        while (amount > 0)
        {
            int _temp_int = Random.Range(1, Rand_Pickup.Length);
            _temp_value = Random.Range(1, 101); //Picks a random number between 1 and 100

            if (_temp_value >= 99 / Game_Control.Player_Luck)
            {
                Instantiate(Rand_Pickup[_temp_int], gameObject.transform.position, gameObject.transform.rotation);
            }
            amount--;
        }
    }
}
