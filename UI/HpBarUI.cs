using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour, IStatUI
{
    [SerializeField] private Image hpBarImage;
    [SerializeField] private float minAmount = 0f;
    [SerializeField] private float maxAmount = 1f;

    public void Start()
    {
        hpBarImage = GetComponent<Image>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (hpBarImage != null && character != null && character.stat != null && character.stat.maxHp > 0)
        {
            float hpRatio = (float)character.stat.nowHp / character.stat.maxHp;
            hpBarImage.fillAmount = Mathf.Lerp(minAmount, maxAmount, hpRatio);
        }
        else
        {
            Debug.Log($"{nameof(UpdateUI)}: Failed to Update UI");
        }
        
    }
}
