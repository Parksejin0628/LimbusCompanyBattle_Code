using UnityEngine;

public interface BattleCommand
{
    void Execute(ICharacter playerCharacter, ICharacter enemyCharacter);
} 
public class ClashCommand : BattleCommand
{
    public void Execute(ICharacter playerCharacter, ICharacter enemyCharacter)
    {
        Debug.Log($"Clash! Player: {playerCharacter}, Enemy: {enemyCharacter}");
    }
}

public class AttackCommand : BattleCommand
{
    public void Execute(ICharacter playerCharacter, ICharacter enemyCharacter)
    {
        Debug.Log($"Attack! Player: {playerCharacter}, Enemy: {enemyCharacter}");
    }
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(ICharacter playerCharacter, int playerSlotNum, ICharacter enemyCharacter, int enemySlotNum)
    {
        // 서로가 서로를 타겟으로 지정했는지 확인 (합 조건)
        if (playerCharacter.stat.slots[playerSlotNum].targetCharacter == enemyCharacter &&
            enemyCharacter.stat.slots[enemySlotNum].targetCharacter == playerCharacter)
        {
            BattleCommand clashCommand = new ClashCommand();
            clashCommand.Execute(playerCharacter, enemyCharacter);
        }
        else
        {
            BattleCommand attackCommand = new AttackCommand();
            attackCommand.Execute(playerCharacter, enemyCharacter);
        }
    }
}
