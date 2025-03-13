using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    [SerializeField]
    PhoneCall callPrefab;
    public Queue<int> callQueue = new Queue<int>();
    [SerializeField]
    GameObject callParent;
    [SerializeField]
    List<AudioClip> audioClips;
    public PhoneCall activeCall;
    MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindAnyObjectByType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCall == null && callQueue.Count > 0){
            StartNextCall();
        }
    }

    public void StartCall(int _callID){
        callQueue.Enqueue(_callID);
    }

    void StartNextCall(){
        menuManager.ActivateMenu(callParent);
        //activeCall = Instantiate(callPrefab,callParent.transform);
        activeCall = Instantiate(callPrefab,callParent.transform);
        activeCall.voiceLine = audioClips[callQueue.Dequeue()];
        Handheld.Vibrate();
    }

    public void StopAllCalls(){
        callQueue.Clear();
        //activeCall.GetComponent<AudioSource>().Stop();
        Destroy(activeCall.gameObject);
        activeCall = null;
    }
}
