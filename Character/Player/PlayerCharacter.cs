using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerCharacter : Character, IPlayerCharacter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    protected PlayerCharacterStat _stat = new PlayerCharacterStat();
    public override CharacterStat stat
    {
        get => _stat;
        set => _stat = value as PlayerCharacterStat;
    }

    protected override void Awake()
    {
        base.Awake();
        
        if(stat.slots.Count == 0)
        {
            stat.slots.Add(new Slot());
            stat.slots[stat.slotCount].slotNumber =  stat.slotCount;
            stat.slotCount++;
        }
    }
    protected override void Start()
    {
        base.Start();
        if (GameManager.instance != null)
        {
            GameManager.instance.onTurnStartDelegate += DistributeSkill;
        }
        InitSkillSystem();
    }

    private void InitSkillSystem()
    {
        _stat.handSkill[0] = new Skill[stat.slotCount];
        _stat.handSkill[1] = new Skill[stat.slotCount];
        _stat.nextSkill = new Skill[stat.slotCount];

        _stat.skillPool[0] = skills[0];
        _stat.skillPool[1] = skills[0];
        _stat.skillPool[2] = skills[0];
        _stat.skillPool[3] = skills[1];
        _stat.skillPool[4] = skills[1];
        _stat.skillPool[5] = skills[2];
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public void DistributeSkill()
    {

        // 1. handSkill[0][i]가 null인 경우 handSkill[1][i]를 가져옴
        for (int i = 0; i < stat.slotCount; i++)
        {
            _stat.handSkill[0][i] = null;
            if (_stat.handSkill[0][i] == null)
            {
                _stat.handSkill[0][i] = _stat.handSkill[1][i];
                _stat.handSkill[1][i] = null;
            }
        }

        // 2. handSkill[1][i]가 null인 경우 nextSkill[i]를 가져옴
        for (int i = 0; i < stat.slotCount; i++)
        {
            if (_stat.handSkill[1][i] == null)
            {
                _stat.handSkill[1][i] = _stat.nextSkill[i];
                _stat.nextSkill[i] = null;
            }
        }

        // 3. 모든 배열을 순회하며 null인 경우 waitingSkills에서 가져옴
        for (int i = 0; i < stat.slotCount; i++)
        {
            if (_stat.handSkill[0][i] == null) _stat.handSkill[0][i] = PopWaitingSkill();
            if (_stat.handSkill[1][i] == null) _stat.handSkill[1][i] = PopWaitingSkill();
            if (_stat.nextSkill[i] == null) _stat.nextSkill[i] = PopWaitingSkill();
        }

        //PrintMySkills();
    }

    protected Skill PopWaitingSkill()
    {
        // 4. waitingSkills가 비게 된 경우 SkillPool 복제 및 셔플
        if (_stat.waitingSkills.Count == 0)
        {
            RefillWaitingSkills();
        }

        return _stat.waitingSkills.Dequeue();
    }

    protected void RefillWaitingSkills()
    {
        List<Skill> tempPool = new List<Skill>(_stat.skillPool);

        // Fisher-Yates Shuffle 알고리즘으로 스킬 섞기
        for (int i = tempPool.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Skill temp = tempPool[i];
            tempPool[i] = tempPool[randomIndex];
            tempPool[randomIndex] = temp;
        }

        // 섞인 스킬들을 대기열(Queue)에 등록
        foreach (Skill skill in tempPool)
        {
            _stat.waitingSkills.Enqueue(skill);
        }
    }

    private void PrintMySkills()
    {
        string logMsg = $"[{gameObject.name}] 현재 스킬 상태:\n";
        
        logMsg += "HandSkill[0]: ";
        for (int i = 0; i < stat.slotCount; i++)
            logMsg += (_stat.handSkill[0][i] != null ? _stat.handSkill[0][i].skillData.grade.ToString() : "null") + " ";
            
        logMsg += "\nHandSkill[1]: ";
        for (int i = 0; i < stat.slotCount; i++)
            logMsg += (_stat.handSkill[1][i] != null ? _stat.handSkill[1][i].skillData.grade.ToString() : "null") + " ";
            
        logMsg += "\nNextSkill: ";
        for (int i = 0; i < stat.slotCount; i++)
            logMsg += (_stat.nextSkill[i] != null ? _stat.nextSkill[i].skillData.grade.ToString() : "null") + " ";
            
        logMsg += "\nWaitingSkills: ";
        foreach (var skill in _stat.waitingSkills)
            logMsg += (skill != null ? skill.skillData.grade.ToString() : "null") + " ";
            
        Debug.Log(logMsg);
    }
}
