//Causes the enemy to find the player when created and everytime it is teleported. Otherwise it is same as AI_Chase.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Striaght_Line : MonoBehaviour
{
    public float DistancePerSecond;

    private GameObject player;
    private Rigidbody2D rb2D;

    private int Max_Distance_From_Player = 24;
    private int TP_Distance_From_Player = 18;

    private float distance;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

        //gets it's own rgidbody2D component
        rb2D = GetComponent<Rigidbody2D>();

        //Finds the player character to reference for calculations
        player = GameObject.FindGameObjectWithTag("Player");

        //Claculates the direction towards the player from it's current poistion
        direction = player.transform.position - this.transform.position;
        direction.Normalize(); //normalizes the dicection because it has 3 vectors when we only need 2.

        //Ignores Collision with other enemies
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if there is a player object in the scene
        if (player == true)
        {
            //Claculates how far it is from the player.
            distance = Vector2.Distance(transform.position, player.transform.position);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Atan2(target y, traget x)

            //moves the enemy towards the player
            rb2D.velocity = direction * DistancePerSecond;

            //rotates the enemy to face the player
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (distance > Max_Distance_From_Player)
            {
                TP_Near_Player();

                //Finds the direction to the Player Again
                direction = player.transform.position - this.transform.position;
                direction.Normalize();
            }
        }
    }

    private void TP_Near_Player()
    {
        float _x, _y;

        (_x, _y) = Get_TP_X_Y();

        gameObject.transform.position = new Vector3(player.transform.position.x + _x, player.transform.position.y + _y, 0); //Teleports the enemy close to the player but they should still be off screen.
    }

    private (float, float) Get_TP_X_Y()
    {
        int _temp_int = Random.Range(0, 2); //If 0 Then above or below the player. If 1 then to the right or left of the player

        int _Up_Or_Down = Random.Range(0, 2);
        int _Right_Or_Left = Random.Range(0, 2);

        float _x = 0, _y = 0;

        if (_temp_int == 0)
        {
            if (_Up_Or_Down == 0) //Above
            {
                _y = TP_Distance_From_Player;
            }
            else //Below
            {
                _y = -TP_Distance_From_Player;
            }

            _x = Random.Range(-TP_Distance_From_Player, TP_Distance_From_Player);
        }
        else if (_temp_int == 1)
        {
            if (_Right_Or_Left == 0) //Right
            {
                _x = TP_Distance_From_Player;
            }
            else //Left
            {
                _x = -TP_Distance_From_Player;
            }

            _y = Random.Range(-TP_Distance_From_Player, TP_Distance_From_Player);
        }
        else
        {
            Debug.Log("Enemy TP Error");
        }

        return (_x, _y);
    }
}
