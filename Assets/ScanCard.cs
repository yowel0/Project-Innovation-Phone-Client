using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScanCard : MonoBehaviour
{
    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;

    

    float acceleration;

    [Range(0f, 1f)]
    public float progress;
    [SerializeField]
    float progressGoal;

    [Header("UI")]
    [SerializeField]
    Slider progressBar;
    bool scanned = false;
    [SerializeField]
    Image statusImage;
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    float timer = 0;
    [SerializeField]
    float timerMax = 1;
    [SerializeField]
    float timerPadding = 0.3f;
    bool swiping = false;

    public bool cardAvailable = false;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void OnEnable()
    {
        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Vector3.Magnitude(Input.gyro.userAcceleration);

        if(cardAvailable){
            if (!swiping && acceleration > 0.2f && progress != progressGoal){
                StartSwiping();
            }
            if (swiping){
                SwipeStep();
            }
        }

        NewSetUI();
    }

    public void EnableCard(){
        cardAvailable = true;
        ResetValues();
    }

    void StartSwiping(){
        timer = 0;
        progress = 0;
        swiping = true;
    }
    void SwipeStep(){
        timer += Time.deltaTime;
        progress += acceleration;
        if (progress >= progressGoal){
            progress = progressGoal;
            SwipeEnd();
        }
    }
    void SwipeEnd(){
        progress = progressGoal;
        swiping = false;
        if (timer < timerMax - timerPadding){
            FailScan();
        }
        else if (timer > timerMax + timerPadding){
            FailScan();
        }
        else{
            Scan();
        }
        StartCoroutine(ResetValues(2));
    }

    IEnumerator ResetValues(int seconds){
        yield return new WaitForSeconds(seconds);
        ResetValues();
    }

    void ResetValues(){
        scanned = false;
        timer = 0;
        progress = 0;
    }

    void Scan(){
        scanned = true;
        webSocket_Phone_Client.SendWebSocketScanCard(0);
    }

    void FailScan(){
        webSocket_Phone_Client.SendWebSocketScanCard(-1);
    }

    void NewSetUI(){
        if (cardAvailable){
            if (progress != progressGoal){
                progressBar.gameObject.SetActive(true);
            }
            else {
                progressBar.gameObject.SetActive(false);
            }
            statusImage.sprite = sprites[1];
            progressBar.value = progress;
            progressBar.maxValue = progressGoal;
            if (scanned){
                    statusImage.sprite = sprites[4];
            }
            else if (progress == progressGoal){
                if (timer < timerMax - timerPadding){
                    statusImage.sprite = sprites[3];
                }
                else if (timer > timerMax + timerPadding ){
                    statusImage.sprite = sprites[2];
                }
            }
            else{
                statusImage.sprite = sprites[1];
            }
        }
        else{
            progressBar.gameObject.SetActive(false);
            statusImage.sprite = sprites[0];
            
        }
    }
}
