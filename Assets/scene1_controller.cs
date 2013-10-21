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
	public string apiKey = "b29f4030aba3b2bc7002c4eae6815a4130c862c386e43ae2a0a092b27de1c5af";
	public string secretKey = "bf45f27e826039754f8dda659166d59ffb7b9dce830ac51d6e6b576ae4b26f7e";
	public string roomid = "1440375425";
	public static string username = "";
	
	scene1_listener listen;
	
	void Start () {
		WarpClient.initialize(apiKey,secretKey);
		listen = GetComponent<scene1_listener>();
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString();
		WarpClient.GetInstance().Connect(username);
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
	}
	
	float interval = 1.0f;
	float timer = 0;
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
	
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
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10,10,500,500), listen.getDebug());
	}
	
	void OnDestroy() {
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
		WarpClient.GetInstance().Disconnect();
	}
	
	public void onMsg(string sender, string msg)
	{
		/*
		if(sender != username)
		{
			
		}
		*/
	}
}
