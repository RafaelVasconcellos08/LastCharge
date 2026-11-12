
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimentacao")]
    public float velocidade = 5f;
    public float velocidadeRotacao = 10f;
    public Transform cameraTransform;

    [Header("Gravidade")]
    public float gravidade = -9.81f;

    private CharacterController controller;
    private Vector3 velocidadeVertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Direcoes baseadas na camera
        Vector3 frente = cameraTransform.forward;
        Vector3 direita = cameraTransform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        Vector3 movimento = direita * horizontal + frente * vertical;
        movimento = Vector3.ClampMagnitude(movimento, 1f);

        // Rotacao do personagem
        if (movimento.magnitude > 0.1f)
        {
            Quaternion rotacaoDesejada = Quaternion.LookRotation(movimento);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacaoDesejada,
                velocidadeRotacao * Time.deltaTime
            );
        }

        // Movimentacao
        controller.Move(movimento * velocidade * Time.deltaTime);

        // Gravidade
        if (controller.isGrounded && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f;
        }

        velocidadeVertical.y += gravidade * Time.deltaTime;

        controller.Move(velocidadeVertical * Time.deltaTime);
    }
}
