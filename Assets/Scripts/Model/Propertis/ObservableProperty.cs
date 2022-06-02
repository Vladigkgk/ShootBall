using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Propertis
{
    [Serializable]
    public class ObservableProperty<TPropertiType>
    {
        [SerializeField] private TPropertiType _value;

        public delegate void OnPropertyChanged(TPropertiType newValue, TPropertiType oldValue);

        public event OnPropertyChanged OnChanged;

        public Action OnSimpleChanged;

        public virtual TPropertiType Value
        {
            get => _value;

            set
            {
                var isSame = Value?.Equals(value) ?? false;
                if (isSame) return;
                var oldValue = _value;
                _value = value;
                OnSimpleChanged?.Invoke();
                InvokeChangeEvent(_value, oldValue);

            }
        }

        protected void InvokeChangeEvent(TPropertiType newValue, TPropertiType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);
        }
    }
}
