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
        if (CheckConnection()){
            canvas.enabled = true;
            canvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ConnectWebsocket(){
            if(ConnectToUrl(inputField.text)){
                EnableEVM();
            }
    }

    bool ConnectToUrl(string URL){
        if (URL == "test"){
            EnableEVM();
            return false;
        }
        if (CheckConnection()){
            print("already Connected");
            return true;
        }
        else{
            if (URL == ""){
                URL = "localhost:8080";
            }
            WS_Phone_Client.ConnectWebSocket(URL);
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
        if (WS_Phone_Client.ws == null){
            return false;
        }
        else{
            return WS_Phone_Client.ws.IsAlive;
        }
    }

    void EnableEVM(){
        canvas.enabled = true;
        canvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
