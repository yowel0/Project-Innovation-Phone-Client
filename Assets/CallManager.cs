using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    [SerializeField]
    PhoneCall callPrefab;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    List<AudioClip> audioClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCall(int _callID){
        PhoneCall call = Instantiate(callPrefab,canvas.transform);
        call.voiceLine = audioClips[_callID];
        //Handheld.Vibrate();
    }
}
