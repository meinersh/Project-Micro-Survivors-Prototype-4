using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;

public class Main_Game_Testing
{
    [SetUp]
    public void Setup()
    {
        //game = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game_Level"));
        SceneManager.LoadScene("Testing Scene");
    }

    [TearDown]
    public void Teardown()
    {

    }

    [UnityTest]
    public IEnumerator _01_Game_Controller_Created()
    {
        yield return new WaitForSecondsRealtime(0);

        Assert.True(GameObject.FindGameObjectWithTag("Game Controller"), "The Game Controller was not found");
    }

    [UnityTest]
    public IEnumerator _02_GameOverWhenPlayersHealthReachesZero()
    {
        Game_Control.Player_Health = 0; //Kills the player

        yield return new WaitForSecondsRealtime(0.1f); //Gives the game time to run everything

        Assert.True(Game_Control.Game_Over_UI_Enabled);
    }

    [UnityTest]
    public IEnumerator _03_UpgradeMenuOpensAfterLevelingUp()
    {
        Game_Control.Player_XP = Game_Control.Player_XP_To_Level; //Levels up the player

        yield return new WaitForSecondsRealtime(0.1f);

        Assert.True(Game_Control.UpgradeMenuOpen);
    }

    [Test]
    public void _04_GamePauses()
    {
        Game_Control.PauseGame();

        Assert.True(Time.timeScale == 0);
    }

    [UnityTest]
    public IEnumerator _05_GameResumes()
    {
        Game_Control.PauseGame();

        yield return new WaitForSecondsRealtime(0.1f);

        Game_Control.ResumeGame();

        Assert.True(Time.timeScale == 1);
    }

    [UnityTest]
    public IEnumerator _06_PlayerCreated()
    {
        yield return new WaitForSecondsRealtime(0);

        Assert.True(GameObject.FindWithTag("Player"), "The Player was not found");
    }

    [UnityTest]
    public IEnumerator _07_EnemySpawned()
    {
        Enemy_Spawner _Enemy_Spawner = GameObject.Find("Enemy Spawner").GetComponent<Enemy_Spawner>();

        _Enemy_Spawner.SpawnEnemy(0, 1); //Spawns 1 basic enemy

        yield return new WaitForSecondsRealtime(0.01f);

        Assert.True(GameObject.FindGameObjectWithTag("Basic Enemy"));
    }

    [UnityTest]
    public IEnumerator _08_PlayerTakesDamageFromEnemy()
    {
        Enemy_Spawner _Enemy_Spawner = GameObject.Find("Enemy Spawner").GetComponent<Enemy_Spawner>();

        float Start_HP = Game_Control.Player_Health;
        Debug.Log("Player_Health: " + Start_HP);

        yield return new WaitForSecondsRealtime(0.1f);

        _Enemy_Spawner.SpawnEnemy(0, 1, false, true, 0, 0); //Spawns a basic enemy at the center of the player

        yield return new WaitForSecondsRealtime(0.1f);

        float Final_HP = Game_Control.Player_Health;
        Debug.Log("Player_Health: " + Final_HP);

        Assert.Less(Final_HP, Start_HP, "Player HP is the same or greater than the start of the test");
    }

    [UnityTest]
    public IEnumerator _09_EnemyTakesDamageFromPlayerBullet()
    {
        //Gets the shoot script and the enemy spawner
        Player_Shoot _Player_Shoot = GameObject.FindWithTag("Player").GetComponent<Player_Shoot>();
        Enemy_Spawner _Enemy_Spawner = GameObject.Find("Enemy Spawner").GetComponent<Enemy_Spawner>();

        GameObject _Enemy = _Enemy_Spawner.SpawnEnemyAndReturn(0,false,true,5,0); //Spawns the enemy at (5,0) relative to the player

        float _Enemy_Start_HP = _Enemy.GetComponent<Enemy_Manager>().Enemy_health; //Stores the enemy's starting health

        yield return new WaitForSecondsRealtime(0.1f);

        _Player_Shoot.Bullet_Create(_Enemy.transform.position.x,_Enemy.transform.position.y, false); //Spawns the bullet at the center of the enemy

        yield return new WaitForSecondsRealtime(0.1f);

        float _Enemy_Final_HP = _Enemy.GetComponent<Enemy_Manager>().Enemy_health; //Stores the enemy's new health value

        Assert.Less(_Enemy_Final_HP, _Enemy_Start_HP, _Enemy_Final_HP + " is equal or greater to " + _Enemy_Start_HP);
    }

    [UnityTest]
    public IEnumerator _10_PlayerProjectileMoves()
    {
        Player_Shoot _Player_Shoot = GameObject.FindWithTag("Player").GetComponent<Player_Shoot>();

        GameObject _Bullet = _Player_Shoot.Bullet_Create();

        Vector3 _Bullet_Start = _Bullet.transform.position;

        yield return new WaitForSecondsRealtime(0.01f);

        Vector3 _Bullet_Final = _Bullet.transform.position;

        Assert.AreNotEqual(_Bullet_Start, _Bullet_Final, "Bullet Positions are the same");
    }

