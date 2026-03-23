using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
    public Transform[] locations; // 인스펙터에서 캐릭터가 배치될 Transform들을 할당해주세요.
    public ICharacter[] characters;
    public IPlayerCharacter[] playerCharacters;
    public IEnemyCharacter[] enemyCharacters;
    public Dictionary<ICharacter, int> playerCharacterOrder = new Dictionary<ICharacter, int>();
    public Dictionary<ICharacter, int> enemyCharacterOrder = new Dictionary<ICharacter, int>();
    public int turn = 1;
    public Action onTurnStartDelegate;
    public Action onTurnStartUIDelegate;
    public static GameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        FindCharacters();
    }

    private void FindCharacters()
    {
        // 씬 내의 모든 MonoBehaviour 중 ICharacter를 구현한 컴포넌트들을 찾습니다.
        characters = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ICharacter>().ToArray();

        playerCharacters = characters.OfType<IPlayerCharacter>().ToArray();
        enemyCharacters = characters.OfType<IEnemyCharacter>().ToArray();
    }

    void Start()
    {
        OnTurnStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 OnTurnStart()를 호출합니다. (새로운 Input System 기반)
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            OnTurnStart();
        }
    }

    private void ResetSlots()
    {
        foreach (ICharacter character in characters)
        {
            foreach (Slot slot in character.stat.slots)
            {
                slot.skillOriginContainer = ESkillTarget.NONE;
                slot.targetCharacter = null;
                slot.targetSlotNum = 0;
                slot.skill = null;
            }
        }
    }

    private void OnTurnStart()
    {
        turn++;

        ResetSlots();
    
        foreach(ICharacter character in characters)
        {
            character.DetermineSpeed();
        }

        // 속도(nowSpeed)를 기준으로 내림차순(빠른 순) 정렬하고, 같으면 배치 순서(deploymentOrder) 기준 오름차순 정렬합니다.
        characters = characters.OrderByDescending(c => c.stat.nowSpeed).ThenBy(c => c.stat.deploymentOrder).ToArray();
        playerCharacters = playerCharacters.OrderByDescending(c => c.stat.nowSpeed).ThenBy(c => c.stat.deploymentOrder).ToArray();
        enemyCharacters = enemyCharacters.OrderByDescending(c => c.stat.nowSpeed).ThenBy(c => c.stat.deploymentOrder).ToArray();

        playerCharacterOrder.Clear();
        for (int i = 0; i < playerCharacters.Length; i++)
        {
            playerCharacterOrder[playerCharacters[i]] = i;
        }

        enemyCharacterOrder.Clear();
        for (int i = 0; i < enemyCharacters.Length; i++)
        {
            enemyCharacterOrder[enemyCharacters[i]] = i;
        }

        // 커맨드 객체를 생성하고 실행하여 캐릭터들을 재배치합니다.
        IRelocationCommand relocationCommand = new SpeedRolocationCommand(playerCharacters, locations);
        relocationCommand.Execute();

        // 델리게이트 실행
        onTurnStartDelegate?.Invoke();
        onTurnStartUIDelegate?.Invoke();
    }

    public void OnBattleStart()
    {
        characters = characters.OrderByDescending(c => c.stat.nowSpeed).ThenBy(c => c.stat.deploymentOrder).ToArray();

        foreach (ICharacter character in characters)
        {
            foreach (Slot slot in character.stat.slots)
            {
                if (BattleManager.instance != null && slot.targetCharacter != null)
                {
                    BattleManager.instance.StartBattle(character, slot.slotNumber, slot.targetCharacter, slot.targetSlotNum);
                }
            }
        }
    }

    public void DeterminePlayerCharacterSkill(ICharacter character, ESkillTarget skillOriginContainer, int slotNum)
    {
        character.stat.slots[slotNum].skillOriginContainer = skillOriginContainer;

        // 적 대상 지정 및 대상 슬롯 0으로 초기화
        int targetIndex = (enemyCharacters.Length + playerCharacterOrder[character]) % enemyCharacters.Length;
        character.stat.slots[slotNum].targetCharacter = enemyCharacters[targetIndex];
        character.stat.slots[slotNum].targetSlotNum = 0;

        // 플레이어 캐릭터의 속도가 더 높을 경우, 적의 타겟을 플레이어 캐릭터로 변경 (합 가로채기)
        if (character.stat.nowSpeed > enemyCharacters[targetIndex].stat.nowSpeed)
        {
            enemyCharacters[targetIndex].stat.slots[0].targetCharacter = character;
            enemyCharacters[targetIndex].stat.slots[0].targetSlotNum = slotNum;
        }

        if (character.stat is PlayerCharacterStat playerStat)
        {
            if (skillOriginContainer == ESkillTarget.HAND_DOWN)
            {
                character.stat.slots[slotNum].skill = playerStat.handSkill[0][slotNum];
            }
            else if (skillOriginContainer == ESkillTarget.HAND_UP)
            {
                character.stat.slots[slotNum].skill = playerStat.handSkill[1][slotNum];
            }
        }
    }
}
