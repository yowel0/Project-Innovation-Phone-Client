using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using WebSocketSharp;
using System.Diagnostics;

public class WebSocket_Phone_Client : MonoBehaviour
{
    public WebSocket ws;
    [SerializeField]
    CallManager callManager;
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ConnectWebSocket(string URL){
        ws = new WebSocket("ws://"+URL);
        ws.Connect();
        print("connection status: " + ws.IsAlive);
        ws.OnMessage += (sender, e) =>
        {
            print("Message Received: " + e.Data);
            if (e.Data.StartsWith("command:")){
                _actions.Enqueue(() => ProcessCommand(e.Data));
            }
            else if (e.Data.StartsWith("phonecall:")){
                _actions.Enqueue(() => ProcessCall(e.Data));
            }
        };
    }

    void ProcessCommand(string _command){
        string command = _command.Replace("command:","");
        switch (command){
            case "SetCardAvailable":
                MenuManager menuManager;
                menuManager = FindAnyObjectByType<MenuManager>();
                menuManager.ActivateMenu(menuManager.menus[2]);
                ScanCard scanCard = FindAnyObjectByType<ScanCard>();
                scanCard.EnableCard();
            break;
            case "StopCall":
                callManager.StopAllCalls();
            break;
        }
    }

    void ProcessCall(string _call){
        string call = _call.Replace("phonecall:","");
        int callID = int.Parse(call);
        callManager.StartCall(callID);
    }

    public void SendWebSocketMessage(string message){
        ws.Send(message);
    }

    public void SendWebSocketCommand(string command){
        ws.Send("command:" + command);
    }

    public void SendWebSocketValue(string value){
        ws.Send("value:" + value);
    }

    public void SendWebSocketPhoneCall(int callID){
        ws.Send("phonecall:" + callID);
    }

    public void SendWebSocketScanCard(int cardID){
        ws.Send("card:" + cardID);
    }

    public void SendWebSocetCode(string code){
        ws.Send("code:" + code);
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null){
            print("ws is null");
            return;
        }

        while (_actions.Count > 0){
            if(_actions.TryDequeue(out var action)){
                action?.Invoke();
            }
        }
    }
}
