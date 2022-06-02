using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop
{
    public class OpenShopWindowComponent : MonoBehaviour
    {
        [SerializeField] private string _windowName;
        [SerializeField] private Image _backGround;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _openShop;

        private Sprite _closeShop;

        private ShopWindow _shopWindowComponent;

        public void OpenWindow()
        {
            var shopWinodow = FindObjectOfType<ShopWindow>();
            if (shopWinodow != null)
            {
                Destroy(shopWinodow.gameObject);
            }
            var finalLevel = Resources.Load<GameObject>($"UI/{_windowName}");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            var shopWindow = Instantiate(finalLevel, convas.transform);
            _shopWindowComponent = shopWindow.GetComponent<ShopWindow>();
            NewView();
            _shopWindowComponent.CloseWindow += OldView;
        }

        private void NewView()
        {
            _closeShop = _backGround.sprite;
            _backGround.sprite = _openShop;
            _backGround.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }

        private void OldView()
        {
            _shopWindowComponent.CloseWindow -= OldView;
            _backGround.sprite = _closeShop;
            _backGround.gameObject.transform.localScale = Vector3.one;
        }
    }
}