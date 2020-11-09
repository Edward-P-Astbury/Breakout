public class EnemySwat : Enemy
{

    // Awake is called when the script gets initialised
    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        enemySpeed = 1;
        damage = 8;
        deathTime = 3.9f;
        attackRate = 1.667f;
        soundName = "SwatShout";
        base.Start(); // Calling the base class implementation
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void AnimationState()
    {
        base.AnimationState();
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