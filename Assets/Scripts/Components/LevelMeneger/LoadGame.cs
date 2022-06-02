using Assets.Scripts.Model.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Components.LevelMeneger
{
    public class LoadGame : MonoBehaviour
    {
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("PlayerData"))
                SceneManager.LoadScene(1);
            var data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));
            SceneManager.LoadScene(data.CurrentLevel.Value);
        }
    }
}