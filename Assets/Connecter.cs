using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Connecter : MonoBehaviour
{
    [SerializeField]
    WebSocket_Phone_Client WS_Phone_Client;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConnectWebsocket(){
            if(ConnectToUrl(inputField.text)){
                canvas.enabled = true;
                canvas.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
    }

    bool ConnectToUrl(string URL){
        if (CheckConnection()){
            print("already Connected");
            return true;
        }
        else{
            if (URL == ""){
                URL = "localhost:8080";
            }
            WS_Phone_Client.ws = new WebSocket("ws://" + URL);
            WS_Phone_Client.ws.Connect();
            print("conncention: "+ CheckConnection());
            if (CheckConnection()){
                return true;
            }
            else {
                return false;
            }
        }
    }

    bool CheckConnection(){
        return WS_Phone_Client.ws.IsAlive;
    }
}
