using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Components.LevelMeneger;
using Assets.Scripts.Creaters;
using Assets.Scripts.Creaters.Hero;
using Assets.Scripts.Model.Data;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        private PlayerData _save;

        public PlayerData Data => _data;
        public SkinModel SkinModel { get; private set; }
        public BullletModel BullletModel { get; private set; }
       
        private void Awake()
        {
            if (PlayerPrefs.HasKey("PlayerData"))
                _data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));

            var exitSession = GetExitSession();
            if (exitSession != null)
            {
                exitSession.StartSession();
                Destroy(gameObject);
            }
            else
            {
                InitModel();
                Save();
                DontDestroyOnLoad(this);
                StartSession();
            }
        }

        private GameSession GetExitSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if (session != this)
                {
                    return session;
                }
            }
            return null;
        }

        private void InitModel()
        {
            SkinModel = new SkinModel(_data);
            SkinModel.OnSkinUpdate += UpdateHeroSkin;
            BullletModel = new BullletModel(_data);
        }

        public void Save()
        {
            _save = _data.Clone();
            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(_save));
        }

        private void StartSession()
        {
            LoadHud();
            SpawnHero();
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private void SpawnHero()
        {
            var checkPoint = FindObjectOfType<CheckPointComponent>();
            checkPoint.SpawnHero();
        }

        public void UpdateHeroSkin()
        {
            var hero = FindObjectOfType<Hero>();
            var target = hero.gameObject.transform;
            Destroy(hero.gameObject);
            var skinId = Data.Skins.Used.Value;
            var heroSkin = DefsFacade.I.Skin.Get(skinId);
            var newHero = Instantiate(heroSkin.Skin, target.position, Quaternion.identity);
            var input = newHero.GetComponent<PlayerInput>();
            input.enabled = false;
            var setFollow = FindObjectOfType<SetFollowComponent>();
            if (setFollow != null)
                setFollow.SetFollow();
        }

        public void LoadLastLevel()
        {
            _data = _save.Clone();
            //dispose
            SkinModel.OnSkinUpdate -= UpdateHeroSkin;
            InitModel();
        }

    }
}
