using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingCall : MonoBehaviour
{
    PhoneCall phoneCall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        phoneCall = GetComponentInParent<PhoneCall>();
        EVM_Case evm_case = FindAnyObjectByType<EVM_Case>();
        evm_case.ButtonAddListener(evm_case.mainButtons[1], phoneCall.AcceptCall);
    }

    void OnDisable()
    {
        EVM_Case evm_case = FindAnyObjectByType<EVM_Case>();
        evm_case.ButtonRemoveAllListeners(evm_case.mainButtons[1]);
    }
}
