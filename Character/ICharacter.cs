using Unity.Profiling;
using UnityEngine;
using System.Collections.Generic;
[System.Serializable]

public enum ESkillTarget
{
    HAND_DOWN = 0,
    HAND_UP,
    NEXT,
    NONE,
    SLOT
};
[System.Serializable]
public class Slot
{
    public Skill skill;
    public float aggro = 1f;
    public int slotNumber;
    public ICharacter targetCharacter;
    public int targetSlotNum;

    public ESkillTarget skillOriginContainer;
}

[System.Serializable]
public class CharacterStat
{
    public int minSpeed = 3;
    public int maxSpeed = 7;
    public int nowSpeed;
    public List<Slot> slots = new List<Slot>();
    public int slotCount = 0;

    public float maxHp = 100.0f;
    public float nowHp = 50.0f;

    public int maxSp = 45;
    public int minSp = -45;
    public int nowSp = 0;

    public int deploymentOrder = 0; 

};
public interface ICharacter
{
    public int DetermineSpeed();
    public CharacterStat stat { get; set; }
    public Transform CharacterTransform { get; }
}
