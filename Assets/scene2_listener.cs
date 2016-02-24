using UnityEngine;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class scene2_listener : MonoBehaviour, ChatRequestListener, NotifyListener
	{
		string debug = "";
		
		private scene2_controller m_apppwarp;
		
		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
			//Debug.Log(msg);
		}

		public string getDebug()
		{
			return debug;
		}
		
		//Mono Behaviour
		
		void Start()
		{
			m_apppwarp = GetComponent<scene2_controller>();
		}
		
		public void onLog(String message){
			Log (message);
		}
		
		//ChatRequestListener
		public void onSendChatDone (byte result)
		{
			Log ("onSendChatDone result : " + result);
			
		}
		
		public void onSendPrivateChatDone(byte result)
		{
			Log ("onSendPrivateChatDone : " + result);
		}
		
		//NotifyListener
		public void onRoomCreated (RoomData eventObj)
		{
			Log ("onRoomCreated");
		}
		
		public void onRoomDestroyed (RoomData eventObj)
		{
			Log ("onRoomDestroyed");
		}
		
		public void onUserLeftRoom (RoomData eventObj, string username)
		{
			Log ("onUserLeftRoom : " + username);
		}
		
		public void onUserJoinedRoom (RoomData eventObj, string username)
		{
			Log ("onUserJoinedRoom : " + username);
		}
		
		public void onUserLeftLobby (LobbyData eventObj, string username)
		{
			Log ("onUserLeftLobby : " + username);
		}
		
		public void onUserJoinedLobby (LobbyData eventObj, string username)
		{
			Log ("onUserJoinedLobby : " + username);
		}
		
		public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
		{
			Log ("onUserChangeRoomProperty : " + sender);
		}
			
		public void onPrivateChatReceived(string sender, string message)
		{
			Log ("onPrivateChatReceived : " + sender);
		}
		
		public void onMoveCompleted(MoveEvent move)
		{
			Log ("onMoveCompleted by : " + move.getSender());
		}
		
		public void onChatReceived (ChatEvent eventObj)
		{
			Log(eventObj.getSender() + " sent Message : " + eventObj.getMessage());
			m_apppwarp.onMsg(eventObj.getSender(), eventObj.getMessage());
		}
		
		public void onUpdatePeersReceived (UpdateEvent eventObj)
		{
			Log ("onUpdatePeersReceived");
			//Log("isUDP " + eventObj.getIsUdp());
		}


		public void onUserPaused(String locid, Boolean isLobby, String username)
		{
		}

		public void onUserResumed(String locid, Boolean isLobby, String username)
		{
		}

		public void onGameStarted(string sender, string roomId, string nextTurn)
		{
		}

		public void onGameStopped(string sender, string roomId)
		{
		}

		public void onPrivateUpdateReceived(String sender, byte[] update, bool fromUdp)
		{

		}

		public void onNextTurnRequest(String lastTurn)
		{

		}

		public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<String, System.Object> properties)
        {
            Log("Notification for User Changed Room Property received");
            Log(roomData.getId());
            Log(sender);
            foreach (KeyValuePair<String, System.Object> entry in properties)
            {
                Log("KEY:" + entry.Key);
                Log("VALUE:" + entry.Value.ToString());
            }
        }
	
		public void sendMsg(string msg)
		{
			WarpClient.GetInstance().SendChat(msg);
		}
	}
}

