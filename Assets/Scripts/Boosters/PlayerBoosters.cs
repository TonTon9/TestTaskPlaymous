using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player.Entity;
using UnityEngine;

namespace Boosters
{
    public class PlayerBoosters : MonoBehaviour, IPlayerBoosters
    {
        private const string SPEED_SETTINGS_PATH = "Dtos/Boosters/SpeedBoosterSetting";
        private const string IMMUNE_SETTINGS_PATH = "Dtos/Boosters/ImmuneBoosterSetting";
        
        private Dictionary<BoosterType, Coroutine> _currentBoosters = new ();
        
        private Dictionary<BoosterType, Booster> _boosters = new ();

        public void InitBoosters(IPlayerView view)
        {
            var speedSettings = Resources.Load<SpeedBoosterSetting>(SPEED_SETTINGS_PATH);
            var immuneBooster = Resources.Load<ImmuneBoosterSetting>(IMMUNE_SETTINGS_PATH);
            
            _boosters.Add(BoosterType.Speed, new SpeedBooster(view,BoostType.Long, speedSettings.Duration, speedSettings.SpeedMultiplier));
            _boosters.Add(BoosterType.Immune, new ImmuneBooster(view, BoostType.Long, immuneBooster.Duration));
            _boosters.Add(BoosterType.Heal, new HealthBooster(view, BoostType.Instant, 0f));
        }

        public void ApplyBooster(BoosterType boosterType)
        {
            var booster = GetBoosterPrefabByType(boosterType);
            if (booster.BoostType == BoostType.Instant)
            {
                booster.Activate();
                return;
            }
            if (_currentBoosters.ContainsKey(boosterType))
            {
                StopCoroutine(_currentBoosters[boosterType]);
            } else
            {
                booster.Activate();
            }
            _currentBoosters[boosterType] = StartCoroutine(RemoveBooster(booster.Duration, booster.Deactivate,boosterType));
        }

        private Booster GetBoosterPrefabByType(BoosterType boosterType)
        {
            var booster = _boosters.FirstOrDefault(t => t.Key.Equals(boosterType));
            if (booster.Value != null)
            {
                return booster.Value;
            } 
            throw new Exception("Have no booster prefab with this type");
        }

        private IEnumerator RemoveBooster(float duration, Action removeAction, BoosterType type)
        {
            yield return new WaitForSeconds(duration);
            _currentBoosters.Remove(type);
            removeAction();
        }
    }

}
