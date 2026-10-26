using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    [Header("Configurações do ataque")]
    [SerializeField] private int dano = 25;
    [SerializeField] private float alcanceAtaque = 1.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ataque();
        }
    }

    private void Ataque()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            alcanceAtaque);

        foreach (Collider2D target in targets)
        {
            Vida_Inimigo enemy =
                target.GetComponentInParent<Vida_Inimigo>();

            if (enemy != null)
            {
                enemy.TomaDano(dano);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alcanceAtaque);
    }
}