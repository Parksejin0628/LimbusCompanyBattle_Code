using UnityEngine;



public abstract class Character : MonoBehaviour, ICharacter
{
    public Skill[] skills;
    public abstract CharacterStat stat { get; set; }
    
    public Transform CharacterTransform => transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        stat.nowHp = stat.maxHp;
    }

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual int DetermineSpeed()
    {
        stat.nowSpeed = Random.Range(stat.minSpeed, stat.maxSpeed+1);

        return stat.nowSpeed;
    }
}
