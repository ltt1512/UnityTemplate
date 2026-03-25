using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Gameplay
{
    public class BoosterCtrl : BaseCtrl
    {
        [SerializeField] private List<BaseBooster> _boosters;
        private Dictionary<BoosterType, int> _inventory = new();
        private const string SaveKey = "booster_inventory";

        private void Awake()
        {
            LoadData();
        }

        public bool TryUse(BoosterType type)
        {
            if (!IsAvailable(type))
            {
                Debug.Log($"[BoosterCtrl] Booster {type} not available.");
                return false;
            }

            var booster = GetBooster(type);
            if (booster == null || booster.IsActive)
                return false;
            booster.Activate();
            Consume(type);
            SaveData();
            BoosterGameEvents.OnBoosterUsed?.Invoke(type);
            return true;
        }

        public void AddBooster(BoosterType type, int amount = 1)
        {
            if (!_inventory.ContainsKey(type))
                _inventory[type] = 0;

            _inventory[type] += amount;
            SaveData();
            BoosterGameEvents.OnBoosterCountChanged?.Invoke(type, _inventory[type]);
        }

        public int GetCount(BoosterType type)
        {
            return _inventory.TryGetValue(type, out int count) ? count : 0;
        }

        public bool IsAvailable(BoosterType type)
        {
            return GetCount(type) > 0;
        }

        private void Consume(BoosterType type)
        {
            if (!_inventory.ContainsKey(type)) return;
            _inventory[type] = Mathf.Max(0, _inventory[type] - 1);
            BoosterGameEvents.OnBoosterCountChanged?.Invoke(type, _inventory[type]);
        }

        private BaseBooster GetBooster(BoosterType type)
        {
            return _boosters.Find(b => b.Data != null && b.Data.type == type);
        }

        private void SaveData()
        {
            ES3.Save(SaveKey, _inventory);
        }

        private void LoadData()
        {
            if (ES3.KeyExists(SaveKey))
                _inventory = ES3.Load<Dictionary<BoosterType, int>>(SaveKey);
            else
                _inventory = new Dictionary<BoosterType, int>();
        }

        public override void Init()
        {
        }

        public override void StartGame()
        {
        }

        public override void Reset()
        {
        }
    }
}