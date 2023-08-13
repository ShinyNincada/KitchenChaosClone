using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleImageUI : MonoBehaviour
{
    [SerializeField] Image _image;
    public void UpdateIconImage(Sprite newSprite){
        _image.sprite = newSprite;
    }
}
