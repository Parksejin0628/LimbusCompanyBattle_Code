using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerCharacterStat : CharacterStat
{
    [SerializeField]
    public Sprite profileSprite;
    [SerializeField]
    public Skill[] skillPool = new Skill[6];
    [SerializeField]
    public Skill[][] handSkill = new Skill[2][];
    [SerializeField]
    public Skill[] nextSkill;
    [SerializeField]
    public Queue<Skill> waitingSkills = new Queue<Skill>();

};
public interface IPlayerCharacter : ICharacter
{
    
}
