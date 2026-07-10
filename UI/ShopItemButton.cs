using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItemButton : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private ItemCategory _itemCategory;
    [SerializeField] private int _itemPrice;
    [SerializeField] private int _itemIndex; 
    [SerializeField, TextArea(2, 4)] private string _itemDescription;

    [Header("Preview Data (Sadece ilgili olaný doldur)")]
    [SerializeField] private ShipData _previewShipData;
    [SerializeField] private CannonData _previewCannonData;
    [SerializeField] private SailData _previewSailData;

    [Header("Managers")]
    [SerializeField] private BuyPanelController _buyPanelController;
    [SerializeField] private ShopPanel _shopPanel; 

    private Button _unlockButton;
    private string _prefsKey; 

    private void Awake()
    {
        _unlockButton = GetComponent<Button>();
        _unlockButton.onClick.AddListener(OnItemClicked);
    }

    private void Start()
    {
        _prefsKey = $"Unlocked_{_itemCategory}_{_itemIndex}";


        if (_itemIndex == 0 || PlayerPrefs.GetInt(_prefsKey, 0) == 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnItemClicked()
    {
        _buyPanelController.OpenPanel(_itemCategory, _itemPrice, _itemDescription, ProcessPurchase, _previewShipData, _previewCannonData, _previewSailData);
    }

    private void ProcessPurchase()
    {
        PlayerPrefs.SetInt(_prefsKey, 1);
        PlayerPrefs.Save();

        this.gameObject.SetActive(false);
        Debug.Log($"{_itemCategory} kategorisindeki eþya {_itemPrice} altýna satýn alýndý ve KALICI olarak kaydedildi!");

        if (_shopPanel != null)
        {
            switch (_itemCategory)
            {
                case ItemCategory.Ship:
                    _shopPanel.SelectShip(_itemIndex);
                    break;
                case ItemCategory.Cannon:
                    _shopPanel.SelectCannon(_itemIndex);
                    break;
                case ItemCategory.Sail:
                    _shopPanel.SelectSail(_itemIndex);
                    break;
            }
        }
        else
        {
            Debug.LogError("ShopPanel referansý ShopItemButton'a atanmamýþ!", this);
        }
    }

    [ContextMenu("Reset This Item's Purchase")]
    public void ResetPurchase()
    {
        _prefsKey = $"Unlocked_{_itemCategory}_{_itemIndex}";
        PlayerPrefs.DeleteKey(_prefsKey);
        PlayerPrefs.Save();
        Debug.Log($"{_prefsKey} satýn alým kaydý silindi!");
    }
}