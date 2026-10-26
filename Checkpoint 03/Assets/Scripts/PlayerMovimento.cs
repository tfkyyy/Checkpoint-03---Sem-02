using UnityEngine;

public class PlayerMovemento : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;

        transform.position += movement * velocidade * Time.deltaTime;
    }
}