using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    //#region Serialized fields
    //[SerializeField]
    //private Transform player1SpawnPoint, player2SpawnPoint,
    //    player3SpawnPoint, player4SpawnPoint;

    //[SerializeField]
    //GameObject playerCharacterPrefab;
    //#endregion

    //#region cosntants
    //public const int MaxPlayers = 4;
    //#endregion

    //#region static fields and properties
    //public static int NumberOfJoinedPlayers
    //{
    //    get
    //    {
    //        return allPlayers.Where(player => player.IsJoined).Count();
    //    }
    //}

    //private static List<Player> allPlayers;
    //#endregion

    //#region private fields
    //private string joinButtonName = "Fire";
    //#endregion

    //#region Monobehaviour functions
    //private void Start()
    //{
    //    InitializePlayerList();
    //}

    //private void Update()
    //{
    //    CheckForJoiningPlayers();
    //}

    //private void CheckForJoiningPlayers()
    //{
    //    var unjoinedPlayers = allPlayers.Where(player => !player.IsJoined);

    //    foreach (var player in unjoinedPlayers)
    //    {
    //        Debug.Log("Check Player " + player.PlayerNumber.ToString());
    //        if (Input.GetButtonDown(joinButtonName + player.PlayerNumber.ToString()))
    //        {
    //            Debug.Log("Join player " + player.PlayerNumber);
    //            player.IsJoined = true;
    //            SpawnPlayerControlledCharacter(player);
    //        }
    //    }
    //}
    //#endregion

    //private void SpawnPlayerControlledCharacter(Player controllingPlayer)
    //{
    //    GameObject playerCharacterGameObject = Instantiate(playerCharacterPrefab);
    //    var playerCharacter = playerCharacterGameObject.GetComponent<NarwhalMoveAndTurn>();
    //    playerCharacter.PlayerNumber = controllingPlayer;

    //    switch (controllingPlayer.PlayerNumber)
    //    {
    //        case 1:
    //            playerCharacterGameObject.transform.position = player1SpawnPoint.position;
    //            break;

    //        case 2:
    //            playerCharacterGameObject.transform.position = player2SpawnPoint.position;
    //            break;

    //        case 3:
    //            playerCharacterGameObject.transform.position = player3SpawnPoint.position;
    //            break;

    //        case 4:
    //            playerCharacterGameObject.transform.position = player4SpawnPoint.position;
    //            break;
    //        default:
    //            throw new System.Exception("Invalid player number.");
    //    }
    //}

    //private void InitializePlayerList()
    //{
    //    allPlayers = new List<Player>();

    //    for (int i = 1; i <= MaxPlayers; i++)
    //    {
    //        var player = new Player(i);
    //        allPlayers.Add(player);
    //    }
    //}
}
