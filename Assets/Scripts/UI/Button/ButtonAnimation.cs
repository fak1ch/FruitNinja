using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonAnimation : Button
{
    public ButtonClickedEvent onClickMy;

    public RectTransform RectTransform;
    public Image ButtonImage;
    public ButtonScriptableObject Settings;
    public Color PressedColor = new Color(193,193,193);
    public float PressedScaleProcent = 0.8f;
    public float ScaleDuration = 0.2f;
    public float ChangeColorDuration = 0.2f;

    private Color _normalColor;
    private Vector3 _startScale;
    private bool _isPointerExit = true;
    private bool _isOpen = true;

    protected override void Start() 
    {
        _startScale = RectTransform.localScale;
        _normalColor = ButtonImage.color;
        ApplySettingsFromScriptableObject();
    }

    private void ApplySettingsFromScriptableObject()
    {
        PressedColor = Settings.PressedColor;
        PressedScaleProcent = Settings.PressedScaleProcent;
        ScaleDuration = Settings.ScaleDuration;
        ChangeColorDuration = Settings.ChangeColorDuration;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_isOpen == true)
        {
            _isPointerExit = false;
            ButtonPressed();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_isOpen == true)
        {
            if (_isPointerExit == false)
                ButtonUnPressed();
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isOpen == true)
        {
            _isPointerExit = true;
            ButtonUnPressed();
        }
    }

    private void ButtonPressed()
    {
        RectTransform.DOScale(RectTransform.localScale * PressedScaleProcent, ScaleDuration);
        ButtonImage.DOColor(PressedColor, ScaleDuration);
    }

    private void ButtonUnPressed()
    {
        RectTransform.DOScale(_startScale, ScaleDuration);
        ButtonImage.DOColor(_normalColor, ScaleDuration);
        ButtonUnPressedComplete();
    }

    private void ButtonUnPressedComplete()
    {
        if (_isPointerExit == false)
        {
            _isOpen = false;
            onClickMy?.Invoke();
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
}
