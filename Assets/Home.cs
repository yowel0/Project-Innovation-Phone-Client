using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    EVM_Case evm;
    MenuManager menuManager;
    void OnEnable()
    {
        evm = FindAnyObjectByType<EVM_Case>();
        menuManager = FindAnyObjectByType<MenuManager>();
        CallManager callManager = FindAnyObjectByType<CallManager>();
        evm.ButtonAddListener(evm.numberButtons[0],() =>{
            menuManager.ActivateMenu(menuManager.menus[2]);
        });
        evm.ButtonAddListener(evm.numberButtons[1],() =>{
            menuManager.ActivateMenu(menuManager.menus[3]);
        });
        evm.ButtonAddListener(evm.numberButtons[2],() =>{
            menuManager.ActivateMenu(menuManager.menus[1]);
        });
        evm.ButtonAddListener(evm.numberButtons[3],() =>{
            callManager.StartCall(0);
        });
    }

    void OnDisable()
    {
        if (evm != null){
            evm.AllButtonsRemoveAllListeners();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
