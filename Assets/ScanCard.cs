using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScanCard : MonoBehaviour
{
    public GameObject test;
    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;

    

    float acceleration;
    [SerializeField]
    float minAcceleration;
    [SerializeField]
    float maxAcceleration;

    [SerializeField]
    [Tooltip("per second")]
    float progressSpeed = 1;
    [Range(0f, 1f)]
    public float progress;

    [Header("UI")]
    [SerializeField]
    Slider progressBar;
    bool scanned = false;
    [SerializeField]
    TextMeshProUGUI statusText;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Vector3.Magnitude(Input.gyro.userAcceleration);
        print(acceleration);
        test.transform.position = Input.gyro.userAcceleration;
        if (progress <= 0){
            scanned = false;
        }
        if (acceleration < minAcceleration){
            print("Too Slow");
            progress -= progressSpeed * 2 * Time.deltaTime;
        }
        else if (acceleration > maxAcceleration){
            print("Too Fast");
            progress -= progressSpeed * 2 * Time.deltaTime;
        }
        else if (scanned){
            print("scanned");
            progress -= progressSpeed * 2 * Time.deltaTime;
        }
        else{
            print("In Range");
            if (!scanned){
                progress += progressSpeed * Time.deltaTime;
            }
            if (progress >= 1){
                Scan();
            }
        }
        progress = Mathf.Clamp(progress,0f,1f);
        SetUI();
    }

    void Scan(){
        scanned = true;
        webSocket_Phone_Client.SendWebSocketScanCard(0);
    }

    void SetUI(){
        progressBar.value = progress;
        if (scanned){
            statusText.text = "Scanned";
        }
        else if (acceleration < minAcceleration){
            statusText.text = "Too Slow";
        }
        else if (acceleration > maxAcceleration){
            statusText.text = "Too Fast";
        }
        else{
            statusText.text = "Scanning...";
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0,minAcceleration*10,0), new Vector3(1,minAcceleration*10,0));
        Gizmos.DrawLine(new Vector3(0,maxAcceleration*10,0), new Vector3(1,maxAcceleration*10,0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(0,acceleration*10,0), new Vector3(1,acceleration*10,0));
    }
}
