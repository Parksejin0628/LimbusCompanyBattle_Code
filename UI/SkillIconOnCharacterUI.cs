using System;
using UnityEngine;

public class SkillIconOnCharacterUI : MonoBehaviour
{
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetIcon()
    {
        IStatUI[] statUIs = GetComponentsInChildren<IStatUI>(true);
        foreach (IStatUI statUI in statUIs)
        {
            if (statUI is MonoBehaviour mb)
            {
                mb.enabled = false;
            }
        }
    }

    public void UpdateStatUI(ICharacter character, int slotNum)
    {
        IStatUI[] statUIs = GetComponentsInChildren<IStatUI>(true);
        
        // 1. 모든 자식들을 우선 활성화
        foreach (IStatUI statUI in statUIs)
        {
            if (statUI is MonoBehaviour mb)
            {
                mb.enabled = true;
            }
        }

        // 2. 스킬이 없는 경우 ResetIcon 호출 후 중단
        if (character == null || character.stat == null || character.stat.slots.Count <= slotNum || character.stat.slots[slotNum].skill == null)
        {
            ResetIcon();
            return;
        }

        foreach (IStatUI statUI in statUIs)
        {
            statUI.UpdateUI(character, slotNum);
        }
    }
}
