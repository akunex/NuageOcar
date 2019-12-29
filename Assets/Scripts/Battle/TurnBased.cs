using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class TurnBased : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;


    PlayerStat playerUnit;
    CMotor_test playerMovement;
    PlayerStat enemyUnit;

    public BattleState state;

    public bool isDead;

    private bool canMove;

    private void Start()
    {

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (canMove == true)
        {
            playerMovement.canMove = true;
        }
        else
        {
            playerMovement.canMove = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(playerMovement.canMove);
        }
        
    }

    IEnumerator SetupBattle()
    {
        
        GameObject playerGO = Instantiate(player, new Vector3(800, 3, 246), Quaternion.identity);
        playerUnit = playerGO.GetComponent<PlayerStat>();
        playerMovement = playerGO.GetComponent<CMotor_test>();

        canMove = false;

        //Same Enemy

        yield return new WaitForSeconds(10f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("Tour du joueur");
        canMove = true;
    }

    IEnumerator EnemyTurn()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void OnSpellButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        //Attaque

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }

    }

    void EndBattle()
    {
        canMove = false;
        if (state == BattleState.WON)
        {
            Debug.Log("You won the battle!");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("You were defeated.");
        }
    }

}
