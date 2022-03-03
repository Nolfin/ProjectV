using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManagerComponent : MonoBehaviourPunCallbacks
{
    public static GameManagerComponent Instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public GameObject enemyPrefab;

    private IEnumerator coroutine;
    private float enemyCountMultiplier;

    void Start()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Going to instantiate");
        this.InstantiatePlayerPrefab();
        coroutine = SpawnEnemies();
        enemyCountMultiplier = 1.0f;
        StartCoroutine(coroutine);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion + " server");
    }


    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered room.");
    }

    private void InstantiatePlayerPrefab()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Missing Player prefab!");
            return;
        }

        Debug.Log("Instantiating LocalPlayer prefab");
        var player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0,0,0), Quaternion.identity, 0);
        player.name = PhotonNetwork.LocalPlayer.NickName;
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Debug.Log(1.0f / (float)Math.Pow(enemyCountMultiplier, 1.05f));
            yield return new WaitForSeconds(1.0f/(float)Math.Pow(enemyCountMultiplier,2));
            enemyCountMultiplier +=0.01f;
            PhotonNetwork.Instantiate(enemyPrefab.name, new Vector3(UnityEngine.Random.Range(-50.0f, 50.0f), UnityEngine.Random.Range(-50.0f, 50.0f), 0), Quaternion.identity, 0);
        }
    }
}
