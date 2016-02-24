using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

//using UnityEditor;

using AssemblyCSharp;

public class scene1_controller : MonoBehaviour 
{
	//Your API KEY
	public string apiKey = "cad2bfab6310acd9696187b98682925125e469ab0d0d585db0b00609f461b791";
	//Your SECRET Key
	public string secretKey = "55811709916e7ce4405cde0cdc5a254cf4b506fbafdae05760a73100b8080b67";
	//Room ID
	public string roomid = "1258637180";
	public static string username = "";
	
	//This is the listener that will be listening to the notifications/responses for Scene 1
	scene1_listener listen;
	
	void Start () {
		//Initialise the WarpClient with API KEY and SECRET KEY.
		WarpClient.initialize("cad2bfab6310acd9696187b98682925125e469ab0d0d585db0b00609f461b791","55811709916e7ce4405cde0cdc5a254cf4b506fbafdae05760a73100b8080b67");
		listen = GetComponent<scene1_listener>();
		//WarpClient is a singleton Class, always use GetInstance() to access the singeton object of WarpClient
		//Add the listeners to the WarpClient. We only need Connection, Room, Notification and Chat Listeners for this sample
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString();
		//Connect to the AppWarp Server 
		WarpClient.GetInstance().Connect(username);
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
	}
	
	float interval = 1.0f;
	float timer = 0;
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
			//Sending a dummy message
			listen.sendMsg("Scene 1 : " + System.DateTime.UtcNow.ToString());
			
			timer = interval;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
    	}
		WarpClient.GetInstance().Update();
	}
	
	void OnGUI()
	{
		//Print the Log on screen
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10,10,500,500), listen.getDebug());
	}
	
	void OnDestroy() 
	{
		//When you will switch to another scene, current scene will be destroyed.
		//In new scene you will be adding new listeners, making the listeners of this scene
		//useless, so you must remove the listeners from current scene on destruction
        WarpClient.GetInstance().RemoveConnectionRequestListener(listen);
		WarpClient.GetInstance().RemoveNotificationListener(listen);
		WarpClient.GetInstance().RemoveRoomRequestListener(listen);
		WarpClient.GetInstance().RemoveChatRequestListener(listen);
    }
	
	/*void OnEditorStateChanged()
	{
    	if(EditorApplication.isPlaying == false) 
		{
			WarpClient.GetInstance().Disconnect();
    	}
	}*/
	
	void OnApplicationQuit()
	{
		//Disconnect from the server when the game is closed
		Debug.Log("OnApplicationQuit ..... Called");
		WarpClient.GetInstance().Disconnect();
	}
	
	public void onMsg(string sender, string msg)
	{
		Debug.Log("onMsg ..... Called");
		/*
		if(sender != username)
		{
			
		}
		*/
	}
}
