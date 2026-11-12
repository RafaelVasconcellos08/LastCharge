
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform jogador;

    [Header("Camera")]
    public float distancia = 5f;
    public float altura = 2f;
    public float sensibilidadeMouse = 3f;
    public float suavidade = 10f;

    private float rotacaoHorizontal;
    private float rotacaoVertical = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (jogador == null)
            return;

        rotacaoHorizontal += Input.GetAxis("Mouse X") * sensibilidadeMouse;
        rotacaoVertical -= Input.GetAxis("Mouse Y") * sensibilidadeMouse;

        rotacaoVertical = Mathf.Clamp(rotacaoVertical, -20f, 60f);

        Quaternion rotacao = Quaternion.Euler(
            rotacaoVertical,
            rotacaoHorizontal,
            0f
        );

        Vector3 deslocamento = rotacao * new Vector3(0f, 0f, -distancia);

        Vector3 posicaoDesejada = jogador.position
            + Vector3.up * altura
            + deslocamento;

        transform.position = Vector3.Lerp(
            transform.position,
            posicaoDesejada,
            suavidade * Time.deltaTime
        );

        transform.LookAt(jogador.position + Vector3.up * altura);
    }

    void OnApplicationFocus(bool foco)
    {
        Cursor.lockState = foco
            ? CursorLockMode.Locked
            : CursorLockMode.None;

        Cursor.visible = !foco;
    }
}
