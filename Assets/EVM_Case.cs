using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EVM_Case : MonoBehaviour
{
    public Button[] mainButtons;
    public Button[] numberButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonAddListener(Button button, UnityAction action){
        button.onClick.AddListener(action);
    }

    public void ButtonRemoveAllListeners(Button button){
        button.onClick.RemoveAllListeners();
    }

    public void AllButtonsRemoveAllListeners(){
        foreach (Button button in numberButtons){
            ButtonRemoveAllListeners(button);
        }
    }

    // Example
    // ButtonAddListener(numberButtons[0], () => {
    //     print("HI");
    // });
}
