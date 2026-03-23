using UnityEngine;
using UnityEngine.UI;

public class SkillIconUI : MonoBehaviour, IStatUI
{
    public ESkillTarget skillTarget;
    [SerializeField]
    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (iconImage == null || character == null) return;

        PlayerCharacterStat playerStat = character.stat as PlayerCharacterStat;
        if (playerStat == null) return;

        Skill targetSkill = null;
        switch (skillTarget)
        {
            case ESkillTarget.HAND_DOWN:
                targetSkill = playerStat.handSkill[0][slotNum];
                break;
            case ESkillTarget.HAND_UP:
                targetSkill = playerStat.handSkill[1][slotNum];
                break;
            case ESkillTarget.NEXT:
                targetSkill = playerStat.nextSkill[slotNum];
                break;
            case ESkillTarget.SLOT:
                targetSkill = playerStat.slots[slotNum].skill;
                break;
        }

        if (targetSkill != null && targetSkill.skillIcon != null)
        {
            iconImage.sprite = targetSkill.skillIcon;
            iconImage.color = new Color(1f, 1f, 1f, 1f); // 아이콘이 있으면 보이게 설정
        }
        else
        {
            Debug.Log($"{nameof(UpdateUI)}: Failed to Update UI");
        }
    }
}
