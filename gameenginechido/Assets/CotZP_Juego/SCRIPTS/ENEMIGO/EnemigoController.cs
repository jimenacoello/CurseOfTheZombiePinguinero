using UnityEngine;
using UnityEngine.AI;

public class EnemigoController : MonoBehaviour
{
    private Vector3 enPosicion;
    private NavMeshAgent agent;
    private Transform personaje;

    private bool detect;

    [Header("Detección")]
    [SerializeField] private float radio;
    [SerializeField] private LayerMask mask;

    [Header("Animación")]
    [SerializeField] private Animator animator; 
    private readonly int walkingHash = Animator.StringToHash("Walking");

    private bool estaPersiguiendo = false;

    private void Start()
    {
        enPosicion = transform.position;
        personaje = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            animator = GetComponent<Animator>(); 
        }
    }

    private void Update()
    {
        detect = Physics.CheckSphere(transform.position, radio, mask);

        if (detect)
        {
            agent.SetDestination(personaje.position);
            agent.stoppingDistance = 2;

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

        if (animator != null)
        {
            animator.SetBool(walkingHash, seMueve);
        }

        AudioManager.Instance.PlayEnemyFootsteps(seMueve);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
