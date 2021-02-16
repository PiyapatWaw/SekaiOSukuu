using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemUpdateAction : MonoBehaviour
{
    public BattlePlayerInputUI uiInstance;
    public GameSystem gameSystem;
    public BattleGraphicUpdate graphicCtrl;
    public BuffAndGuardPanelUI buffGuardPanel;

    Action actionUpdateComplete;

    float methodDelayTime = 1f;

    public void DoAction()
    {
        int selectaction = GameSystem.Instanst.ButtonExecuteIndex;
        Player p = GameSystem.Instanst._playerParty[GameSystem.Instanst.playerExecuteIndex];
        if (p.actionDatas[selectaction] is Attack)
            StartCoroutine(PlayerAttack(p,
                                        selectaction, 
                                        GameSystem.Instanst.UpdateBattleState));
        if (p.actionDatas[selectaction] is Guard)
            StartCoroutine(ExecuteBuff(()=> 
            {
                buffGuardPanel.TweenIn();
                p.CharacterState = CharacterState.WaitToExecute;
                buffGuardPanel.RegiserAction(()=> PlayerGuard(p, GameSystem.Instanst._playerParty[GameSystem.Instanst.SelectTarget], 
                                                              selectaction,
                                                              GameSystem.Instanst.UpdateBattleState));
            }));

        if (p.actionDatas[selectaction] is Heal)
            StartCoroutine(ExecuteBuff(() =>
            {
                buffGuardPanel.TweenIn();
                p.CharacterState = CharacterState.WaitToExecute;
                buffGuardPanel.RegiserAction(() => PlayerHeal(p, GameSystem.Instanst._playerParty[GameSystem.Instanst.SelectTarget],
                                                              selectaction,
                                                              GameSystem.Instanst.UpdateBattleState));
            }));

        if (p.actionDatas[selectaction] is Buff)
            StartCoroutine(ExecuteBuff(() =>
            {
                Buff mybuff = p.actionDatas[selectaction] as Buff;
                Character Target = gameSystem._enemy;
                if(mybuff.TargetType == BuffTarget.Allies)
                {
                    buffGuardPanel.TweenIn();
                }
                p.CharacterState = CharacterState.WaitToExecute;
                buffGuardPanel.RegiserAction(() => PlayerBuff(p, mybuff.TargetType == BuffTarget.Allies ? GameSystem.Instanst._playerParty[GameSystem.Instanst.SelectTarget] : Target,
                                                              selectaction,
                                                              GameSystem.Instanst.UpdateBattleState), mybuff.TargetType == BuffTarget.Enemy);
            }));
    }

    IEnumerator ExecuteBuff(Action OnComplete = null)
    {
        yield return new WaitForSeconds(methodDelayTime);
        OnComplete?.Invoke();
    }

    IEnumerator PlayerAttack(Player P,int Action, Action OnComplete)
    {
        yield return new WaitForSeconds(methodDelayTime);
        P.CharacterState = CharacterState.Execute;
        P.DoAction(Action, GameSystem.Instanst._enemy);
        graphicCtrl.TweenEnemyGetHit(()=> 
        {
            graphicCtrl.TweenAllCharacterToDefault(0.5f);
            P.CharacterState = CharacterState.EndTurn;
            OnComplete?.Invoke();
        });
    }

    void PlayerGuard(Player P, Character target, int Action, Action OnComplete)
    {
        P.CharacterState = CharacterState.Execute;
        P.DoAction(Action, target);
        graphicCtrl.TweenEnemyGetHit(() =>
        {
            graphicCtrl.TweenAllCharacterToDefault(0.5f);
            buffGuardPanel.executeAction = null;
            P.CharacterState = CharacterState.EndTurn;
            OnComplete?.Invoke();
        });
    }

    void PlayerHeal(Player P,Character target, int Action, Action OnComplete)
    {
        P.CharacterState = CharacterState.Execute;
        P.DoAction(Action, target);
        graphicCtrl.TweenEnemyGetHit(() =>
        {
            graphicCtrl.TweenAllCharacterToDefault(0.5f);
            buffGuardPanel.executeAction = null;
            P.CharacterState = CharacterState.EndTurn;
            OnComplete?.Invoke();
        });
    }

    void PlayerBuff(Player P, Character target, int Action, Action OnComplete)
    {
        Debug.Log("PlayerBuff");
        P.CharacterState = CharacterState.Execute;
        P.DoAction(Action, target);
        graphicCtrl.TweenEnemyGetHit(() =>
        {
            graphicCtrl.TweenAllCharacterToDefault(0.5f);
            buffGuardPanel.executeAction = null;
            P.CharacterState = CharacterState.EndTurn;
            OnComplete?.Invoke();
        });
    }


}
