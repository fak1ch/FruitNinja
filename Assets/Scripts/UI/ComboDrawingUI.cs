using UnityEngine;

public class ComboDrawingUI : MonoBehaviour
{
    [SerializeField] private GameObject _comboPanel;

    public void ShowComboPanel(int cuttenFruitsCount)
    {
        Debug.Log("Combo " + cuttenFruitsCount);
    }
}
