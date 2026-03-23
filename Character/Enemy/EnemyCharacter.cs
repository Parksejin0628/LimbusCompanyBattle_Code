using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character, IEnemyCharacter
{
    [SerializeField]
    protected CharacterStat _stat = new CharacterStat();
    public override CharacterStat stat
    {
        get => _stat;
        set => _stat = value;
    }

    protected Dictionary<Slot, IDetermineSkillCommand> slotSkillCommands = new Dictionary<Slot, IDetermineSkillCommand>();
    protected Dictionary<Slot, IDetermineTargetCommand> slotTargetCommands = new Dictionary<Slot, IDetermineTargetCommand>();

    protected override void Awake()
    {
        base.Awake();
        
        if(stat.slots.Count == 0)
        {
            stat.slots.Add(new Slot());
            stat.slots[stat.slotCount].slotNumber = stat.slotCount;
            stat.slotCount++;
        }
    }
    protected override void Start()
    {
        base.Start();

        if (GameManager.instance != null)
        {
            GameManager.instance.onTurnStartDelegate += DetermineSkill;
            GameManager.instance.onTurnStartDelegate += DetermineTarget;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void DetermineSkill()
    {
        if (stat.slots == null) return;

        foreach (Slot slot in stat.slots)
        {
            if (slotSkillCommands.TryGetValue(slot, out IDetermineSkillCommand command))
            {
                command.Execute();
            }
        }
    }

    public void DetermineTarget()
    {
        if (stat.slots == null) return;

        foreach (Slot slot in stat.slots)
        {
            if (slotTargetCommands.TryGetValue(slot, out IDetermineTargetCommand command))
            {
                command.Execute();
            }
        }
    }
}
