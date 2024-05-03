using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    // enemyPrefab Index Reference
    // 0. Basic Enemy
    // 1. Rhinovirus
    // 2. Mimivirus 
    // 3. Influenza Virus
    // 4. Varicella Virus
    // 5. Tanky Enemy
    // 6. Straight Line Enemy

    [SerializeField] private float Spawn_Distance_From_Player = 18;

    private float Time_Until_Spawn;

    private GameObject Player;

    private int[] Available_Enemies;

    private int Spawn_Multiplier;

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == true)
        {
            this.transform.position = Player.transform.position;
        }
            

        Time_Until_Spawn -= Time.deltaTime;

        if(Time_Until_Spawn <= 0 && Game_Control.Total_Enemies <= Game_Control.Max_Enemies)
        {
            for (int i = 0; i < Spawn_Multiplier; i++)
            {
                int choice = PickEnemy();
                switch (choice)
                {
                    case 0: //Basic Enemy
                    {
                        SpawnEnemy(choice, 4);
                        break;
                    }
                    case 1: //Rhinovirus
                    {
                        SpawnEnemy(choice, 12);
                        break;
                    }
                    case 2: //Mimivirus
                    {
                        SpawnEnemy(choice, 1);
                        break;
                    }
                    case 3: //Influenza Virus
                    {
                        SpawnEnemy(choice, 2);
                        break;
                    }
                    case 4: //Varicella Virus
                    {
                        SpawnEnemy(choice, 3);
                        break;
                    }
                    case 5: //Tanky Enemy
                    {
                        SpawnEnemy(choice, 2);
                        break;
                    }
                    case 6: //Straight Line Enemy
                    {
                        SpawnEnemy(choice, 4);
                        break;
                    }
                }
            }
            SetTimeUntilSpawn();
        }
    }


    //sets the time till the next spawn and controls which enemies need to spawn.
    private void SetTimeUntilSpawn()
    {
        switch (Game_Control.Game_Total_Time)
        {
            case (<= 20): //Checks if current game time is less than or greater than 20
                Time_Until_Spawn = Random.Range(1, 3); //Range of seconds till the next time an enemy can spawn.
                Available_Enemies = new int[] { 0 }; //Array of emenies that will be allowed to spawn
                Spawn_Multiplier = 1; //Multiplier to the amount of enmies that will be spawned. The deafult is 1.
                break;

            case (> 20 and <= 40): //Checks if the current game time is greater than 20 but less than or equal to 40.
                Time_Until_Spawn = Random.Range(1, 3);
                Available_Enemies = new int[] { 0 , 1};
                Spawn_Multiplier = 1;
                break;

            //Enemy waves are now ususally changed every 40 seconds.
            case (> 40 and <= 80):
                Time_Until_Spawn = Random.Range(1, 3);
                Available_Enemies = new int[] { 0 , 1, 3};
                Spawn_Multiplier = 1;
                break;

            case (> 80 and <= 120):
                Time_Until_Spawn = Random.Range(1, 2);
                Available_Enemies = new int[] { 0 , 1, 3, 4};
                Spawn_Multiplier = 1;
                break;

            case (> 120 and <= 160):
                Time_Until_Spawn = Random.Range(1, 2);
                Available_Enemies = new int[] { 0, 3};
                Spawn_Multiplier = 2;
                break;

            case (> 160 and <= 200):
                Time_Until_Spawn = Random.Range(1, 2);
                Available_Enemies = new int[] { 0, 3};
                Spawn_Multiplier = 3;
                break;

            case (> 200 and <= 240):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 3, 4 };
                Spawn_Multiplier = 1;
                break;

            case (> 240 and <= 280):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] {3, 5};
                Spawn_Multiplier = 1;
                break;

            case (> 280 and <= 320):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 3, 5, 6 };
                Spawn_Multiplier = 1;
                break;

            case (> 320 and <= 360):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 3, 5, 6};
                Spawn_Multiplier = 2;
                break;

            case (> 360 and <= 400):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] {5, 6 };
                Spawn_Multiplier = 1;
                break;

            case (> 400 and <= 440):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] {5, 6 };
                Spawn_Multiplier = 2;
                break;

            case (> 440 and <= 480):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 6 };
                Spawn_Multiplier = 1;
                break;

            case (> 480 and <= 520):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 6 };
                Spawn_Multiplier = 2;
                break;

            case (> 520 and <= 560):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 6 };
                Spawn_Multiplier = 4;
                break;

            case (> 560 and <= 600):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2 };
                Spawn_Multiplier = 1;
                break;

            case (> 600 and <= 640):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 2;
                break;

            case (> 640 and <= 680):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 3;
                break;

            case (> 680 and <= 720):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2 };
                Spawn_Multiplier = 4;
                break;

            case (> 720 and <= 760):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 5;
                break;

            case (> 760 and <= 800):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 6;
                break;

            case (> 800 and <= 840):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 7;
                break;
            case (> 840 and <= 880):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 8;
                break;
            case (> 880 and <= 920):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 9;
                break;
            case (> 920 and <= 960):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 9;
                break;
            case (> 960 and <= 1000):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 2, 6 };
                Spawn_Multiplier = 9;
                break;
            case (> 1000):
                Time_Until_Spawn = Random.Range(0.5f, 1);
                Available_Enemies = new int[] { 6 };
                Spawn_Multiplier = 40;
                break;
        }
    }


    /// Spawn Enemy Summary
    /// type is the index of the enemy in the enemyPrefab array
    /// amount is the total amount of enemys that you want to spawn
    /// _randmom_spawn is optional. It decides if _x & _y should be random values. NOTE: If you enter values for _x or _y while this is true they will be overriden
    /// _relative_to_player is optional. 
    /// _x is optional. Exact x position you want the enemy to spawn at.
    /// _y is optional. Exact y position you want the enemy to spawn at.
    public void SpawnEnemy(int type, int amount,bool _random_spawn=true, bool _relative_to_player=true, float _x = 0, float _y = 0)
    {
        Vector3 Final_Spawn_Point;

        for (int i = 0; i < amount; i++)
        {
            if (_random_spawn == true)
            {
                (_x, _y) = Get_Spawn_X_Y(); //Gets the random spawn position
            }

            if (_relative_to_player == true)
            {
                Final_Spawn_Point = new Vector3(Player.transform.position.x + _x, Player.transform.position.y + _y, 0); //Adds the player position to the random position.
            }
            else
            {
                Final_Spawn_Point = new Vector3(_x, _y, 0);
            }
            
            Instantiate(enemyPrefab[type], Final_Spawn_Point, Quaternion.identity); //Spawns the Enemy
        }
    }
    private int PickEnemy()
    {
        int _option; //Temp Variable

        //Generates a random number between 0 and the length of the available enemies
        _option = Random.Range(0, Available_Enemies.Length);

        //returns value in Available_Enemies at the random position
        return Available_Enemies[_option];
    }

    (float, float) Get_Spawn_X_Y()
    {
        int _temp_int = Random.Range(0, 2); //If 0 Then above or below the player. If 1 then to the right or left of the player

        float _x = 0, _y = 0;

        if (_temp_int == 0)
        {
            int _Up_Or_Down = Random.Range(0, 2); //Chooses if y is negative or positive

            if (_Up_Or_Down == 0) //Above
            {
                _y = Spawn_Distance_From_Player;
            }
            else //Below
            {
                _y = -Spawn_Distance_From_Player;
            }

            _x = Random.Range(-Spawn_Distance_From_Player, Spawn_Distance_From_Player);
        }
        else if (_temp_int == 1)
        {
            int _Right_Or_Left = Random.Range(0, 2); //Chooses if x is negative or positive

            if (_Right_Or_Left == 0) //Right
            {
                _x = Spawn_Distance_From_Player;
            }
            else //Left
            {
                _x = -Spawn_Distance_From_Player;
            }

            _y = Random.Range(-Spawn_Distance_From_Player, Spawn_Distance_From_Player);
        }
        else
        {
            Debug.Log("Enemy TP Error");
        }

        return (_x, _y);
    }


    // Spawn Enemy And Return Summary
    // type is the index of the enemy in the enemyPrefab array
    // _randmom_spawn is optional. It decides if _x & _y should be random values. NOTE: If you enter values for _x or _y while this is true they will be overriden
    // _relative_to_player is optional. 
    // _x is optional. Exact x position you want the enemy to spawn at.
    // _y is optional. Exact y position you want the enemy to spawn at.
    // Returns the enemy object at the end of function 
    public GameObject SpawnEnemyAndReturn(int type, bool _random_spawn = true, bool _relative_to_player = true, float _x = 0, float _y = 0)
    {
        Vector3 Final_Spawn_Point;

        if (_random_spawn == true)
        {
            (_x, _y) = Get_Spawn_X_Y(); //Gets the random spawn position  (
        }    

        if (_relative_to_player == true)
        {
            Final_Spawn_Point = new Vector3(Player.transform.position.x + _x, Player.transform.position.y + _y, 0); //Adds the player position to the random position.
        }
        else
        {
            Final_Spawn_Point = new Vector3(_x, _y, 0);
        }

        GameObject Enemy = Instantiate(enemyPrefab[type], Final_Spawn_Point, Quaternion.identity); //Spawns the Enemy

        return Enemy;
    }
}
