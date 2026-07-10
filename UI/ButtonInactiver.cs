using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class CategoryButtonGroup
{
    public ItemCategory Category;
    public Button[] Buttons;
}

public class ButtonInactiver : MonoBehaviour
{
    [SerializeField] private List<CategoryButtonGroup> _categoryGroups;

    public void InactiveSelectButton(ItemCategory category, int selectedIndex)
    {
        CategoryButtonGroup targetGroup = null;
        foreach (var group in _categoryGroups)
        {
            if (group.Category == category)
            {
                targetGroup = group;
                break;
            }
        }

        if (targetGroup == null)
        {
            Debug.LogError(category + " kategorisi için buton grubu bulunamadý!");
            return;
        }

        foreach (Button btn in targetGroup.Buttons)
        {
            btn.interactable = true;
        }

        if (selectedIndex >= 0 && selectedIndex < targetGroup.Buttons.Length)
        {
            targetGroup.Buttons[selectedIndex].interactable = false;
        }
        else
        {
            Debug.LogWarning("Geçersiz buton index'i: " + selectedIndex);
        }
    }
}