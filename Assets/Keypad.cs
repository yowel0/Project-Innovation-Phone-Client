using System.Linq;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    WebSocket_Phone_Client webSocket_Phone_Client;
    [SerializeField]
    string code = "";
    [SerializeField]
    TextMeshProUGUI text;
    
    EVM_Case evm;
    void OnEnable()
    {
        evm = FindAnyObjectByType<EVM_Case>();
        for (int i = 0; i < evm.numberButtons.Count(); i++){
            string iString = (i + 1).ToString();
            int iInt = int.Parse(iString);
            evm.ButtonAddListener(evm.numberButtons[i],delegate{AddInt(iInt);});
        }
        ResetCode();

        evm.ButtonAddListener(evm.mainButtons[1],ResetCode);
        evm.ButtonAddListener(evm.mainButtons[2],ConfirmCode);
    }

    void OnDisable()
    {
        evm.AllButtonsRemoveAllListeners();
        evm.ButtonRemoveAllListeners(evm.mainButtons[1]);
        evm.ButtonRemoveAllListeners(evm.mainButtons[2]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInt(int _int){
        if (code.Length >= 4){
            code = code.Remove(0,1);
        }
        code += _int.ToString();
        print(code);
        SetUI();
    }

    public void ConfirmCode(){
        if (code.Length >= 4){
            webSocket_Phone_Client.SendWebSocetCode(code);
            ResetCode();
        }
    }

    public void ResetCode(){
        code = "";
        SetUI();
    }

    void SetUI(){
        text.text = code;
    }
}
