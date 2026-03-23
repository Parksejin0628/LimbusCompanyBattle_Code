using UnityEngine;
using System.Linq;

public interface IDetermineTargetCommand
{
    void Execute();
}

public class DetermineRandomTarget : IDetermineTargetCommand
{
    static float totalAggro; // Slot의 aggro가 float 타입이므로 float로 변경합니다.
    private Slot _ownerSlot;


    public DetermineRandomTarget(Slot ownerSlot)
    {
        _ownerSlot = ownerSlot;
    }

    public void Execute()
    {
        // 씬 내의 모든 IPlayerCharacter를 찾습니다.
        var playerCharacters = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPlayerCharacter>().ToArray();

        totalAggro = 0f;
        
        // 모든 플레이어 캐릭터의 각 슬롯을 순회하여 어그로를 합산합니다.
        foreach (var player in playerCharacters)
        {
            if (player.stat.slots == null) continue;

            foreach (var slot in player.stat.slots)
            {
                if (slot != null) totalAggro += slot.aggro;
            }
        }

        // 0부터 totalAggro 사이에서 무작위 값을 하나 뽑습니다.
        float randomValue = UnityEngine.Random.Range(0f, totalAggro);

        // 다시 순회하면서 randomValue를 차감하여 타겟을 결정합니다.
        foreach (var player in playerCharacters)
        {
            if (player.stat.slots == null) continue;

            foreach (var slot in player.stat.slots)
            {
                if (slot == null) continue;

                if (randomValue <= slot.aggro)
                {
                    _ownerSlot.targetCharacter = player;
                    _ownerSlot.targetSlotNum = slot.slotNumber;
                    return;
                }
                
                randomValue -= slot.aggro;
            }
        }
    }
}