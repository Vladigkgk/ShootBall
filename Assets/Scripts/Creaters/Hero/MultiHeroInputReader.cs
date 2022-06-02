using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Creaters.Hero
{
    public class MultiHeroInputReader : MonoBehaviour
    {
        [SerializeField] private PressHero _pressHero;

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _pressHero.StartTouch();
            }
            
            if (context.canceled)
            {
                _pressHero.EndTouch();
            }
        }
    }
}