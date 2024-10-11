using System;
using System.Collections.Generic;
using Quantum;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.UIs.Enemies {
    public class UIHealthBarsManager : MonoBehaviour {

        [SerializeField] private UIHealthBar prefab;
        [SerializeField] private float spawnDelay = 0.1f;
        
        private ObjectPool<UIHealthBar> objectPool;
        private EntityViewUpdater entityViewUpdater;
        private ViewService service;
        
        private IDisposable spawnSubscription;
        private readonly Dictionary<EntityRef, UIHealthBar> spawnedHealthBars = new();

        private void Awake() {
            service = FindObjectOfType<ViewService>();
            entityViewUpdater = FindObjectOfType<EntityViewUpdater>();
            objectPool = CreateObjectPool();
            
            spawnSubscription = service.EventsSubject
                .OfType<object, EventEnemySpawn>()
                .Delay(TimeSpan.FromSeconds(spawnDelay))
                .Subscribe(HandleEnemySpawn);
            
            service.EventsSubject
                .OfType<object, EventEnemyDeath>()
                .Subscribe(HandleEnemyDeath);
        }
        
        private void OnDestroy() {
            spawnSubscription.Dispose();
        }
        
        private void HandleEnemySpawn(EventEnemySpawn spawnEvent) {
            EntityRef entityRef = spawnEvent.Enemy;
            EntityView entityView = entityViewUpdater.GetView(entityRef);
            if (entityView is null) {
                Debug.LogWarning($"EntityView for {entityRef} not found");
                return;
            }
            
            CreateHealthBar(entityView);
        }
        
        private void HandleEnemyDeath(EventEnemyDeath deathEvent) {
            if (!spawnedHealthBars.TryGetValue(deathEvent.Victim, out UIHealthBar healthBar)) {
                return;
            }

            RemoveHealthBar(healthBar);
        }
        
        private ObjectPool<UIHealthBar> CreateObjectPool() {
            return new ObjectPool<UIHealthBar>(createFunc: () => {
                UIHealthBar instance = Instantiate(prefab, transform);
                instance.gameObject.SetActive(false);
                return instance;
            }, actionOnRelease: bar => bar.gameObject.SetActive(false));
        }

        private void CreateHealthBar(EntityView entityView) {
            UIHealthBar healthBar = objectPool.Get();
            healthBar.Setup(entityView);
            spawnedHealthBars.Add(entityView.EntityRef, healthBar);
        }
        
        public void RemoveHealthBar(UIHealthBar healthBar) {
            objectPool.Release(healthBar);
            spawnedHealthBars.Remove(healthBar.AttachedEntity.EntityRef);
        }
        
    }
   
}