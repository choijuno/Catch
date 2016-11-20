using UnityEngine;
using System.Collections;



public class NetworkManager : MonoBehaviour {

	//player
	public GameObject playerPrefab;

	public GameObject spawnPoint_host;
	public GameObject spawnPoint_guest;


	//server create
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";

	//server load
	private HostData[] hostList;

	void Start () {
	
	}

	void Update () {
	
	}


	//Server Create
	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}


	//_player host
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		SpawnPlayer (spawnPoint_host);
	}



	//Server Load
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	//Server Load 2
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	//_player join
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer (spawnPoint_guest);
	}



	//player


	void SpawnPlayer(GameObject spawnpoint){
		Network.Instantiate (playerPrefab, new Vector3 (spawnpoint.transform.position.x,spawnpoint.transform.position.y,spawnpoint.transform.position.z), Quaternion.identity, 0);
	}





	//GUI Button
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();

			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}

		}

	}

}
