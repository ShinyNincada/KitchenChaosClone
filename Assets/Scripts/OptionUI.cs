using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance {get; private set;}

    [Header("SFX")]
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button hideButton;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;
    [Header("KeyMap")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

     [Header("Texts")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    
    [Header("Other")]
    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake() {
        Instance = this;
        soundButton.onClick.AddListener(()=>{
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(()=>{
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        hideButton.onClick.AddListener(()=>{
            Hide();
            
        });

        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });

        interactAlternateButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.AlternativeInteract);
        });

        pauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual(){
        soundText.text =$"Sound Effect: { Mathf.Round(SoundManager.Instance.GetVolume() * 10f)}";
        musicText.text = $"Music: { Mathf.Round(MusicManager.Instance.GetVolume() * 10f)}";

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AlternativeInteract);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
       
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {   
        gameObject.SetActive(true);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.Rebinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

     private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
}
