using System.Collections;
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
    public float distanciaAtaque = 1.1f; 
    [Header("Ataque")]
    public float intervaloDano = 1.5f;
    public float atrasoDano = 0.9f;

    private NavMeshAgent agent;
    private Animator anim;
    private float proximoAtaque;
    private bool atacando = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (jogador == null || gameManager == null)
            return;

        if (gameManager.jogoEncerrado)
        {
            agent.isStopped = true;
            if (anim != null) anim.SetBool("Andando", false);
            return;
        }

        if (!agent.isOnNavMesh)
            return;

        if (atacando)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            if (anim != null) anim.SetBool("Andando", false);
            return;
        }

        float distancia = Vector3.Distance(
            transform.position,
            jogador.position
        );

        if (distancia <= distanciaDeteccao)
        {
            if (distancia <= distanciaAtaque)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                if (anim != null) anim.SetBool("Andando", false);
                Atacar();
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(jogador.position);
                if (anim != null) anim.SetBool("Andando", true);
            }
        }
        else
        {
            agent.isStopped = true;
            if (anim != null) anim.SetBool("Andando", false);
        }
    }

    void Atacar()
    {
        if (Time.time >= proximoAtaque && gameManager.vidas > 0)
        {
            atacando = true;
            if (anim != null) anim.SetTrigger("Atacar");

            proximoAtaque = Time.time + intervaloDano;
            StartCoroutine(AplicarDanoAposAtaque());
        }
    }

    IEnumerator AplicarDanoAposAtaque()
    {
        yield return new WaitForSeconds(atrasoDano);

        if (jogador != null && gameManager != null && !gameManager.jogoEncerrado && gameManager.vidas > 0)
        {
            float distanciaNoImpacto = Vector3.Distance(
                transform.position,
                jogador.position
            );

            if (distanciaNoImpacto <= distanciaAtaque)
            {
                gameManager.ReceberDano();
            }
        }

        atacando = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}