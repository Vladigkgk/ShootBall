using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        public Action CloseWindow;

        public void Close()
        {   
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            CloseWindow?.Invoke();
        }
    }
}