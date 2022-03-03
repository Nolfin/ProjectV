using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManagerScript : MonoBehaviourPunCallbacks
{
    public static NetworkManagerScript instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Start()
    {
        if (PhotonNetwork.IsConnected) return;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
        CreateRoom("testRoom");
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room: "+PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Could not join room: {message} (return code: {returnCode})");
    }



    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);

        
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}!");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("SampleScene");
            return;
        }
        ChangeScene("SampleScene");
    }
}
