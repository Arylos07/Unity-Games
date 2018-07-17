using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
public class MultiplayerManager : Photon.MonoBehaviour {


    [Header("Enable Objects On Join")]
    public GameObject[] EOOnJoin;
    [Header("Disable Objects On Join")]
    public GameObject[] DOOnJoin;
    [Header("Enable Objects Master Server Connected")]
    public GameObject[] EOMasterConnect;
    public GameObject[] Survivors;
    public GameObject[] SpawnPoints;
    public MPOptions mp_Options;
    
    //UI
    public Text ConnectionStatus;
	// Use this for initialization
	void Start () {
        PhotonNetwork.offlineMode = mp_Options.OfflineMode;
        //Set what region this client is using
        
        //Connect to the cloud using the current game version
        //PhotonNetwork.ConnectUsingSettings(mp_Options.GameVersion);
        PhotonNetwork.ConnectToRegion(mp_Options.ServerRegion, mp_Options.GameVersion);
    }
	
	// Update is called once per frame
	void Update () {
        ConnectionStatus.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString();

    }
    //Join a random room
    public void JoinRandomRoom()
    {
        Debug.Log("JoinRandomRoom()");
        PhotonNetwork.JoinRandomRoom();
    }
    
    public virtual void OnConnectedToMaster()
    {
        foreach(GameObject g in EOMasterConnect)
        {
            g.SetActive(true);
        }
    }
    //If random room join has failed this function will be called and create a new room for the user
    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonJoinRoomFailed()");
        int rID = Random.Range(0, 9999);
        RoomOptions ro = new RoomOptions
        {
            MaxPlayers = mp_Options.PlayersPerRoom,
            IsVisible = mp_Options.AreRoomsPublic
        };
        PhotonNetwork.CreateRoom("Room - " + rID, ro, null);
    }

    public virtual void OnJoinedRoom()
    {
        foreach (GameObject g in EOOnJoin)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in DOOnJoin)
        {
            g.SetActive(false);
        }
        GameObject player = PhotonNetwork.Instantiate(Survivors[Random.Range(0, Survivors.Length)].name, SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity,0);

    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //This is the local instance of the player
        if(stream.isWriting)
        {

        }
        //This is the remote instance of a player
        else if(stream.isReading)
        {

        }
    }
}

[System.Serializable]
public class MPOptions
{
    public string GameVersion = "Default - v1";
    public byte PlayersPerRoom = 4;
    public bool AreRoomsPublic = false;
    public CloudRegionCode ServerRegion;
    public bool OfflineMode = false;
}
