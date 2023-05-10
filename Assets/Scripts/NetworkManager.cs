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
    private GameObject leaveRoomButton;
    private GameObject lobbyUI;
    private GameObject Player;
    private GameObject ReadyUpButton;
    private GameObject ballsBackUI;
    public UnityEvent connectedToServer;
    public UnityEvent ballPickedUp;
    public bool isPickedUp = false;
    // Start is called before the first frame update
    void Awake()
    {
        ballsBackUI = GameObject.FindWithTag("ballsBackUI");
        joinRoomButton = GameObject.FindWithTag("joinRoomButton");
        createRoomButton = GameObject.FindWithTag("createRoomButton");
        leaveRoomButton = GameObject.FindWithTag("leaveRoomButton");
        lobbyUI = GameObject.FindWithTag("lobbyUI");
        Player = GameObject.FindWithTag("Player");
        ReadyUpButton = GameObject.FindWithTag("ReadyUp");
    }
    void Start()
    {
        joinRoomButton.SetActive(false);
        createRoomButton.SetActive(false);
        leaveRoomButton.SetActive(false);
        ReadyUpButton.SetActive(false);
        
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
        leaveRoomButton.SetActive(true);
        connectedToServer.Invoke();
        ballsBackUI.SetActive(true);
        if (PhotonNetwork.CurrentRoom.PlayerCount> 1)
        {
            ReadyUpButton.SetActive(true);
            Player.transform.position = new Vector3(0.085f, 0.35f, -1.8f);
            Player.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(GameObject.Find("redSide"));
                            ballsBackUI.SetActive(false);

        } else { 
                        Destroy(GameObject.Find("blueSide"));
                                        ballsBackUI.SetActive(false);
                        Player.transform.rotation = Quaternion.Euler(0, 180, 0);

        }


    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ReadyUpButton.SetActive(true);
        Debug.Log("Player entered room");
        base.OnPlayerEnteredRoom(newPlayer);
    }


}
