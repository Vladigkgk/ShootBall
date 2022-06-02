using System.Collections;
using Assets.Scripts.Components.GoBased;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using UnityEngine;

namespace Assets.Scripts.Components.LevelMeneger
{
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        public void SpawnHero()
        {
            var session = FindObjectOfType<GameSession>();
            var skinId = session.Data.Skins.Used.Value;
            var heroSkin = DefsFacade.I.Skin.Get(skinId);
            Instantiate(heroSkin.Skin, transform.position, Quaternion.identity);
        }
    }
}