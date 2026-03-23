using UnityEngine;

public interface IDetermineSkillCommand
{
    void Execute();
}

public class DetermineRandomSkill : IDetermineSkillCommand
{
    private Skill[] _skills;
    private Slot _slot;

    public DetermineRandomSkill(Skill[] skills, Slot slot)
    {
        _skills = skills;
        _slot = slot;
    }

    public void Execute()
    {
        if (_skills == null || _skills.Length == 0 || _slot == null) return;

        int randomIndex = UnityEngine.Random.Range(0, _skills.Length);
        _slot.skill = _skills[randomIndex];
    }
}
