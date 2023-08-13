using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameobject;
    [SerializeField] private Image barImage;
     private IHasProgress hasProgress;
    private void Start() {
        hasProgress = hasProgressGameobject.GetComponent<IHasProgress>();
        if(hasProgress == null) {
            throw new NotImplementedException();
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f){
            Hide();
        }
        else{
            Show();
        }
    }

    void Hide(){
        gameObject.SetActive(false);
    }

    void Show(){
        gameObject.SetActive(true);
    }
}
