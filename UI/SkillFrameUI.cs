using UnityEngine;
using UnityEngine.UI;

public class SkillFrameUI : MonoBehaviour, IStatUI
{
    public ESkillTarget skillTarget;
    
    [Header("Sin Colors")]
    static public Color wrathColor = Color.red;
    static public Color lustColor = new Color(1f, 0.5f, 0f); // 주황
    static public Color slothColor = Color.yellow;
    static public Color gluttonyColor = Color.green;
    static public Color gloomColor = Color.cyan;
    static public Color prideColor = Color.blue;
    static public Color envyColor = new Color(0.5f, 0f, 0.5f); // 보라
    static public Color noneColor = Color.white;

    [SerializeField]
    private Image frameImage;

    private void Awake()
    {
        frameImage = GetComponent<Image>();
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (frameImage == null || character == null) return;

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

        if (targetSkill != null)
        {
            ESinAttribute sin = targetSkill.skillData.sinAttribute;
            switch (sin)
            {
                case ESinAttribute.Wrath: frameImage.color = wrathColor; break;
                case ESinAttribute.Lust: frameImage.color = lustColor; break;
                case ESinAttribute.Sloth: frameImage.color = slothColor; break;
                case ESinAttribute.Gluttony: frameImage.color = gluttonyColor; break;
                case ESinAttribute.Gloom: frameImage.color = gloomColor; break;
                case ESinAttribute.Pride: frameImage.color = prideColor; break;
                case ESinAttribute.Envy: frameImage.color = envyColor; break;
                default: frameImage.color = noneColor; break;
            }
        }
    }
}
