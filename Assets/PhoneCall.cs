using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    public AudioClip voiceLine;
    [SerializeField]
    GameObject incomingCall;
    [SerializeField]
    GameObject calling;

    [SerializeField]
    float pickUpTimer = 1;
    bool callAccepted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)){
        //     AcceptCall();
        // }
        if (pickUpTimer > 0){
            pickUpTimer -= Time.deltaTime;
        }
        else{
            if (!callAccepted){
                AcceptCall();
            }
        }
        if (callAccepted && !audioSource.isPlaying){
            //Call ended
            print("call end");
            CallManager callManager;
            callManager = FindAnyObjectByType<CallManager>();
            if (callManager.callQueue.Count <= 0){
                OpenHome();
            }
            callManager.activeCall = null;
            Destroy(gameObject);
        }
    }

    public void AcceptCall(){
        callAccepted = true;

        incomingCall.SetActive(false);
        calling.SetActive(true);

        audioSource.loop = false;
        audioSource.Stop();
        audioSource.clip = voiceLine;
        audioSource.Play();
    }

    void OpenHome()
    {
        MenuManager menuManager = FindAnyObjectByType<MenuManager>();
        menuManager.ActivateHome();
    }
}
