using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocket_Phone_Client : MonoBehaviour
{
    WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        Debug.Log("connection status: " + ws.IsAlive);
        ws.OnMessage += (sender, e) =>{
            Debug.Log("Message Received: " + e.Data);
        };
    }

    public void SendWebSocketMessage(string message){
        ws.Send(message);
    }

    public void SendWebSocketRequest(string request){
        ws.Send("request:" + request);
    }

    public void SendWebSocketValue(string value){
        ws.Send("value:" + value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
