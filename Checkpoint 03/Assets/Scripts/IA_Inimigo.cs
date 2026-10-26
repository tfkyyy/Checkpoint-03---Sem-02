using UnityEngine;

public class IA_Inimigo : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return
    }

    [Header("Referências")]
    [SerializeField] private Transform player;

    [Header("Configurações")]
    [SerializeField] private float detectar = 5f;
    [SerializeField] private float alcanceAtaque = 1f;
    [SerializeField] private float foraAlcanceDetectar = 8f;
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float cooldownAtaque = 1.5f;

    private EnemyState currentState = EnemyState.Idle;
    private Vector3 initialPosition;
    private float nextAttackTime;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distance <= detectar)
                    currentState = EnemyState.Move;
                break;

            case EnemyState.Move:
                if (distance > foraAlcanceDetectar)
                {
                    currentState = EnemyState.Return;
                }
                else if (distance <= alcanceAtaque)
                {
                    currentState = EnemyState.Attack;
                }
                else
                {
                    MoveTowards(player.position);
                }
                break;

            case EnemyState.Attack:
                if (distance > foraAlcanceDetectar)
                {
                    currentState = EnemyState.Return;
                }
                else if (distance > alcanceAtaque)
                {
                    currentState = EnemyState.Move;
                }
                else
                {
                    Ataque();
                }
                break;

            case EnemyState.Return:
                MoveTowards(initialPosition);

                if (Vector2.Distance(
                    transform.position,
                    initialPosition
                ) < 0.05f)
                {
                    transform.position = initialPosition;
                    currentState = EnemyState.Idle;
                }
                break;
        }
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            velocidade * Time.deltaTime
        );
    }

    private void Ataque()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + cooldownAtaque;
        }
    }
}