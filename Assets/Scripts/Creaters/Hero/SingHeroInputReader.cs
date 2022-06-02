using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Assets.Scripts.Creaters.Hero
{
    public class SingHeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Shoot();
            }
        }
    }
}

