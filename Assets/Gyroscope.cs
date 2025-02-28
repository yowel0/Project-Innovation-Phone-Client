using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Mathematics;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    [SerializeField]
    Transform _transform;
    
    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion gyroRotation = GyroToUnity(Input.gyro.attitude);
        _transform.rotation = gyroRotation;
        //webSocket_Phone_Client.SendWebSocketRequest("messagefromgyro");
        webSocket_Phone_Client.SendWebSocketValue("gyroscope:" + gyroRotation.x + "," + gyroRotation.y + ","+ gyroRotation.z + ","+ gyroRotation.w);
        print(_transform.rotation);
    }

    private Quaternion GyroToUnity(Quaternion _q){
        return new Quaternion(_q.x, _q.z, _q.y, -_q.w);
    }
}
