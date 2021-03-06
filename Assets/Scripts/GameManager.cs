﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script adapted from TANKS tutorial
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int roundsToWin = 3;
    public CameraControl m_CameraControl;
    public float startDelay = 3f;
    public float endDelay = 3f;
    public GameObject playerPrefab;
    public PlayerManager[] players;
    public Text messageText;

    private int roundNumber;
    private WaitForSeconds StartRoundWait;
    private WaitForSeconds EndRoundWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    [SerializeField]
    GameObject playerEndTextPrefab;
    [SerializeField]
    GameObject endPanel;
    [SerializeField]
    string menuScene;
       
    int activePlayers;
    List<GameObject> playerEndTexts = new List<GameObject>();
    GameObject playerScores;


    private void Start()
    {
        activePlayers = JoinScreen.NumberOfJoinedPlayers;
        StartRoundWait = new WaitForSeconds(startDelay);
        EndRoundWait = new WaitForSeconds(endDelay);
        playerScores = endPanel.transform.Find("PlayerScores").gameObject;

        SpawnAllPlayers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllPlayers()
    {
        if (activePlayers < 2)
            SceneManager.LoadScene(menuScene);
        for (int i = 0; i < activePlayers; i++)
        {
            players[i].m_Instance =
                Instantiate(playerPrefab, players[i].m_SpawnPoint.position, players[i].m_SpawnPoint.rotation) as GameObject;
            players[i].m_PlayerNumber = i + 1;
            players[i].Setup();
            playerEndTexts.Add(Instantiate(playerEndTextPrefab, playerScores.transform));
            playerEndTexts[i].transform.Find("Player").GetComponent<Text>().text = players[i].m_ColoredPlayerText;
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[activePlayers];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = players[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
            //endPanel.SetActive(true);
            //for(int i = 0; i < activePlayers; i++)
            //{
            //    playerEndTexts[i].transform.Find("Points").GetComponent<Text>().text = players[i].m_Wins.ToString();
            //}
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllPlayers();
        DisablePlayerControl();

        m_CameraControl.SetStartPositionAndSize();

        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        yield return StartRoundWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();
        
        messageText.text = string.Empty;

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

        gameWinner = GetGameWinner();

        string message = EndMessage();
        if (gameWinner != null)
            playerScores.transform.Find("EndMessage").GetComponent<Text>().text = message;
        else
            messageText.text = message;

        yield return EndRoundWait;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < activePlayers; i++)
        {
            if (players[i].m_Instance.activeSelf)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 1;
    }


    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            if (players[i].m_Instance.activeSelf)
                return players[i];
        }

        return null;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            if (players[i].m_Wins == roundsToWin)
                return players[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "Donuts are unscathed!";

        if (roundWinner != null)
            message = roundWinner.m_ColoredPlayerText + " has lasered a Donut!";

        message += "\n\n\n\n";

        for (int i = 0; i < activePlayers; i++)
        {
            message += players[i].m_ColoredPlayerText + ": " + players[i].m_Wins + " WINS\n";
        }

        if (gameWinner != null)
            message = gameWinner.m_ColoredPlayerText + " hella lasered those Donuts!";

        return message;
    }


    private void ResetAllPlayers()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            players[i].Reset();
        }
    }


    private void EnablePlayerControl()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            players[i].DisableControl();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToTitleButton()
    {
        SceneManager.LoadScene(0);
    }
}