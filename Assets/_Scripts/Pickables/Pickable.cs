using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D m_boxCollider2D;
        [SerializeField] protected bool m_isMovingTowardPlayer;
        private readonly float m_flyingSpeed = 20;
        private readonly float m_disableColliderDuration = 0.5f;

        private void OnEnable()
        {
            StartCoroutine(DisableColliderForDuration());
        }

        private IEnumerator DisableColliderForDuration()
        {
            m_boxCollider2D.enabled = false;
            yield return new WaitForSeconds(m_disableColliderDuration);
            m_boxCollider2D.enabled = true;
        }

        public virtual void Picking(Transform playerTransform)
        {
            if (m_isMovingTowardPlayer) return;
            StartCoroutine(OnMovingToPlayer(playerTransform));
        }

        protected virtual IEnumerator OnMovingToPlayer(Transform playerTransform)
        {
            bool tweenComplete = false;
            m_isMovingTowardPlayer = true;
            var directionToPlayer = (playerTransform.position - transform.position).normalized;
            var tween = transform.DOMove(transform.position - directionToPlayer * 1f, 0.2f)
                .OnComplete(() => tweenComplete = true);
            
            yield return new WaitUntil(()=> tweenComplete);
            var distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            while (distToPlayer > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, m_flyingSpeed * Time.deltaTime);
                distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
                yield return null;
            }

            Picked();
        }

        protected virtual void Picked()
        {
            Destroy(gameObject);
        }
    }
}

