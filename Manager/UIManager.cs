using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class UIManager : MonoBehaviour
{
    public GameObject SkillPannel;
    public GameObject SkillBlock;
    public GameObject SkillIconOnCharacterPrefab; // 캐릭터 위에 띄울 스킬 아이콘 프리팹
    public GameObject SkillIconOnCharacters; // 스킬 아이콘들을 담을 부모 오브젝트
    public Vector3 skillIconOffset = new Vector3(0, 2f, 0); // 캐릭터 위치를 기준으로 얼마나 위에 띄울지 오프셋

    private IObjectPool<GameObject> _skillBlockPool;
    private IObjectPool<GameObject> _skillIconPool;
    
    public Dictionary<Slot, GameObject> skillBlockMap = new Dictionary<Slot, GameObject>();
    public Dictionary<ICharacter, GameObject> SkillIconOnCharacter = new Dictionary<ICharacter, GameObject>();

    public static UIManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        _skillBlockPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(SkillBlock, SkillPannel.transform),
            actionOnGet: (block) => block.SetActive(true),
            actionOnRelease: (block) => block.SetActive(false),
            actionOnDestroy: (block) => Destroy(block)
        );

        _skillIconPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(SkillIconOnCharacterPrefab, SkillIconOnCharacters.transform),
            actionOnGet: (icon) => icon.SetActive(true),
            actionOnRelease: (icon) => icon.SetActive(false),
            actionOnDestroy: (icon) => Destroy(icon)
        );
    }

    void Start()
    {
        // GameManager의 턴 시작 이벤트에 RelocationSkillPannel 메서드를 등록합니다.
        GameManager.instance.onTurnStartUIDelegate += UpdateSkillPannel;
        GameManager.instance.onTurnStartUIDelegate += UpdateSkillIconOnCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnSkillBlock(Slot slot)
    {
        GameObject block = _skillBlockPool.Get();
        skillBlockMap[slot] = block;
        block.transform.SetSiblingIndex(1);
        return block;
    }

    public void DespawnSkillBlock(Slot slot)
    {
        if (skillBlockMap.TryGetValue(slot, out GameObject block))
        {
            skillBlockMap.Remove(slot);
            _skillBlockPool.Release(block);
        }
    }

    void UpdateSkillPannel()
    {
        // 각 플레이어 캐릭터의 슬롯마다 맵에 할당된 스킬 블록이 없을 경우에만 생성합니다.
        foreach (IPlayerCharacter player in GameManager.instance.playerCharacters.Reverse())
        {

            foreach (Slot slot in player.stat.slots)
            {
                if (!skillBlockMap.ContainsKey(slot))
                {
                    SpawnSkillBlock(slot);
                }
                //UI 정렬
                skillBlockMap[slot].transform.SetSiblingIndex(1);
                //UI 업데이트
                skillBlockMap[slot].GetComponent<SkillBlockUI>().UpdateStatUI(player, slot.slotNumber);
            }
        }
    }

    public void UpdateSkillIconOnCharacter()
    {
        if (GameManager.instance == null || GameManager.instance.characters == null) return;

        foreach (ICharacter character in GameManager.instance.characters)
        {
            if (!SkillIconOnCharacter.ContainsKey(character))
            {
                SkillIconOnCharacter[character] = _skillIconPool.Get();
            }

            GameObject iconObj = SkillIconOnCharacter[character];
            if (Camera.main != null && character.CharacterTransform != null)
            {
                Vector3 worldPos = character.CharacterTransform.position + skillIconOffset;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                iconObj.transform.position = screenPos;
            }

            iconObj.GetComponent<SkillIconOnCharacterUI>()?.UpdateStatUI(character, 0);
        }
    }
}
