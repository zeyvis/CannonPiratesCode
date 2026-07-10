using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BuyPanelController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private Button _confirmBuyButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private ButtonCancelAnimation _buttonCancelAnimation;

    [Header("Stats UI References")]
    [SerializeField] private ShipStatsUI _shipStatsUI;
    [SerializeField] private CannonStatsUI _cannonStatsUI;
    [SerializeField] private CoinManager _coinManager;

    private Action _onConfirmAction;
    private int _currentItemPrice; 

    private void Awake()
    {
        _confirmBuyButton.onClick.AddListener(OnConfirmClicked);

        if (_cancelButton != null)
            _cancelButton.onClick.AddListener(OnCancelClicked);
    }

    public void OpenPanel(ItemCategory category, int price, string description, Action onConfirm, ShipData previewShipData = null, CannonData previewCannonData = null, SailData previewSailData = null)
    {
        _currentItemPrice = price;

        _headerText.text = category.ToString().ToUpper();
        _priceText.text = price.ToString();
        _coinAmountText.text = _coinManager.GetCoinAmount().ToString();

        if (_descriptionText != null)
        {
            _descriptionText.text = description;
        }

        _onConfirmAction = onConfirm;

        ClearAllPreviews();

        if ((previewShipData != null || previewSailData != null) && _shipStatsUI != null)
        {
            _shipStatsUI.UpdatePreviewStats(previewShipData, previewSailData);
        }

        if (previewCannonData != null && _cannonStatsUI != null)
        {
            _cannonStatsUI.UpdatePreviewStats(previewCannonData);
        }

        _panel.SetActive(true);
    }

    private void OnConfirmClicked()
    {
        if (_coinManager.GetCoinAmount() >= _currentItemPrice)
        {

             _coinManager.SetCoinAmount(-(_currentItemPrice)); 

            _onConfirmAction?.Invoke();
            ClearAllPreviews();
            _panel.SetActive(false);
        }
        else
        {
            _buttonCancelAnimation.PlayCancelAnimation(_confirmBuyButton);
        }
    }

    private void OnCancelClicked()
    {
        ClearAllPreviews();
        _panel.SetActive(false);
    }

    private void ClearAllPreviews()
    {
        _shipStatsUI?.ClearPreview();
        _cannonStatsUI?.ClearPreview();
    }
}