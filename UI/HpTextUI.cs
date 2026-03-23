using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpTextUI : MonoBehaviour, IStatUI
{
    [SerializeField]
    private TextMeshProUGUI hpText;

    public void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (hpText != null && character != null && character.stat != null)
        {
            hpText.text = ((int)character.stat.nowHp).ToString();
        }
        else
        {
            Debug.Log($"{nameof(UpdateUI)}: Failed to Update UI");
        }
        
    }
}
