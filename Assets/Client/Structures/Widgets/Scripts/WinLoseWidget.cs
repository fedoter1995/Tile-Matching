using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseWidget : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private TextMeshProUGUI _inscription;
    [SerializeField]
    private UIWidgetPanel _widget;
    [SerializeField]
    private Table _table;
    private void Awake()
    {
        _table.WinLoseEvent += ShowWindow;
        _button.onClick.AddListener(ReRunApplication);
    }
    private void ShowWindow(bool isWin)
    {
        if(isWin)
        {
            _widget.ShowWidget();
            _inscription.text = "Победа";
        }
        else
        {
            _widget.ShowWidget();
            _inscription.text = "Ошика";
        }
    }
    private void ReRunApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
