using UnityEngine;

namespace TradeMarket.BulletSystem
{
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;
        private SpriteRenderer bulletSpriteRenderer;
        private Rigidbody2D bulletRigidBody;
        private Collider2D bulletCollider;

        private void Awake()
        {
            bulletSpriteRenderer = GetComponent<SpriteRenderer>();
            bulletRigidBody = GetComponent<Rigidbody2D>();
            bulletCollider = GetComponent<Collider2D>();
        }

        public void Initialize(BulletController controller)
        {
            bulletController = controller;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            if (!active)
            {
                bulletRigidBody.linearVelocity = Vector2.zero;
            }
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            bulletRigidBody.linearVelocity = velocity;
        }

        public void SetSprite(Sprite sprite)
        {
            if (bulletSpriteRenderer != null)
                bulletSpriteRenderer.sprite = sprite;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bulletController?.OnCollisionDetected(collision);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bulletController?.OnTriggerDetected(other);
        }
    }
}