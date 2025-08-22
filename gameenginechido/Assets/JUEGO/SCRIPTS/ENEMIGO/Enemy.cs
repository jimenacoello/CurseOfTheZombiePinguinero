using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Vector3 enPosicion;
    private NavMeshAgent agent;
    private Transform personaje;

    private bool detect;

    [SerializeField] private float radio;
    [SerializeField] private LayerMask mask;

    private bool estaPersiguiendo = false;

    private void Start()
    {
        enPosicion = transform.position;
        personaje = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        detect = Physics.CheckSphere(transform.position, radio, mask);

        if (detect)
        {
            agent.SetDestination(personaje.position);
            agent.stoppingDistance = 3;

            if (!estaPersiguiendo)
            {
                AudioManager.Instance.PlayChaseMusic();
                estaPersiguiendo = true;
            }
        }
        else
        {
            agent.SetDestination(enPosicion);
            agent.stoppingDistance = 0;

            if (estaPersiguiendo)
            {
                AudioManager.Instance.PlayAmbient();
                estaPersiguiendo = false;
            }
        }

        bool seMueve = agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance;
        AudioManager.Instance.PlayEnemyFootsteps(seMueve);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
