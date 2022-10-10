using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI _value;
    [SerializeField]
    private VolumeSettings _settings;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private UIWidgetPanel _widgetPanel;

    private void Start()
    {
        InitWidget();
        _slider.value = _settings.Volume;
        ChangeValue(_settings.Volume);
        _slider.onValueChanged.AddListener(ChangeValue);
        if (_settings == null)
            _settings = FindObjectOfType<VolumeSettings>();
    }
    private void ChangeValue(float value)
    {
        _settings.ChangeVolume(value);
        var percent = Mathf.Round(value * 100);
        _value.text = percent.ToString() + "%";
    }

    private void InitWidget()
    {
        _button.onClick.AddListener(_widgetPanel.ShowWidget);
        _widgetPanel.OnPanelClickEvent += _widgetPanel.HideWidget;
    }
   
}
