using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    Player,
    Enemy,
    EndLoopCalc
}

public enum CharacterState
{
    OnWait,
    WaitToExecute,
    Execute,
    EndTurn,
    IsDead
}

public abstract class BaseGameState : MonoBehaviour
{
    [Header("SetUp Data")]
    protected int _maxTurnLeft = 0;
    protected const int _maxPlayerTurnIndex = 3;
    [Header("GamePlay Data")]
    public BattleState battleState;
    public int playerTurnIndex =0;
    public int _turnLeft;

    [Header("EndGame Condition")]
    public bool _partyIsWipedOut;
    public bool _surviveForTenTurn;
    #region Unity Callback

    public virtual void Awake()
    {
       
    }

    public virtual void Start()
    {
        InitData();
    }

    public virtual void Update()
    {

    }
    #endregion

    protected void InitData()
    {
        _maxTurnLeft = GameSystem.Instanst._enemy.ActionQue.Count;
        _turnLeft = _maxTurnLeft;
    }

    protected void EndturnLoop()
    {
        _turnLeft--;
    }

    protected void EndGame(bool isWin, Action WinAction, Action LoseAction)
    {
        if (isWin)
        {
            WinAction?.Invoke();

            int enemyid = PlayerPrefs.GetInt("EnemyID", 0);
            enemyid++;
            PlayerPrefs.SetInt("EnemyID", enemyid);
            if (enemyid == GameSystem.Instanst.AllEnemy.Count)
            { PlayerPrefs.SetString("DialogNextScene", "Credit"); PlayerPrefs.SetInt("ShowEndCredit", 1); }
            else
                PlayerPrefs.SetString("DialogNextScene", "Select");
            SceneManager.LoadScene("Dialog");
        }

        else
        {
            LoseAction?.Invoke();
            SceneManager.LoadScene("Select");
        }

        

    }

    public void CheckWinLose(Action WinAction, Action LoseAction)
    {
        _surviveForTenTurn = _turnLeft <= 0;

        if (_partyIsWipedOut)
        {
            EndGame(false, WinAction, LoseAction);
        }

        else if (_surviveForTenTurn)
        {
            _turnLeft = 0;
            EndGame(true, WinAction, LoseAction);
        }
    }
}
