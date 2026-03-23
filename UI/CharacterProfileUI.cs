using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileUI : MonoBehaviour, IStatUI
{
    public Image profileImage;

    public void Start()
    {
        profileImage = GetComponent<Image>();
    }
    

    public void UpdateUI(ICharacter character, int slotNum)
    {
        if (character == null) return;
        
        // stat이 PlayerCharacterStat 타입인지 확인하고 캐스팅합니다.
        if (character.stat is PlayerCharacterStat playerStat)
        {
            if (profileImage != null)
            {
                profileImage.sprite = playerStat.profileSprite;
            }
            else
            {
                Debug.Log($"{nameof(UpdateUI)}: Failed to load sprite");
            }
        }
    }
}
