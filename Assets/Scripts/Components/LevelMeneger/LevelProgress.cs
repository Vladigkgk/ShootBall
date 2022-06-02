using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components.GoBased;
using Assets.Scripts.Creaters;
using Assets.Scripts.Model;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.LevelMeneger
{
    public class LevelProgress : MonoBehaviour
    {
        private List<KillMonster> _listEnemy = new List<KillMonster>();
        private SpawnComponent[] _spawnerEnemy;    
        private int _countKill;

        public int CountKill => _listEnemy.Count;

        private void Start()
        {
            _spawnerEnemy = GetComponentsInChildren<SpawnComponent>();
            foreach (var spawner in _spawnerEnemy)
            {
                var countEnemy = spawner.InstanseSpawn();
                var killMonseter = countEnemy.GetComponent<KillMonster>();
                killMonseter.OnKill += CheckProgress;
                _listEnemy.Add(killMonseter);
            }

        }

        private void CheckProgress()
        {
            int count = 0;
            foreach (var  enemyItem in _listEnemy)
            {
                if (enemyItem.IsDead)
                    count += 1;
            }
            if (count == _listEnemy.Count)
            {

                LoadFinalLevelWindow();
                return;
            }
        }

        private void LoadFinalLevelWindow()
        {
            var finalLevel = Resources.Load<GameObject>("UI/FinalLevel");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            Instantiate(finalLevel, convas.transform);
        }
    }
}