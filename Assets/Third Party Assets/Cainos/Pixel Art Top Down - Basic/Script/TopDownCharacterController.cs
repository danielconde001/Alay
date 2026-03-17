using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        private State state;
        private enum State
        {
            Normal,
            Dodging,
            Attacking,
            Dead
        }

        [SerializeField] private float speed = 3f;
        [SerializeField] private float dodgeSpeed = 75f;
        [SerializeField] private float dodgeSpeedDropMultiplier = 5f;
        [SerializeField] private float dodgeSpeedMinimum = 50f;
        [SerializeField] private int minDamage = 15;
        public int MinDamage
        {
            get => minDamage;
            set => minDamage = value;
        }

        [SerializeField] private int maxDamage = 25;
        public int MaxDamage
        {
            get => maxDamage;
            set => maxDamage = value;
        }

        [SerializeField] private float projectileSpeed;
        public float ProjectileSpeed
        {
            get => projectileSpeed;
            set => projectileSpeed = value;
        }

        [SerializeField] private float yOffset;
        [SerializeField] private float fireRate = 0.5f;
        public float FireRate 
        {
            get => fireRate;
            set => fireRate = value;
        }

        private float fireRateCounter = 0;

        [SerializeField] private float projectileSpread;
        public float ProjectileSpread
        {
            get => projectileSpread;
            set => projectileSpread = value;
        }

        [SerializeField] private int numberOfProjectilesInSpread;
        public int NumberOfProjectilesInSpread
        {
            get => numberOfProjectilesInSpread;
            set => numberOfProjectilesInSpread = value;
        }

        [SerializeField] private bool canMove = true;
        public bool CanMove 
        {
            get => canMove;
            set => canMove = value; 
        }
        [SerializeField] private bool canDodge = true;
        public bool CanDodge
        {
            get => canDodge;
            set => canDodge = value;
        }

        [SerializeField] private bool canShoot = true;
        [SerializeField] private bool canSpread = false;
        public bool CanSpread
        {
            set => canSpread = value;
        }

        [SerializeField] private bool hasProjectileBuff = false; // Dirty as fuck.. but you're out of time!
        public bool HasProjectileBuff
        {
            set => hasProjectileBuff = value;
        }

        private PlayerDamageable playerDamageable;
        public PlayerDamageable PlayerDamageable
        {
            get => playerDamageable;
        }

        [SerializeField] private ProjectileBehaviour projectilePrefab;

        /*[SerializeField] private float dropRate;
        float dropRateCounter = 0;

        [SerializeField] private GameObject dropOnDodge;
        public GameObject DropOnDodge
        {
            set => dropOnDodge = value;
        }

        [SerializeField] private bool canDropOnDodge;
        public bool CanDropOnDodge
        {
            set => canDropOnDodge = value;
        }*/

        // TODO: Player Dodge component
        private float currentDodgeSpeed;
        private Vector2 direction;
        private Vector2 dodgeDirection;
        private Rigidbody2D rigidbody2d;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private PlayerStamina playerStamina;
        [SerializeField] private float dodgeStaminaCost;
        [SerializeField] private TrailRenderer dodgeTrail;
        private Vector2 mousePosition;

        [SerializeField] private UnityEvent OnDodgeEvent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerStamina = GetComponent<PlayerStamina>();
            playerDamageable = GetComponent<PlayerDamageable>();
            state = State.Normal;
        }

        private void Update()
        {
            if (!canMove)
            {
                return;
            }
            switch (state)
            {
                case State.Normal:
                    HandleMovement();
                    HandleDodging();
                    HandleAttacking();
                    break;
                case State.Dodging:
                    playerDamageable.Invincible = true;
                    currentDodgeSpeed -= currentDodgeSpeed * dodgeSpeedDropMultiplier * Time.deltaTime;
                    dodgeTrail.emitting = true;
                    //dropRateCounter -= Time.deltaTime;
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Collision Blocker"), true);
                    /*if (canDropOnDodge)
                    {
                        if (dropRateCounter <= 0)
                        {
                            Instantiate(dropOnDodge, transform.position, Quaternion.identity);
                            dropRateCounter = dropRate;
                        }
                    }*/
                    if (currentDodgeSpeed < dodgeSpeedMinimum)
                    {
                        //dropRateCounter = dropRate;
                        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
                        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Collision Blocker"), false);
                        playerDamageable.Invincible = false;
                        dodgeTrail.emitting = false;
                        state = State.Normal;
                    }
                    break;
                case State.Attacking:
                    HandleAttacking();
                    break;
                case State.Dead:
                    break;
            }
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case State.Normal:
                    GetComponent<Rigidbody2D>().linearVelocity = speed * direction;
                    break;
                case State.Dodging:
                    GetComponent<Rigidbody2D>().linearVelocity = dodgeSpeed * dodgeDirection;
                    break;
                case State.Attacking:
                    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                    break;
            }
            
        }

        private void HandleMovement()
        {
            direction = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction.y = -1;
                animator.SetInteger("Direction", 0);
            }

            direction.Normalize();
            animator.SetBool("IsMoving", direction.magnitude > 0);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }

        private void HandleDodging()
        {
            if (!canDodge)
            {
                return;
            }
            if (Input.GetButtonDown("Dodge") && playerStamina.CurrentStaminaPoints >= dodgeStaminaCost)
            {
                AudioManager.Instance.PlaySFX("Dash");
                animator.SetTrigger("IsDodging");
                dodgeDirection = direction;
                currentDodgeSpeed = dodgeSpeed;
                playerStamina.DeductStamina(dodgeStaminaCost);
                OnDodgeEvent.Invoke();
                state = State.Dodging;
            }
        }

        private void HandleAttacking()
        {
            if (fireRateCounter > 0)
            {
                fireRateCounter -= Time.deltaTime;
            }

            canShoot = fireRateCounter > 0 ? false : true;

            if (canShoot)
            {
                if (Input.GetButton("Attack"))
                {
                    fireRateCounter = fireRate;

                    if (canSpread)
                    {
                        Vector2 yOffsetProjectileSpawn = new Vector2(0, yOffset);
                        Vector2 spawnPoint = (Vector2)transform.position + yOffsetProjectileSpawn;
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePosition = (mousePos - spawnPoint).normalized;

                        float facingRotation = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
                        float startRotation = facingRotation + projectileSpread / 2f;
                        float angleIncrease = projectileSpread / ((float)numberOfProjectilesInSpread - 1f);
                        for (int i = 0; i < numberOfProjectilesInSpread; i++)
                        {
                            float tempRot = startRotation - angleIncrease * i;

                            ProjectileBehaviour projectile =
                                Instantiate(projectilePrefab, spawnPoint, Quaternion.identity).GetComponent<ProjectileBehaviour>();

                            Vector2 shootDirection = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));

                            int damage = Random.Range(minDamage, maxDamage + 1);

                            if (!hasProjectileBuff)
                            {
                                projectile.SetupProjectile(shootDirection, damage, projectileSpeed, 1.5f);
                            }
                            else
                            {
                                projectile.SetupProjectile(shootDirection, damage, projectileSpeed, 1.5f, true);
                            }
                        }

                        AudioManager.Instance.PlaySFX("Bullet " + Random.Range(1, 3));
                    }
                    else
                    {
                        Vector2 yOffsetProjectileSpawn = new Vector2(0, yOffset);
                        Vector2 spawnPoint = (Vector2)transform.position + yOffsetProjectileSpawn;

                        ProjectileBehaviour projectile =
                        Instantiate(projectilePrefab, spawnPoint, Quaternion.identity).GetComponent<ProjectileBehaviour>();

                        int damage = Random.Range(minDamage, maxDamage + 1);

                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePosition = (mousePos - spawnPoint).normalized;

                        if (!hasProjectileBuff)
                        {
                            projectile.SetupProjectile(mousePosition, damage, projectileSpeed, 1.5f);
                        }
                        else
                        {
                            projectile.SetupProjectile(mousePosition, damage, projectileSpeed, 1.5f, true);
                        }

                        AudioManager.Instance.PlaySFX("Bullet " + Random.Range(1, 3));
                    }
                }
            }
        }

        public void OnDeath()
        {
            state = State.Dead;
            direction = Vector2.zero;
            animator.SetTrigger("IsDead");
            GameManager.Instance.RestartRun();
        }
    }
}
