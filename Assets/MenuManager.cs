using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("First menu will be active first")]
    List<GameObject> menus;
    // Start is called before the first frame update
    void Start()
    {
        DeactivateMenus();
        menus[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMenu(GameObject _menu){
        DeactivateMenus();
        if (menus.Contains(_menu)){
            _menu.SetActive(true);
        }
        else{
            Debug.LogWarning("Menu is not assigned to the menus list");
        }
    }

    void DeactivateMenus(){
        foreach (var menu in menus){
            menu.SetActive(false);
        }
    }
}
