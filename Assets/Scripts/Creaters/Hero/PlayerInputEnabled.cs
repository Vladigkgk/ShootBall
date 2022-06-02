using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Creaters.Hero
{
    public class PlayerInputEnabled : MonoBehaviour
    {
       public void EnabledPlayerInput()
       {
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = true;
       }
        
        public void DisabledPlayerInput()
        {
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = false;
        }
    }
}