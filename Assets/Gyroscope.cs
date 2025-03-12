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
    RectTransform directionCircle;
    [SerializeField]
    float directionMultiplier = 10;
    
    Vector2 circleStartPos;

    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        circleStartPos = directionCircle.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion gyroRotation = GyroToUnity(Input.gyro.attitude);
        _transform.rotation = gyroRotation;
        Vector2 direction = new Vector2(_transform.up.x, _transform.up.z);
        directionCircle.position = circleStartPos + direction * directionMultiplier;
        //webSocket_Phone_Client.SendWebSocketRequest("messagefromgyro");
        webSocket_Phone_Client.SendWebSocketValue("gyroscope:" + gyroRotation.x + "," + gyroRotation.y + ","+ gyroRotation.z + ","+ gyroRotation.w);
        print(_transform.rotation);
    }

    private Quaternion GyroToUnity(Quaternion _q){
        return new Quaternion(_q.x, _q.z, _q.y, -_q.w);
    }
}
