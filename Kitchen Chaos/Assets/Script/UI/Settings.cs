using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    #region FloatVaribles
    [HideInInspector] public float BGMMixerVolume;
    [HideInInspector] public float SFXMixerVolume;
    #endregion

    #region OtherVariables
    //[SerializeField] private AudioSource BGMAudioSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMMixerVolumeSlider;
    [SerializeField] private Slider SFXMixerVolumeSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject pressToRebindKey;
    GameManager gm;
    GameInputManager gameInputManager;
    #endregion

    #region TextVariables
    [Header("Text References (For Key Rebinding)")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    #endregion

    #region ButtonVariables
    [Header("Button References (For Key Rebinding)")]
    [SerializeField] private Button moveUpKey;
    [SerializeField] private Button moveDownKey;
    [SerializeField] private Button moveLeftKey;
    [SerializeField] private Button moveRightKey;
    [SerializeField] private Button interactKey;
    [SerializeField] private Button interactAlternateKey;
    [SerializeField] private Button pauseKey;
    #endregion
    
    void Awake()
    {
        moveUpKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.MoveUp);
        });

        moveDownKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.MoveDown);
        });

        moveLeftKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.MoveLeft);
        });

        moveRightKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.MoveRight);
        });

        interactKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.Interact);
        });

        interactAlternateKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.InteractAlternate);
        });

        pauseKey.onClick.AddListener(() => {
            RebindBinding(GameInputManager.Binding.Pause);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gameInputManager = GameInputManager.Instance;

        if(SceneManager.GetActiveScene().name == "GameScene") gm.OnGameUnpaused += GameManager_OnGameUnpaused;
        
        BGMMixerVolume = PlayerPrefs.GetFloat("BGMMixerVolume",0);
        SFXMixerVolume = PlayerPrefs.GetFloat("SFXMixerVolume",0);

        audioMixer.SetFloat("BGM_Volume",BGMMixerVolume);
        audioMixer.SetFloat("SFX_Volume",SFXMixerVolume);

        BGMMixerVolumeSlider.value = BGMMixerVolume;
        SFXMixerVolumeSlider.value = SFXMixerVolume;

        closeButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
        
        UpdateKeyBindingVisual();
        HidePressToRebindKeyWindow();
        gameObject.SetActive(false);
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    public void ChangeBGMVolume(float value)
    {
        //BGMAudioSource.volume = value;
        audioMixer.SetFloat("BGM_Volume",value);
        PlayerPrefs.SetFloat("BGM_Volume",value);
    }

    public void ChangeSFXVolume(float value)
    {
        // SFXVolume = value;
        audioMixer.SetFloat("SFX_Volume",value);
        PlayerPrefs.SetFloat("SFX_Volume",value);
    }

    private void UpdateKeyBindingVisual()
    {
        moveUpText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveUp);
        moveDownText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveDown);
        moveLeftText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveLeft);
        moveRightText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveRight);
        interactText.text = gameInputManager.GetBindingText(GameInputManager.Binding.Interact);
        interactAlternateText.text = gameInputManager.GetBindingText(GameInputManager.Binding.InteractAlternate);
        pauseText.text = gameInputManager.GetBindingText(GameInputManager.Binding.Pause);
    }

    private void HidePressToRebindKeyWindow()
    {
        pressToRebindKey.SetActive(false);
    }

    private void RebindBinding(GameInputManager.Binding binding)
    {
        pressToRebindKey.SetActive(true);
        gameInputManager.RebindBinding(binding, () => {
            HidePressToRebindKeyWindow();
            UpdateKeyBindingVisual();
        });
    }
}
