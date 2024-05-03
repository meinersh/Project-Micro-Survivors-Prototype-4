using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Suck : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(gameObject.name + "has spawned");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game_Control.Pickup_Suck = true;
            Game_Control.Pickup_Suck_Duration = 0.1f;
            Destroy(gameObject);
        }
    }
}
