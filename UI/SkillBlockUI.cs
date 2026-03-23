using System;
using UnityEngine;

public class SkillBlockUI : MonoBehaviour
{
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatUI(ICharacter character, int slotNum)
    {
        IStatUI[] statUIs = GetComponentsInChildren<IStatUI>();
        foreach (IStatUI statUI in statUIs)
        {
            statUI.UpdateUI(character, slotNum);
        }
    }
}
