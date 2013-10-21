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

public class scene2_controller : MonoBehaviour 
{
	//This is the listener that will be listening to the notifications/responses for Scene 2
	scene2_listener listen;
	
	void Start () {
		//Since we have already initialised WarpClient, we don't need to initialise it
		//or call the connect function. Simply add the required listeners
		//We only need chat and notification listeners for scene 2
		listen = GetComponent<scene2_listener>();
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
	}
	
	float interval = 1.0f;
	float timer = 0;
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
			//sending a dummy message
			listen.sendMsg("Scene 2 : " + System.DateTime.UtcNow.ToString());
			
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
		//You must remove the listener on destruction of scene
		WarpClient.GetInstance().RemoveNotificationListener(listen);
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
		//Disconnect from server on quit
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
