using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialUI : MonoBehaviour
{
    #region TextVariables
    [Header("Key Text References")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    #endregion

    #region OtherVariables
    GameManager gm;
    GameInputManager gameInputManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gameInputManager = GameInputManager.Instance;

        gameInputManager.OnKeyRebind += GameInput_OnKeyRebind;
        gm.OnStateChanged += GameManager_OnStateChanged;
        UpdateKeyVisual();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(gm.IsCountdownStarted()) gameObject.SetActive(false);
    }

    private void GameInput_OnKeyRebind(object sender, EventArgs e)
    {
        UpdateKeyVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateKeyVisual()
    {
        moveUpText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveUp);
        moveDownText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveDown);
        moveLeftText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveLeft);
        moveRightText.text = gameInputManager.GetBindingText(GameInputManager.Binding.MoveRight);
        interactText.text = gameInputManager.GetBindingText(GameInputManager.Binding.Interact);
        interactAlternateText.text = gameInputManager.GetBindingText(GameInputManager.Binding.InteractAlternate);
        pauseText.text = gameInputManager.GetBindingText(GameInputManager.Binding.Pause);
    }
}
