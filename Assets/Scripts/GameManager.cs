using System.Collections.Generic;
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
    string menuScene;
       
    int activePlayers;
    List<GameObject> playerEndTexts = new List<GameObject>();
    GameObject playerScores;


    private void Start()
    {
        activePlayers = JoinScreen.NumberOfJoinedPlayers;
        StartRoundWait = new WaitForSeconds(startDelay);
        EndRoundWait = new WaitForSeconds(endDelay);

        SpawnAllPlayers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].m_Instance =
                Instantiate(playerPrefab, players[i].m_SpawnPoint.position, players[i].m_SpawnPoint.rotation) as GameObject;
            players[i].m_PlayerNumber = i + 1;
            players[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            if (players[i].narwhalPlayer.isAlive)
            {
                targets[i] = players[i].m_Instance.transform;
            }
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        Debug.Log("Game Loop is occurring");
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
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
        roundWinner = null;
        roundWinner = GetRoundWinner();
        if(roundWinner != null)
        {
            roundWinner.m_Wins++;
        }
        gameWinner = GetGameWinner();

        string message = EndMessage();
        //if (gameWinner != null)
        //    playerScores.transform.Find("EndMessage").GetComponent<Text>().text = message;
        //else
        //    
messageText.text = message;

        yield return EndRoundWait;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].narwhalPlayer.isAlive)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 3;
    }


    private PlayerManager GetRoundWinner()
    {
        Debug.Log("Round Winner is Running!");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].narwhalPlayer.isAlive)
                return players[i];
        }

        return null;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].m_Wins == roundsToWin)
                return players[i];
        }

        return null;
    }


    private string EndMessage()
    {
        //roundWinner = GetRoundWinner();
        string message = "";

        if (roundWinner != null)
            message = roundWinner.m_ColoredPlayerText + " has lasered a Donut!";

        message += "\n\n\n\n";

        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].m_ColoredPlayerText + ": " + players[i].m_Wins + " WINS\n";
        }

        if (gameWinner != null)
            message = gameWinner.m_ColoredPlayerText + " hella lasered those Donuts!";

        return message;
    }


    private void ResetAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }


    private void EnablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}