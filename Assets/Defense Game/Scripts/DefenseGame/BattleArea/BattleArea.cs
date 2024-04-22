using UnityEngine;


namespace DefenseGame
{
    public class BattleArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var baCollider = collision.GetComponent<BACollider>();
            
            baCollider.OnEnterBattleArea();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var baCollider = collision.GetComponent<BACollider>();
            baCollider.OnExitBattleArea();
        }
    }
}