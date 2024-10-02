using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace UIToolkitDemo {
    public enum ShopItemType {
        Gold,
        Gem,
        HealthPotion,
        LevelUpPotion
    }

    public static class ShopEvents {
        public static Action<ShopItemSo, Vector2> TransactionProcessed;
        public static Action<ShopItemType, uint, Vector2> RewardProcessed;
    }

    public class ShopItemSo : ScriptableObject {
        public ShopItemType m_contentType;
        public uint m_quantity;
    }

    public class ObjectPoolBehaviour : MonoBehaviour {
        [SerializeField] GameObject m_prefab;
        [SerializeField] int m_poolSize = 10;
        [SerializeField] List<GameObject> m_pooledObjects = new();

        void Awake() {
            for (var i = 0; i < m_poolSize; i++) {
                var obj = Instantiate(m_prefab, transform);
                obj.SetActive(false);
                m_pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject() {
            foreach (var obj in m_pooledObjects) {
                if (!obj.activeInHierarchy) {
                    return obj;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct MagnetData {
        // gold, gems, health potion, power potion
        public ShopItemType m_itemType;

        // ParticleSystem pool
        public ObjectPoolBehaviour m_fxPool;

        // forcefield target
        public ParticleSystemForceField m_forceField;
    }

    // FX generator when buying an item from the shop
    public class CoinMagnet : MonoBehaviour {
        [Header("UI Elements")]
        [Tooltip("Locate screen space positions from this document's UI elements.")]
        [SerializeField] UIDocument m_document;
        [Tooltip("Match a target VisualElement by name to each ShopItemType")]
        [SerializeField] List<MagnetData> m_magnetData;

        [Header("Camera")]
        [Tooltip("Use Camera and Depth to calculate world space positions.")]
        [SerializeField] Camera m_camera;
        [SerializeField] float m_zDepth = 10f;
        [Tooltip("3D offset to the particle emission")]
        [SerializeField] Vector3 m_sourceOffset = new(0f, 0.1f, 0f);

        // start and end coordinates for effect
        void OnEnable() {
            // FX from ShopScreen
            ShopEvents.TransactionProcessed += OnTransactionProcessed;

            // FX from MailScreen
            ShopEvents.RewardProcessed += OnRewardProcessed;

            ThemeEvents.CameraUpdated += OnCameraUpdated;
        }



        void OnDisable() {
            ShopEvents.TransactionProcessed -= OnTransactionProcessed;
            ShopEvents.RewardProcessed -= OnRewardProcessed;

            ThemeEvents.CameraUpdated += OnCameraUpdated;
        }

        ObjectPoolBehaviour GetFXPool(ShopItemType itemType) {
            var magnetData = m_magnetData.Find(x => x.m_itemType == itemType);
            return magnetData.m_fxPool;
        }

        ParticleSystemForceField GetForcefield(ShopItemType itemType) {
            var magnetData = m_magnetData.Find(x => x.m_itemType == itemType);
            return magnetData.m_forceField;
        }

        void PlayPooledFX(Vector2 screenPos, ShopItemType contentType) {
            var worldPos = screenPos.ScreenPosToWorldPos(m_camera, m_zDepth) + m_sourceOffset;

            var fxPool = GetFXPool(contentType);

            // Initialize ParticleSystem
            var ps = fxPool.GetPooledObject().GetComponent<ParticleSystem>();

            if (!ps)
                return;

            ps.gameObject.SetActive(true);
            ps.gameObject.transform.position = worldPos;

            // Add the Forcefield for destination
            var forceField = GetForcefield(contentType);
            forceField.gameObject.SetActive(true);

            // Update the ForceField position relative to the UI
            var positionToVisualElement = forceField.gameObject.GetComponent<PositionToVisualElement>();
            positionToVisualElement.MoveToElement();

            // Attach the ForceField to the particle system
            var externalForces = ps.externalForces;
            externalForces.enabled = true;
            externalForces.AddInfluence(forceField);

            ps.Play();

        }

        // event-handling methods

        // claiming free reward from MailScreen
        void OnRewardProcessed(ShopItemType rewardType, uint rewardQuantity, Vector2 screenPos) {
            // only play effect for gold or gem purchases
            if (rewardType == ShopItemType.HealthPotion || rewardType == ShopItemType.LevelUpPotion)
                return;

            PlayPooledFX(screenPos, rewardType);
        }

        // buying an item from the ShopScreen
        void OnTransactionProcessed(ShopItemSo shopItem, Vector2 screenPos) {
            // only play effect for gold or gem purchases
            if (shopItem.m_contentType == ShopItemType.HealthPotion || shopItem.m_contentType == ShopItemType.LevelUpPotion)
                return;

            PlayPooledFX(screenPos, shopItem.m_contentType);
        }

        void OnCameraUpdated(Camera cam) => m_camera = cam;
    }
}