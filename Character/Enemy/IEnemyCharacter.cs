using System;
using System.Linq;
using UnityEngine;


public interface IEnemyCharacter : ICharacter
{
    void DetermineSkill();
    void DetermineTarget();
}
