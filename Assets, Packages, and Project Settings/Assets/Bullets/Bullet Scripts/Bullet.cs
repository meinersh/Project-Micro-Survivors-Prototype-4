//Controls the Bullets stats and behaviors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Bullet : MonoBehaviour
{
    Rigidbody2D Rb2d;

    //Gets the Player Object for reference;
    public GameObject Player;

    //Creates the Damage number pop ups
    public GameObject Damage_Pop_Up;

    //Base Bullet Stats
    public float Base_Bullet_Damage;
    public float Base_Bullet_Size;
    public float Base_Bullet_Duration;
    public bool canBounce;
    private int maxBounces;

    //Final Bullet Stats
    public float Bullet_Damage;
    public float Bullet_Size;
    public float Bullet_Duration; //How long the bullet last before despawning naturally in seconds

    //Bullet Bounce Variables
    public int currentBounces = 0; //Should always start at 0

    public bool canPierce; //This is here because I think it would be cool but... I am not sure how to implement it.

    //Runs once at the when the bullet is created
    private void Start()
    {
        Rb2d = this.GetComponent<Rigidbody2D>();
        //Debug.Log("Bullet Spawned");

        //Turns Off collision between the bullet other player bullets
        Physics2D.IgnoreLayerCollision(7, 7, true);

        //Truns Off collision between the bullet and the player
        Physics2D.IgnoreLayerCollision(7, 3, true);

        //Truns Off collision between the bullet and enemy bullets
        Physics2D.IgnoreLayerCollision(7, 8, true);

        Bullet_Damage = Base_Bullet_Damage * Game_Control.Player_Attack_Damage;
        Bullet_Size = Base_Bullet_Size * Game_Control.Player_Attack_Size;

        gameObject.transform.localScale = new Vector3(Bullet_Size, Bullet_Size, Bullet_Size); //Stores the Bullet size into a vecotr3 so it can be used for the scale

        canBounce = Game_Control.Projectile_Bounce;
        maxBounces = Game_Control.Max_Projectile_Bounce_Level + 2;
    }


    private void Awake()
    {
        Bullet_Duration = Base_Bullet_Duration * Game_Control.Player_Projectile_Duration;
        //Destroys Self after the amount second life is set to.
        Destroy(gameObject, Bullet_Duration);
    }

    private void Update()
    {
        transform.Rotate(0, 0, 60 * Time.deltaTime * (Rb2d.velocity.x + Rb2d.velocity.y)); //Adds the velocity of the bullet to change the direction of spin based on bullets direction as well as the speed.
    }

    //Runs when the object collides with another collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Wall Collision
        if (collision.gameObject.CompareTag("Wall") && canBounce != true)
        {
            Destroy(gameObject);
        }

        //Enemy and Boss Collision
        if (collision.gameObject.CompareTag("Basic Enemy") || CompareTag("Boss Enemy"))
        {
            //Gets the enemy's health then subtracts the bullets damage from it.
            collision.gameObject.GetComponent<Enemy_Manager>().Enemy_health -= Bullet_Damage;

            Game_Control.Damage_This_Game += Bullet_Damage; //Adds the damage to the total damage this game

            if (Game_Control.Highest_Single_Damage < Bullet_Damage) //Checks if the damage is higher than the current highest single instance of damage.
            {
                Game_Control.Highest_Single_Damage = Bullet_Damage; //Sets the value
            }

            Spawn_Damage_Number(Bullet_Damage);

            //deletes the bullet if bounce is turned off.
            if (canBounce != true)
            {
                Destroy(gameObject);
            }
        }

        //If the object bounces more than is allowed it will destroy itself.
        currentBounces += 1;
        if (currentBounces == maxBounces || currentBounces > maxBounces || canBounce == false)
        {
            Destroy(gameObject);
        }
    }

    public void Spawn_Damage_Number(float NumberToDisplay)
    {
        GameObject Prefab = Instantiate(Damage_Pop_Up, transform.position, Quaternion.identity); //Spawns a damage pop up
        Prefab.GetComponentInChildren<TextMesh>().text = NumberToDisplay.ToString(); //Assigns the pop up text to the damage number.

        Color NewColor = Color.white; //Declars white as the default if switch logic breaks

        //Changes the color of the number based on the damage value of the attack.
        switch (NumberToDisplay)
        {
            case (< 20):
            {
                NewColor = Color.white; //Value of white is (1,1,1,1)
                break;
            }

            case (>= 20 and < 40):
            {
                NewColor = new Color(1, 0.7f, 0.7f, 1);
                break;
            }

            case (>= 40 and < 60):
            {
                NewColor = new Color(1, 0.4f, 0.4f, 1);
                Prefab.GetComponentInChildren<TextMesh>().text += "!";
                break;
            }

            case (>= 60 and < 80):
            {
                NewColor = new Color(1, 0.2f, 0.2f, 1);
                Prefab.GetComponentInChildren<TextMesh>().text += "!!";
                break;
            }
            case (>= 80):
            {
                NewColor = Color.red; //Value of Red is (1,0,0,1)
                Prefab.GetComponentInChildren<TextMesh>().text += "!!!";
                break;
            }
        }
        Prefab.GetComponentInChildren<TextMesh>().color = NewColor; //Assigns the color to the object
    }
}
