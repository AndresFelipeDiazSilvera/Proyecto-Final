using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    [SerializeField] int pointLife;
    [SerializeField] int poitDamage;
    private GameObject target;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    public void takeDamage()
    {

    }
    public void AttackTarget()
    {

    }
}
