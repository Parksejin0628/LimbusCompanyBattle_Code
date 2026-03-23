using NUnit.Framework.Constraints;
using UnityEngine;

public interface ISkill
{
    
}
[System.Serializable]
public enum ESinAttribute
{
    None = 0,    // 속성 없음 (기본값)
    Wrath,       // 분노 (빨강)
    Lust,        // 색욕 (주황)
    Sloth,       // 나태 (노랑)
    Gluttony,    // 탐식 (초록)
    Gloom,       // 우울 (파랑)
    Pride,       // 오만 (남색)
    Envy         // 질투 (보라)
}
[System.Serializable]
public struct SkillData
{
    public int grade;
    public ESinAttribute sinAttribute;
}

[System.Serializable]
public class Skill : ISkill
{
    public SkillData skillData;
    [SerializeField]
    public Sprite skillIcon;
}
