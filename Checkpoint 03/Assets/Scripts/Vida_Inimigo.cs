using System.Collections;
using UnityEngine;

public class Vida_Inimigo : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int maxVida = 100;

    private int vidaAtual;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine efeitoDano;

    private void Awake()
    {
        vidaAtual = maxVida;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void TomaDano (int damage)
    {
        if (vidaAtual <= 0 || damage <= 0)
            return;

        vidaAtual = vidaAtual - damage;

        if (spriteRenderer != null)
        {
            if (efeitoDano != null)
                StopCoroutine(efeitoDano);

            efeitoDano = StartCoroutine(DanoFlash());
        }

        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DanoFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        efeitoDano = null;
    }
}