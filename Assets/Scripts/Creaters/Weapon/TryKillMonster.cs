using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Weapon
{
    public class TryKillMonster : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void TryKill(GameObject monster)
        {
            var component = monster.GetComponent<KillMonster>();
            component?.ChangeHealth(_damage);

        }
    }
}