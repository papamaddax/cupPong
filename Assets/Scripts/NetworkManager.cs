using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private GameObject joinRoomButton;
    private GameObject createRoomButton;
    private GameObject lobbyUI;
    private GameObject Player;
    public UnityEvent connectedToServer;
    public UnityEvent ballPickedUp;
    public bool isPickedUp = false;
    // Start is called before the first frame update
    void Awake()
    {
        joinRoomButton = GameObject.FindWithTag("joinRoomButton");
        createRoomButton = GameObject.FindWithTag("createRoomButton");
        lobbyUI = GameObject.FindWithTag("lobbyUI");
        Player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        joinRoomButton.SetActive(false);
        createRoomButton.SetActive(false);
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to server");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        joinRoomButton.SetActive(true);
        createRoomButton.SetActive(true);


        //  PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
        base.OnConnectedToMaster();

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        base.OnJoinedRoom();
       lobbyUI.SetActive(false);
        connectedToServer.Invoke();
        if(PhotonNetwork.PlayerList.Length > 1){
        Player.transform.position = new Vector3(0.085f, 0f, -1.8f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

   
}
