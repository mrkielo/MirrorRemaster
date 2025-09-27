using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Hero heroLeft;
    [SerializeField] Hero heroRight;
    [SerializeField] SaveSystem saveSystem;


    [Header("Data Objects")]
    [SerializeField] LevelData levelData;
    [SerializeField] WorldData worldData;
    [SerializeField] PlayerData playerData;


    List<Hero> heroes = new List<Hero>();
    InputSystem_Actions inputActions;

    bool canMove = true;


    float startTime;

    void Awake()
    {
        heroes.Add(heroLeft);
        heroes.Add(heroRight);
        //Inject Data into heroes
        foreach (var hero in heroes)
        {
            hero.worldData = worldData;
            hero.playerData = playerData;
            hero.levelData = levelData;
        }
    }

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Disable();
        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.DashLeft.performed += DashLeft;
        inputActions.Player.DashRight.performed += DashRight;

        SubscribeHeroEvents();
        saveSystem.saveData.unlockedLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (CheckWin())
        {
            Win();
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 dir = AnalogToDigital(inputActions.Player.Move.ReadValue<Vector2>());
        if (!canMove) dir = Vector2.zero;
        foreach (var hero in heroes)
        {
            hero.Move(dir.x);
        }
    }

    Vector2 AnalogToDigital(Vector2 dir)
    {
        float x = dir.x, y = dir.y;

        if (dir.x > 0.2) x = 1;
        else if (dir.x < -0.2) x = -1;
        else x = 0;
        // if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x)) x = 0;

        return new Vector2(x, y);
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (!canMove) return;
        foreach (var hero in heroes)
        {
            hero.TryJump();
        }
    }

    void DashLeft(InputAction.CallbackContext ctx)
    {
        heroLeft.TryDash();
    }

    void DashRight(InputAction.CallbackContext ctx)
    {
        heroRight.TryDash();
    }

    void GameOver()
    {
        saveSystem.saveData.deathCount++;
        Debug.Log(saveSystem.saveData.deathCount);
        canMove = false;
        Invoke("ReloadScene", 1f);
    }
    void Win()
    {
        canMove = false;
        Debug.Log("Win");
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    bool CheckWin()
    {
        foreach (Hero hero in heroes)
        {
            if (!hero.CheckPortal())
            {
                return false;
            }
        }
        return true;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SubscribeHeroEvents()
    {
        foreach (Hero hero in heroes)
        {
            hero.OnDeath += GameOver;
        }
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

}
