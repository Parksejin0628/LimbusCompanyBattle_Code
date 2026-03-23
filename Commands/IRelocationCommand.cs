using UnityEngine;
using System.Linq;

public interface IRelocationCommand
{
    bool Execute();
}

public class SpeedRolocationCommand : IRelocationCommand
{
    private ICharacter[] characters;
    private Transform[] locations;

    public SpeedRolocationCommand(ICharacter[] characters, Transform[] locations)
    {
        this.characters = characters;
        this.locations = locations;
    }

    public bool Execute()
    {
        if (characters == null || locations == null) return false;

        // 캐릭터들을 속도 내림차순(빠른 순)으로 정렬
        var sortedCharacters = characters.OrderBy(c => c.stat.nowSpeed).ThenByDescending(c => c.stat.deploymentOrder).ToArray();

        // 정렬된 순서대로 지정된 location 위치로 이동시킴
        for (int i = 0; i < sortedCharacters.Length && i < locations.Length; i++)
        {
            sortedCharacters[i].CharacterTransform.position = locations[i].position;
        }

        return true;
    }
}