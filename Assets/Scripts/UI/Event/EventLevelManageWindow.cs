using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Event
{
    public class EventLevelManageWindow : MonoBehaviour
    {
        [SerializeField] private Transform _contrainerWidget;

        private PredefinedDataGroup<EventDef, EventLevelWidget> _dataGroup;
        private GameSession _session;

        private void Start()
        {
            _dataGroup = new PredefinedDataGroup<EventDef, EventLevelWidget>(_contrainerWidget);
            _session = FindObjectOfType<GameSession>();
            _session.Data.ShowAds.OnSimpleChanged += OnEventLevelChanged;
            OnEventLevelChanged();
        }

        private void OnEventLevelChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Event.All);
        }

        private void OnDestroy()
        {
            _session.Data.ShowAds.OnSimpleChanged -= OnEventLevelChanged;
        }

    }
}