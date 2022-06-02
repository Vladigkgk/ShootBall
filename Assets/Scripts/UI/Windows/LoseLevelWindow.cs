using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Windows
{
    public class LoseLevelWindow : FinalLevelWindow
    {

        public void ReloadLevel()
        {

            var session = FindObjectOfType<GameSession>();
            session.LoadLastLevel();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}