
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("Alvo")]
    public Transform jogador;
    public GameManager gameManager;

    [Header("Percepcao")]
    public float distanciaDeteccao = 15f;
    public float distanciaAtaque = 1.5f;

    [Header("Ataque")]
    public float intervaloDano = 1.5f;

    private NavMeshAgent agent;
    private float proximoAtaque;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jogador == null || gameManager == null)
            return;

        if (gameManager.jogoEncerrado)
        {
            agent.isStopped = true;
            return;
        }

        if (!agent.isOnNavMesh)
            return;

        float distancia = Vector3.Distance(
            transform.position,
            jogador.position
        );

        if (distancia <= distanciaDeteccao)
        {
            agent.isStopped = false;
            agent.SetDestination(jogador.position);

            if (distancia <= distanciaAtaque)
            {
                agent.isStopped = true;
                Atacar();
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Atacar()
    {
        if (Time.time >= proximoAtaque && gameManager.vidas > 0)
        {
            gameManager.ReceberDano();

            proximoAtaque = Time.time + intervaloDano;
        }
    }
}
