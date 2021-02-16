using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : BaseGameState
{
    [Header("Character Data")]
    public static GameSystem Instanst;
    public BattleGraphicUpdate battleGraphic;
    public BattlePlayerInputUI battleUI;
    public GameSystemUpdateAction systemUpdate;
    public List<Player> _playerParty = new List<Player>();
    public List<Player> AllCharacter = new List<Player>();
    public List<Enemy> AllEnemy = new List<Enemy>();
    public Enemy _enemy;
    public Transform BattleMap;
    public int SelectTarget;
    public int playerExecuteIndex, ButtonExecuteIndex;
    [SerializeField] UIExtendComponent txtTurn;
    #region Unity Callback

    public override void Awake()
    {
        Instanst = this;
        base.Awake();
        List<int> currentparty = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            currentparty.Add(PlayerPrefs.GetInt(string.Format("Party{0}", i + 1)));
        }
        int index = 0;
        foreach (var item in AllCharacter)
        {
            if(currentparty.Contains(item.ID))
            {
                Player p = Instantiate(item, BattleMap);
                partyHP[index].Setup(p);
                p.CharacterState = CharacterState.OnWait;
                _playerParty.Add(p);
                index++;
            }
        }
        _enemy = Instantiate(AllEnemy[PlayerPrefs.GetInt("EnemyID",0)],BattleMap);
        enemyHP.Setup(_enemy);
        battleGraphic.InitInstanceOfCharacter(_playerParty, _enemy);
        _enemy.CharacterState = CharacterState.OnWait;
    }
    public override void Start()
    {
        base.Start();
        txtTurn.txt.text = _turnLeft + "Turn Left";
        InitPlayerTurn();
    }

    public override void Update()
    {
        base.Update();
    }
    #endregion

    public List<HpScript> partyHP = new List<HpScript>();
    public HpScript enemyHP;

    public void ExecutePlayerAction(Action playerAction)
    {
        playerAction?.Invoke();
    }

    public void UpdateBattleState()
    {
        List<Player> AllcanTarget = _playerParty.Where(w => w.Hp > 0).ToList();
        int selectTarget = UnityEngine.Random.Range(0, AllcanTarget.Count);
        switch (battleState)
        {
            case BattleState.Player:
                playerTurnCounter--;
                bool playerTurnLoopIsEnd = playerTurnCounter <= 0;

                if (playerTurnLoopIsEnd)
                {
                    playerTurnCounter = 0;
                    this.battleState = BattleState.Enemy;
                    UpdateBattleState();
                }

                else
                {
                    UpdatePlayerInputInteractable();
                    battleGraphic.TweenAllCharacterToDefault();
                }

                break;
            case BattleState.Enemy:
                //AI Enemy
                
                if (_enemy.actionDatas[_enemy.ActionQue[0] - 1] is Attack)
                {
                    battleGraphic.TweenEnemyToTarget(()=> 
                    {
                        _enemy.DoAction(_enemy.ActionQue[0] - 1, AllcanTarget[selectTarget]);
                        battleGraphic.TweenPlayerGetHit(selectTarget);
                    });

                }
                if(_enemy.actionDatas[_enemy.ActionQue[0] - 1] is Buff)
                {
                    Buff skill = _enemy.actionDatas[_enemy.ActionQue[0] - 1] as Buff;
                    battleGraphic.TweenEnemyToTarget(() => 
                    {
                        if (skill.TargetType is BuffTarget.me)
                        {
                            _enemy.DoAction(_enemy.ActionQue[0] - 1, _enemy);
                        }
                        else
                        {
                            _enemy.DoAction(_enemy.ActionQue[0] - 1, AllcanTarget[selectTarget]);
                        }
                    });
                }
                if(_enemy.actionDatas[_enemy.ActionQue[0] - 1] is Guard || _enemy.actionDatas[_enemy.ActionQue[0] - 1] is Heal)
                {
                    _enemy.DoAction(_enemy.ActionQue[0] - 1, _enemy);
                }
                _enemy.ActionQue.RemoveAt(0);
                Invoke("EndLoop",2f);
                break;
            case BattleState.EndLoopCalc:
                EndTurnLoop();
                break;
        }
    }

    

    public void EndLoop()
    {
        battleState = BattleState.EndLoopCalc;
        UpdateBattleState();
    }

    public int playerTurnCounter;

    public void InitPlayerTurn()
    {
        for (int i = 0; i < _playerParty.Count; i++)
        {
            if (!_playerParty[i].IsDead)
            {
                playerTurnCounter++;
            }
        }
    }

    public void UpdatePlayerInputInteractable()
    {
        for (int i = 0; i < _playerParty.Count; i++)
        {
            if (_playerParty[i].CharacterState != CharacterState.EndTurn)
            {
                battleUI.btnPartyList[i].ui.button.interactable = true;
            }

            else
            {
                battleUI.btnPartyList[i].ui.button.interactable = false;
            }
        }       
    }

    public void EndTurnLoop()
    {
        _turnLeft--;
        CheckWinLose(()=>Debug.Log("Win"), () => Debug.Log("Lose"));
        txtTurn.txt.text = _turnLeft + "Turn Left";
        InitPlayerTurn();

        for (int i = 0; i < _playerParty.Count; i++)
        {
            if (!_playerParty[i].IsDead)
            {
                _playerParty[i].CharacterState = CharacterState.OnWait;
            }
            _playerParty[i].DecressBuffTurn();
        }

        _enemy.DecressBuffTurn();

        battleState = BattleState.Player;
        UpdatePlayerInputInteractable();
        battleGraphic.TweenAllCharacterToDefault();
        // UpdateBattleState();
    }

}
