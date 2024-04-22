using UnityEngine;
using System;

namespace DefenseGame
{
    public class OldManAnimationController : BasicEnemyAnimationController
    {
        [SerializeField] private string _deathVarIntName;
        [SerializeField] FloatRandomizer _deathRandomizer;

        public override void InitializeAfterActivation()
        {
            base.InitializeAfterActivation();

            Animator.SetFloat(_deathVarIntName, _deathRandomizer.GetRandomFloat());
        }

    }
}