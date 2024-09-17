using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    // private Animator animator;
    //
    // private void Start()
    // {
    //     animator = GetComponent<Animator>();
    //
    //     // Подписываемся на события изменения состояния
    //     EnemyAI.OnIdle += PlayIdleAnimation;
    //     EnemyAI.OnRoaming += PlayRoamingAnimation;
    // }
    //
    // private void OnDestroy()
    // {
    //     // Отписываемся от событий при уничтожении объекта
    //     EnemyAI.OnIdle -= PlayIdleAnimation;
    //     EnemyAI.OnRoaming -= PlayRoamingAnimation;
    // }
    //
    // private void PlayIdleAnimation()
    // {
    //     if (animator != null)
    //     {
    //         animator.Play("Idle"); // Замените "Idle" на имя вашей анимации для состояния Idle
    //     }
    // }
    //
    // private void PlayRoamingAnimation()
    // {
    //     if (animator != null)
    //     {
    //         animator.Play("Roaming"); // Замените "Roaming" на имя вашей анимации для состояния Roaming
    //     }
    // }
}