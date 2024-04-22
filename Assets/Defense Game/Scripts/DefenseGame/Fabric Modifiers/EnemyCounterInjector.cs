using Unity.VisualScripting;
using UnityEngine;


namespace DefenseGame
{
    public class EnemyCounterInjector : IFabricModifier
    {
        private EnemyCounter _enemyCounter;

        public EnemyCounterInjector(EnemyCounter enemyCounter)
        {
            _enemyCounter = enemyCounter;
        }

        public void ModifyObject(MonoBehaviour obj)
        {
            var isAccountable = obj.GetComponent<EnemyComponent>().IsAccountable;

            if (isAccountable)
            {
                var account = obj.AddComponent<EnemyAccount>();
                account.AddCounter(_enemyCounter);
            }
        }
    }
}