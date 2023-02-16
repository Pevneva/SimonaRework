using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Arrow
{
    public class ArrowAttack : MonoBehaviour
    {
        [SerializeField] private float _damage = 10;

        private void OnTriggerEnter2D(Collider2D hittable)
        {
            hittable.transform.parent.GetComponent<IHealth>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}