using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Hero heroLeft;
    [SerializeField] Hero heroRight;
    List<Hero> heroes = new List<Hero>();
    bool canMove = true;
    InputSystem_Actions inputActions;


    void Awake()
    {
        heroes.Add(heroLeft);
        heroes.Add(heroRight);
    }

    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Disable();
        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += Jump;
        SubscribeHeroEvents();
    }

    void Update()
    {
        if (CheckWin())
        {
            Debug.Log("Win");
            //TODO: Win things
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 dir = inputActions.Player.Move.ReadValue<Vector2>();
        if (!canMove) dir = Vector2.zero;
        foreach (var hero in heroes)
        {
            hero.Move(dir.x);
        }
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (!canMove) return;
        foreach (var hero in heroes)
        {
            if (hero.CanJump()) hero.Jump();
        }
    }

    void GameOver()
    {
        canMove = false;
        Invoke("ReloadScene", 1f);
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
