using UnityEngine;

namespace TradeMarket.BulletSystem
{
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;
        private SpriteRenderer bulletSpriteRenderer;
        private Rigidbody2D bulletRigidBody;

        private void Awake()
        {
            bulletSpriteRenderer = GetComponent<SpriteRenderer>();
            bulletRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(BulletController controllerToInitialize) => bulletController = controllerToInitialize;

        public BulletController GetController() => bulletController;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            if (!active)
                bulletRigidBody.linearVelocity = Vector2.zero;
        }

        public void SetPosition(Vector2 position) => transform.position = position;

        public void SetVelocity(Vector2 velocity) => bulletRigidBody.linearVelocity = velocity;

        public void SetSprite(Sprite sprite)
        {
            if (bulletSpriteRenderer != null)
                bulletSpriteRenderer.sprite = sprite;
        }
    }
}