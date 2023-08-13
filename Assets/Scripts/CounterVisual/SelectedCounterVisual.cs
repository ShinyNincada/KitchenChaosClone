using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] counterVisual;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectCounterChanged += Instance_OnSelectCounterChanged;
    }

    private void Instance_OnSelectCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counter){
            Show();
        }
        else{
            Hide();
        }
    }

    void Show(){
        foreach (GameObject item in counterVisual) {
            item.SetActive(true);
        }
    }

    void Hide(){
        foreach (GameObject item in counterVisual) {
            item.SetActive(false);
        }
    }
 
}
