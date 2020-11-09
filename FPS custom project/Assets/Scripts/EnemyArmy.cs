using UnityEngine;

public class EnemyArmy : Enemy
{
    private float timeBtwShots;
    private float startTimeBtwShots = 0.3f;

    private Bullet enemyBullet;

    // Awake is called when the script gets initialised
    public override void Awake()
    {
        enemyBullet = GetComponent<Bullet>(); // Reference bullet class
        base.Awake();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        enemySpeed = 1;
        attackRange = 8;
        timeBtwShots = startTimeBtwShots;
        damage = 10;
        deathTime = 3.8f;
        soundName = "ArmyShout";
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (Vector3.Distance(target.position, transform.position) < attackRange && isDead == false)
        {
            animationState = State.Attacking; // Attack animation

            if(timeBtwShots <= 0)
            {
                // Debug message
                Debug.Log("Player attacked at range");

                // Implement health system and damage
                PlayerManager.instance.PlayerHealth.TakeDamage(damage);

                AudioManager.instance.Play("EnemyShooting");

                // Creating the bullet
                enemyBullet.CreateBullet();

                // Pushing the bullet prefab in a direction
                enemyBullet.TempRigidBody.AddForce(transform.forward * enemyBullet.BulletFowardForce);

                timeBtwShots = startTimeBtwShots;

                if (PlayerManager.instance.PlayerHealth.CurrentHealth <= 0)
                {
                    //Debug.Log("Player dead");
                    FindObjectOfType<GameManager>().EndGame();
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime; // Slowly decrease the time, in our case its 0.3
            }
        }
    }

    public override void AnimationState()
    {
        switch (animationState)
        {
            case State.Idle:
                {
                    EnemyObject.GetComponent<Animation>().Play("Idle"); //accessing the EnemyObject property
                    break;
                }
            case State.Running:
                {
                    EnemyObject.GetComponent<Animation>().Play("Firing");
                    break;
                }
            case State.Death:
                {
                    EnemyObject.GetComponent<Animation>().Play("Death");
                    isDead = true;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public override void FaceTarget()
    {
        base.FaceTarget();
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}