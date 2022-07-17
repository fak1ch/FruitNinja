using TMPro;
using UnityEngine;

public class ComboDrawingUI : MonoBehaviour
{
    [SerializeField] private GameObject _comboPanel;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _indexText;

    private int _animIDShowComboTrigger;
    private int _animIDStopComboTrigger;

    private void Start()
    {
        _animIDShowComboTrigger = Animator.StringToHash("isShowCombo");
        _animIDStopComboTrigger = Animator.StringToHash("isStopShowingCombo");
    }

    public void ShowComboPanel(int cuttenFruitsCount)
    {
        _indexText.text = $"{cuttenFruitsCount}x";

        if (IsAnimationPlaying("Base Layer.FruitCombo") == true)
        {
            _animator.SetTrigger(_animIDStopComboTrigger);
            _animator.SetTrigger(_animIDShowComboTrigger);
        }
        else
        {
            _animator.SetTrigger(_animIDShowComboTrigger);
        }
    }

    public bool IsAnimationPlaying(string animationName)
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }
}
