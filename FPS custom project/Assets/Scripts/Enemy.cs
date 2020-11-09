using UnityEngine;
using UnityEngine.AI; // Library for AI

public class Enemy : MonoBehaviour
{
    // Protected keyword means only child classes can have access to variables

    protected enum State { Idle, Running, Attacking, Death }
    protected State animationState;

    [SerializeField] protected float lookRadius = 10f;
    [SerializeField] protected int attackRange = 3; // Range is 3 as our stopping distance is two and we want a melee attack
    [SerializeField] protected float attackRate = 2f; // attackRate is set to the length of the animation time
    
    protected float enemySpeed = 4;

    [SerializeField] private GameObject enemyObject;
    
    protected float nextAttack;
    protected Transform target;
    private NavMeshAgent agent;

    private HealthSystem enemyHealth;
    protected int damage = 5;

    protected float deathTime = 3.633f;

    protected bool isDead = false;

    protected bool singleDeathCheck = false;

    protected bool soundPlayed = false;
    protected string soundName = "PrisonShout";

    #region Properties

    // Enemy and players health as property as when we implement other systems other classes may need access to the GameObject
    public GameObject EnemyObject
    {
        get
        {
            return enemyObject;
        }
        set
        {
            enemyObject = value;
        }
    }

    public HealthSystem EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
        set
        {
            enemyHealth = value;
        }
    }

    #endregion

    // Awake is called when the script gets initialised
    public virtual void Awake()
    {
        enemyHealth = GetComponent<HealthSystem>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        animationState = State.Idle; // Start code with idle animation
        target = PlayerManager.instance.Player.transform; // Reference to PlayerManager. Have to use transform because it's of type GameObject
        agent = GetComponent<NavMeshAgent>(); // Reference to our NavMeshAgent
        GetComponent<NavMeshAgent>().speed = enemySpeed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        AnimationState();

        // Distance from player and enemy
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            
            animationState = State.Running;

            if(soundPlayed == false)
            {
                AudioManager.instance.Play(soundName);
                soundPlayed = true;
            }

            // Call attack method
            Attack();

            if (distance <= agent.stoppingDistance)
            {
                // Face the player
                FaceTarget();
            }
        }
        else
        {
            animationState = State.Idle; // Return to Idle animation if outside of radius
        }

        if (EnemyHealth.CurrentHealth < 0)
        {
            animationState = State.Death;

            if (singleDeathCheck == false)
            {
                singleDeathCheck = true;

                AudioManager.instance.Play("EnemyDying");
                agent.isStopped = true; // Stop the navmesh when the enemy is dead

                // Reference to the EnemyManager object with the enemy gameObject passed in
                FindObjectOfType<EnemyManager>().KilledEnemy(gameObject);

                enemyHealth.Die(deathTime);
                Debug.Log("Enemy dead");
            }
        }
    }

    public virtual void Attack()
    {
        if (Vector3.Distance(target.position, transform.position) < attackRange && isDead == false)
        {
            animationState = State.Attacking; // Attack animation

            if (Time.time > nextAttack)
            {
                // Implement health system and damage
                PlayerManager.instance.PlayerHealth.TakeDamage(damage);
                
                Debug.Log("Player attacked by melee");

                AudioManager.instance.Play("PunchEffect");

                // Setting nextAttack in accordance to attackRate
                nextAttack = Time.time + attackRate;

                if (PlayerManager.instance.PlayerHealth.CurrentHealth <= 0)
                {
                    //Debug.Log("Player dead");

                    // Search for GameManager, similar to GetComponent, but doesn't require a variable
                    FindObjectOfType<GameManager>().EndGame();
                }
            }
        }
    }

    public virtual void FaceTarget()
    {
        // Direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotation to point to target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Update enemy rotation to point to the lookRotation and using Slerp for smoothing
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public virtual void AnimationState()
    {
        switch(animationState)
        {
            case State.Idle:
                {
                    enemyObject.GetComponent<Animation>().Play("Idle");
                    break;
                }
            case State.Running:
                {
                    enemyObject.GetComponent<Animation>().Play("Running");
                    break;
                }
            case State.Attacking:
                {
                    enemyObject.GetComponent<Animation>().Play("Attack");
                    break;
                }
            case State.Death:
                {
                    enemyObject.GetComponent<Animation>().Play("Death");
                    isDead = true;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    // Display radius in editor
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Enemy current position and radius
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}