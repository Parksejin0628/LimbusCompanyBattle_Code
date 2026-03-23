using UnityEngine;



public class EnemyTest1 : EnemyCharacter
{
    
    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void Start()
    {
        base.Start();
        SetupSkill();
    }

    private void SetupSkill()
    {
        Skill skill1 = new Skill();
        skill1.skillData.grade = 1;
        Skill skill2 = new Skill();
        skill2.skillData.grade = 2;
        Skill skill3 = new Skill();
        skill3.skillData.grade = 3;

        skills = new Skill[3];
        skills[0] = skill1;
        skills[1] = skill2;
        skills[2] = skill3;

        // slots 리스트가 초기화되어 있지 않다면 생성합니다.
        
        slotSkillCommands.Add(stat.slots[0], new DetermineRandomSkill(skills, stat.slots[0]));
        slotTargetCommands.Add(stat.slots[0], new DetermineRandomTarget(stat.slots[0]));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // if (stat.slots.Count > 0 && stat.slots[0].targetCharacter != null)
        // {
        //     Debug.Log($"{gameObject.name}의 슬롯 0 타겟: {stat.slots[0].targetCharacter.CharacterTransform.name} (슬롯 {stat.slots[0].targetSlotNum})");
        // }
    }
}
