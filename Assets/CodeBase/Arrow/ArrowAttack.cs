using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Arrow
{
    public class ArrowAttack : MonoBehaviour
    {
        [SerializeField] private float _damage = 10;

        private void OnTriggerEnter2D(Collider2D enemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}