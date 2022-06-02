using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.DataGroup
{
    public class PredefinedDataGroup<TDataType, TItemType> : DataGroup<TDataType, TItemType>
        where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        public PredefinedDataGroup(Transform container) : base(null, container)
        {
            var items = container.GetComponentsInChildren<TItemType>();
            CreatedItems.AddRange(items);
        }

        public override void SetData(IList<TDataType> data)
        {
            if (data.Count < CreatedItems.Count)
            {
                throw new IndexOutOfRangeException();
            }
            base.SetData(data);
        }
    }
}
