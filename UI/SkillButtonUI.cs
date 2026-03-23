using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour, IStatUI
{
    public ESkillTarget skillTarget;

    private ICharacter _character;
    private int _slotNum;
    [SerializeField]
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(OnClickButton);
        }
    }

    public void UpdateUI(ICharacter character, int slotNum)
    {
        _character = character;
        _slotNum = slotNum;
    }

    private void OnClickButton()
    {
        if (_character != null && GameManager.instance != null)
        {
            GameManager.instance.DeterminePlayerCharacterSkill(_character, skillTarget, _slotNum);
            if (UIManager.instance != null)
            {
                UIManager.instance.UpdateSkillIconOnCharacter();
            }
        }
    }
}
