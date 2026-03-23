using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedTextUI : MonoBehaviour, IStatUI
{
    [SerializeField]
    private TextMeshProUGUI speedText;

    private void Awake()
    {
        speedText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (speedText != null && character != null && character.stat != null)
        {
            speedText.text = character.stat.nowSpeed.ToString();
        }
        else
        {
            Debug.Log($"{nameof(UpdateUI)}: Failed to Update UI");
        }
    }
}