    [UnityTest]
    public IEnumerator _11_AttackUpgradeIncreaseBulletDamage()
    {
        Game_Control.ResetPlayerStats(); //Makes sure player stats are at the default
        
        GameObject Player = GameObject.FindWithTag("Player"); //Finds the Player

        GameObject Bullet1 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.Attack_Damage_Upgrade_Level++; //Increase the players attack

        Game_Control.UpdatePlayerStats(); //Makes sure the players stats are updated

        GameObject Bullet2 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        //Stores the bullets damage
        float Bullet1_Damage = Bullet1.GetComponent<Bullet>().Bullet_Damage;
        float Bullet2_Damage = Bullet2.GetComponent<Bullet>().Bullet_Damage;

        //Checks if the bullet2 damage is greater than Bullet1 damage.
        Assert.Greater(Bullet2_Damage, Bullet1_Damage, Bullet2_Damage + " is less or equal to " + Bullet1_Damage);
    }

    [UnityTest]
    public IEnumerator _12_AttackSpeedUpgradeIncreaseFireRate()
    {
        Game_Control.ResetPlayerStats();

        float _Start = Game_Control.Player_Attack_Speed;

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.Attack_Speed_Upgrade_Level++;

        Game_Control.UpdatePlayerStats();

        yield return new WaitForSecondsRealtime(0.01f);

        float _Final = Game_Control.Player_Attack_Speed;

        Assert.Greater(_Final, _Start, _Final + " is less or equal to " + _Start);
    }

    [UnityTest]
    public IEnumerator _13_AttackSizeUpgradeIncreaseBulletScale()
    {
        Game_Control.ResetPlayerStats();

        GameObject Player = GameObject.FindWithTag("Player");

        GameObject Bullet1 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.Attack_Size_Upgrade_Level++;

        Game_Control.UpdatePlayerStats(); //Makes sure the players stats are updated

        GameObject Bullet2 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        float Bullet1_Size = Bullet1.GetComponent<Bullet>().Bullet_Size;
        float Bullet2_Size = Bullet2.GetComponent<Bullet>().Bullet_Size;

        //Checks if the bullet2 damage is greater than Bullet1 damage.
        Assert.Greater(Bullet2_Size, Bullet1_Size, Bullet2_Size + " is less or equal to " + Bullet1_Size);
    }

    [Test]
    public void _14_MaxHealthUpgradeIncreasePlayerHealth()
    {
        //Loads Default values into the player.
        Game_Control.ResetPlayerStats();
        Game_Control.UpdatePlayerStats();

        float starting_health = Game_Control.Player_Health_Max;

        Game_Control.Health_Upgrade_Level++;
        Game_Control.UpdatePlayerStats();

        Assert.AreNotEqual(starting_health, Game_Control.Player_Health_Max);
    }

    [UnityTest]
    public IEnumerator _15_PlayerSpeedUpgradeIncreasePlayerMovementSpeed()
    {
        Game_Control.ResetPlayerStats();

        float _Start = Game_Control.Player_Speed;

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.Speed_Upgrade_Level++;
        Game_Control.UpdatePlayerStats();

        yield return new WaitForSecondsRealtime(0.01f);

        float _Final = Game_Control.Player_Speed;

        Assert.Greater(_Final, _Start, _Final + " is less than or equal to " + _Start);
    }

    [UnityTest]
    public IEnumerator _16_ProjectileDurationUpgradeIncreaseProjectileLifeSpan()
    {
        Game_Control.ResetPlayerStats();

        GameObject Player = GameObject.FindWithTag("Player");

        GameObject Bullet1 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.Projectile_Duration_Level++;

        Game_Control.UpdatePlayerStats(); //Makes sure the players stats are updated

        GameObject Bullet2 = Player.GetComponent<Player_Shoot>().Bullet_Create(); //Spawns a bullet and stores it

        yield return new WaitForSecondsRealtime(0.01f);

        float Bullet1_Duration = Bullet1.GetComponent<Bullet>().Bullet_Duration;
        float Bullet2_Duration = Bullet2.GetComponent<Bullet>().Bullet_Duration;

        //Checks if the bullet2 damage is greater than Bullet1 damage.
        Assert.Greater(Bullet2_Duration, Bullet1_Duration, Bullet2_Duration + " is less or equal to " + Bullet1_Duration);
    }

    [UnityTest]
    public IEnumerator _17_ResetPlayerStats()
    {
        Game_Control.ResetPlayerStats();

        Game_Control.Attack_Damage_Upgrade_Level++;

        Game_Control.UpdatePlayerStats();

        float _Start = Game_Control.Player_Attack_Damage;

        yield return new WaitForSecondsRealtime(0.01f);

        Game_Control.ResetPlayerStats();

        Game_Control.UpdatePlayerStats();

        float _Final = Game_Control.Player_Attack_Damage;

        Assert.Greater(_Start, _Final, _Start + " is less than or equal to " + _Final);
    }

    [UnityTest]
    public IEnumerator _18_SpawnUpgradeChoice()
    {
        Game_Control.Player_XP = Game_Control.Player_XP_To_Level;

        yield return new WaitForSecondsRealtime(0.01f);

        Assert.True(GameObject.FindGameObjectWithTag("Upgrade Choice"));
    }
}
