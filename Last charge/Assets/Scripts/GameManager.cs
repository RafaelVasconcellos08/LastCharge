
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Baterias")]
    public int bateriasColetadas = 0;
    public int totalBaterias = 5;
    public TMP_Text textoBaterias;

    [Header("Vidas")]
    public int vidas = 3;
    public TMP_Text textoVidas;

    [Header("Pontuacao")]
    public int pontuacao = 0;
    public int pontosPorBateria = 100;
    public TMP_Text textoPontuacao;

    [Header("Game Over")]
    public GameObject telaGameOver;
    public GameObject telaVitoria;

    public bool jogoEncerrado = false;

    void Start()
    {
        Time.timeScale = 1f;
        AtualizarHUD();

        telaGameOver.SetActive(false);
        telaVitoria.SetActive(false);
    }

    public void ColetarBateria()
    {
        if (jogoEncerrado)
            return;

        bateriasColetadas++;
        pontuacao += pontosPorBateria;

        Debug.Log("Baterias: " + bateriasColetadas + "/" + totalBaterias);
        Debug.Log("Pontuacao: " + pontuacao);

        AtualizarHUD();
    }

    public void ReceberDano()
    {
        if (jogoEncerrado)
            return;

        vidas--;

        Debug.Log("Vidas restantes: " + vidas);

        AtualizarHUD();

        if (vidas <= 0)
        {
            EncerrarPartida();
        }
    }

    void EncerrarPartida()
    {
        jogoEncerrado = true;
        telaGameOver.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("GAME OVER!");
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void AtualizarHUD()
    {
        textoBaterias.text = "Baterias: " +
            bateriasColetadas + "/" + totalBaterias;

        textoVidas.text = "Vidas: " + vidas;

        textoPontuacao.text = "Pontuação: " + pontuacao;
    }
    public void TentarVencer()
    {
        if (jogoEncerrado)
            return;

        if (bateriasColetadas >= totalBaterias)
        {
            jogoEncerrado = true;

            telaVitoria.SetActive(true);

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("VOCE VENCEU!");
        }
        else
        {
            Debug.Log("Voce ainda precisa coletar todas as baterias!");
        }
    }
}
