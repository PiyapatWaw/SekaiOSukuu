using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

public class BattleGraphicUpdate : MonoBehaviour
{
    public CinemachineVirtualCamera Defaultcam;
    public CinemachineVirtualCamera Pcam;
    public CinemachineVirtualCamera Ecam;

    public List<Player> playerPrefabs = new List<Player>();
    public Enemy enemyPrefabs;

    [Header("Pose")]
    public List<Transform> playerPos = new List<Transform>();
    public Transform enemyPos;
    public Transform playerOnActionPos;
    public Transform enemyOnActionPos;


    public void InitInstanceOfCharacter(List<Player> pList, Enemy enemy)
    {
        playerPrefabs = pList;
        enemyPrefabs = enemy;

        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            playerPrefabs[i].gameObject.transform.position = playerPos[i].transform.position;
            playerPrefabs[i].gameObject.transform.rotation = playerPos[i].transform.rotation;
        }
        enemyPrefabs.transform.position = enemyPos.transform.position;
        enemyPrefabs.transform.rotation = enemyPos.transform.rotation;
    }



    #region Camera
    public void UpdateFollowCamera(int playerIndex)
    {
        Defaultcam.gameObject.SetActive(false);
        Pcam.gameObject.SetActive(true);
        Ecam.gameObject.SetActive(false);

        TweenChosenPlayerTotarget(playerIndex, playerOnActionPos.position);
        Pcam.LookAt = playerPrefabs[playerIndex].CharacterGraphic.cameraFocusPoint.transform;
    }

    public void UpdateCameraDefaultView()
    {
        Defaultcam.gameObject.SetActive(true);
        Pcam.gameObject.SetActive(false);
        Ecam.gameObject.SetActive(false);

        
    }

    #region Tween

    float tweenTime = 0.25f;
    public void TweenChosenPlayerTotarget(int playerIndex, Vector3 Pos, float delayTime = 0)
    {
        playerPrefabs[playerIndex].transform.DOMove(Pos, tweenTime).SetDelay(delayTime);
    }

    public void TweenEnemyToTarget(Action OnComplete)
    {
        enemyPrefabs.transform.DOMove(enemyOnActionPos.transform.position, tweenTime).OnComplete(()=> OnComplete?.Invoke());
    }
    public void TweenAllCharacterToDefault(float delayTime = 0)
    {
        for (int i = 0; i < 3; i++)
        {
            TweenChosenPlayerTotarget(i, playerPos[i].transform.position, delayTime);
        }

        enemyPrefabs.transform.DOMove(enemyPos.transform.position, tweenTime).SetDelay(delayTime);
    }

    public void TweenPlayerGetHit(int playerIndex,Action OnComplete = null)
    {
        Vector3 vectorToPunch = new Vector3(-50f,0,0);
        playerPrefabs[playerIndex].transform.DOPunchPosition(vectorToPunch, tweenTime).OnComplete(()=> 
        {
            playerPrefabs[playerIndex].transform.position = playerPos[playerIndex].transform.position;
            OnComplete?.Invoke();
        });
    }

    public void TweenEnemyGetHit(Action OnComplete = null)
    {
        Vector3 vectorToPunch = new Vector3(50f, 0, 0);
        enemyPrefabs.transform.DOPunchPosition(vectorToPunch, tweenTime).OnComplete(() =>
        {
            enemyPrefabs.transform.position = enemyPos.transform.position;
            OnComplete?.Invoke();
        });
    }
    #endregion


    public void UpdateCurrentHeaderIconFeedback(int playerIndex)
    {
        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            bool choseThisChar = i == playerIndex;
            if (choseThisChar)
            {
                playerPrefabs[i].CharacterGraphic.ActiveCurrentChosen(true);
            }

            else
            {
                playerPrefabs[i].CharacterGraphic.ActiveCurrentChosen(false);
            }
        }
    }

    public void HideAlltHeaderIconFeedback()
    {
        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            playerPrefabs[i].CharacterGraphic.ActiveCurrentChosen(false);
        }
    }

    #endregion


}
