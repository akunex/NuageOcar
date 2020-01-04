using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class TurnBased : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;


    PlayerStat playerUnit;
    CMotor_test playerMovement;
    SpellOne playerSpell;
    PlayerStat enemyUnit;

    public GameObject bg_info;
    public GameObject player_turn_text;
    public GameObject enemy_turn_text;

    public BattleState state;

    public bool isDead;

    private bool canMove;


    public Button spell1;

    private void Start()
    {

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (canMove == true && playerSpell.useSpell != true)
        {
            playerMovement.canMove = true;
        }
        else
        {
            playerMovement.canMove = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnSpellButton();
        spell1.onClick.AddListener(OnSpellButton);

    }

    IEnumerator SetupBattle()
    {
        
        GameObject playerGO = Instantiate(player, new Vector3(800, 3, 246), Quaternion.identity);
        playerGO.name = "Lady Pirate";
        playerUnit = playerGO.GetComponent<PlayerStat>();
        playerMovement = playerGO.GetComponent<CMotor_test>();
        playerSpell = playerGO.GetComponent<SpellOne>();
    
        canMove = false;

        //Same Enemy
        enemyUnit = enemy.GetComponent<PlayerStat>();

        yield return new WaitForSeconds(0);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        canMove = true;
        bg_info.SetActive(true);
        player_turn_text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        player_turn_text.SetActive(false);
        bg_info.SetActive(false);
    }

    IEnumerator EnemyTurn()
    {
        canMove = false;
        bg_info.SetActive(true);
        enemy_turn_text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        enemy_turn_text.SetActive(false);
        bg_info.SetActive(false);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
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
        playerSpell.ActivateSpell();

        //Attaque

        yield return new WaitForSeconds(2f);
        if(enemyUnit.currentHealth <= 0)
        {
            isDead = true;
        }
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
