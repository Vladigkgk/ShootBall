using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Hero
{
    public class KillHeroComponent : MonoBehaviour
    {
        public void KillHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            hero.Die();
        }
    }
}