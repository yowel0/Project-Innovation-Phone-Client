using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;
    [SerializeField]
    string code = "";
    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInt(int _int){
        code += _int.ToString();
        SetUI();
    }

    public void ConfirmCode(){
        webSocket_Phone_Client.SendWebSocetCode(code);
        ResetCode();
    }

    public void ResetCode(){
        text.text = "####";
        code = "";
    }

    void SetUI(){
        text.text = code;
    }
}
