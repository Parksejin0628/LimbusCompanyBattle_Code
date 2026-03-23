using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpTextUI : MonoBehaviour, IStatUI
{
    [SerializeField]
    private TextMeshProUGUI spText;

    private void Awake()
    {
        spText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (spText != null && character != null && character.stat != null)
        {
            spText.text = character.stat.nowSp.ToString();
        }
        else
        {
            Debug.Log($"{nameof(UpdateUI)}: Failed to Update UI");
        }
    }
}
