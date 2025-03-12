using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    [SerializeField]
    PhoneCall callPrefab;
    Queue<int> callQueue = new Queue<int>();
    [SerializeField]
    RectTransform callParent;
    [SerializeField]
    List<AudioClip> audioClips;
    PhoneCall activeCall;
    // Start is called before the first frame update
    void Start()
    {
        
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
        //activeCall = Instantiate(callPrefab,callParent.transform);
        activeCall = Instantiate(callPrefab,callParent.transform);
        activeCall.voiceLine = audioClips[callQueue.Dequeue()];
        Handheld.Vibrate();
    }

    public void StopAllCalls(){
        callQueue.Clear();
        //activeCall.GetComponent<AudioSource>().Stop();
        Destroy(activeCall.gameObject);
    }
}
