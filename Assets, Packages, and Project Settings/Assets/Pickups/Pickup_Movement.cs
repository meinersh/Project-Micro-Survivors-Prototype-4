using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Movement : MonoBehaviour
{
    private GameObject Player;
    private float Pickup_Speed;
    private bool Picked_Up = false;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //Find the player object
        Pickup_Speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == true)
        {
            distance = Vector2.Distance(this.transform.position, Player.transform.position);

            if(distance < Game_Control.Player_Pickup_Range || Picked_Up == true || Game_Control.Pickup_Suck == true)
            {
                Picked_Up = true;
                float _x, _y; //Temp variables.
                Vector3 direction = Player.transform.position - this.transform.position;
                if(direction.x < 0)
                {
                    _x = -Pickup_Speed;
                }
                else
                {
                    _x = Pickup_Speed;
                }
                if (direction.y < 0)
                {
                    _y = -Pickup_Speed;
                }
                else
                {
                    _y = Pickup_Speed;
                }
                this.transform.position += new Vector3(_x * Time.deltaTime, _y * Time.deltaTime, 0) ;

                Pickup_Speed = Pickup_Speed + 0.1f * Time.deltaTime;
            }
            
        }
    }
}