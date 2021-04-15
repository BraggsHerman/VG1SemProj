using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Sarah
{
    public class Chaser : MonoBehaviour
    {
        // Outlets
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private Transform _player;
        private Animator _animator;
        
        // Configuration
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        };
        public float sightRange;
        public float attackVertRange;
        public float attackHorizRange;
        public float wallVertRange;
        public float wallHorizRange;
        private float attackRestTime;
        public float idleSpeed;
        public float chaseSpeed;
        public float damage;
        public float attackDelay;

        // State Tracking
        private Direction direction;
        private bool playerInSightRange;
        private bool playerInAttackRange;
        private Vector2 lastPosition;
        private int stuckCounter;
        private Vector2 dir;
        private bool readyToAttack;
        private int status;

        //Methods
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            int randomDirection = UnityEngine.Random.Range(0, 4);
            switch (randomDirection)
            {
                case 0:
                    direction = Direction.Up;
                    dir = transform.up;
                    break;
                case 1:
                    direction = Direction.Down;
                    dir = -transform.up;
                    break;
                case 2:
                    direction = Direction.Left;
                    dir = -transform.right;
                    break;
                case 3:
                    direction = Direction.Right;
                    dir = transform.right;
                    break;
                default:
                    direction = Direction.Right;
                    dir = transform.right;
                    break;
            }

            _animator = GetComponent<Animator>();
            status = (randomDirection < 2 ? randomDirection : 2);
            _animator.SetInteger("Status", status);
            
            _sprite = GetComponent<SpriteRenderer>();
            _player = FindObjectOfType<PlayerController>().transform;
            lastPosition = transform.position;
            stuckCounter = 0;
            readyToAttack = true;
        }
        
        void Update()
        {
            if (!CheckForPlayerInRange())
            {
                RaycastHit2D[] wallHitList = { };
            
                if (direction == Direction.Left || direction == Direction.Right)
                {
                    wallHitList = Physics2D.RaycastAll(transform.position, dir, wallHorizRange);
                    Debug.DrawRay(transform.position, dir * wallHorizRange, Color.blue);
                }
                else
                {
                    wallHitList = Physics2D.RaycastAll(transform.position, dir, wallVertRange);
                    Debug.DrawRay(transform.position, dir * wallVertRange, Color.blue);
                }

                foreach (RaycastHit2D wallHit in wallHitList)
                {
                    if (wallHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall") || (wallHit.collider.gameObject != this.gameObject && wallHit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")))
                    {
                        ChangeDirection();
                    }
                    /*
                    if (wallHit.collider != null)
                    {
                        print("change dir");
                        ChangeDirection();
                    }*/
                    
                }
                
                IdleWalk();
            }
            CheckIfStuck();
        }

        void ChasePlayer()
        {
            Vector2 playerPos = _player.position;
            Vector2 enemyPos = transform.position;
            
            _sprite.flipX = playerPos.x < enemyPos.x;
            
            _rigidbody.AddForce((playerPos - enemyPos) * chaseSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        void AttackPlayer()
        {
            // Attack player
            PlayerController player;
            if (player = _player.gameObject.GetComponent<PlayerController>())
            {
                player.TakeDamage(damage);
                readyToAttack = false;
                StartCoroutine("AttackTimer");
                ChangeDirection();
            }
        }
        
        IEnumerator AttackTimer()
        {
            yield return new WaitForSeconds(attackDelay);

            readyToAttack = true;
        }

        void IdleWalk()
        {
            _sprite.flipX = direction == Direction.Left; 
            _rigidbody.AddForce(dir * idleSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        void ChangeDirection()
        {
            
            RaycastHit2D[] wallAboveList = Physics2D.RaycastAll(transform.position, transform.up, wallVertRange);
            RaycastHit2D[] wallBelowList = Physics2D.RaycastAll(transform.position, -transform.up, wallVertRange);
            RaycastHit2D[] wallLeftList = Physics2D.RaycastAll(transform.position, -transform.right, wallHorizRange);
            RaycastHit2D[] wallRightList = Physics2D.RaycastAll(transform.position, transform.right, wallHorizRange);
            
            // Checking if there's a wall in any direction
            bool freeAbove = true;
            bool freeBelow = true;
            bool freeLeft = true;
            bool freeRight = true;
            foreach (RaycastHit2D above in wallAboveList)
            {
                if (above.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    freeAbove = false;
                }
                else if (above.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (above.collider.gameObject != this.gameObject)
                    {
                        freeBelow = false;
                        break;
                    }
                }
            }
            foreach (RaycastHit2D below in wallBelowList)
            {
                if (below.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    freeBelow = false;
                    break;
                }
                else if (below.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (below.collider.gameObject != this.gameObject)
                    {
                        freeBelow = false;
                        break;
                    }
                }
            }
            foreach (RaycastHit2D left in wallLeftList)
            {
                if (left.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    freeLeft = false;
                }
                else if (left.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (left.collider.gameObject != this.gameObject)
                    {
                        freeBelow = false;
                        break;
                    }
                }
            }
            foreach (RaycastHit2D right in wallRightList)
            {
                if (right.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    freeRight = false;
                }
                else if (right.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (right.collider.gameObject != this.gameObject)
                    {
                        freeBelow = false;
                        break;
                    }
                }
            }
            
            bool validPath = false;
            int checksLeft = 10;
            while (!validPath && checksLeft > 0)
            {
                --checksLeft;
                int choice = UnityEngine.Random.Range(0, 4);
                switch (choice)
                {
                    case 0:
                        validPath = freeAbove;
                        direction = Direction.Up;
                        dir = transform.up;
                        break;
                    case 1:
                        validPath = freeBelow;
                        direction = Direction.Down;
                        dir = -transform.up;
                        break;
                    case 2:
                        validPath = freeLeft;
                        direction = Direction.Left;
                        dir = -transform.right;
                        break;
                    case 3:
                        validPath = freeRight;
                        direction = Direction.Right;
                        dir = transform.right;
                        break;
                }
                status = (choice < 2 ? choice : 2);
            }
            _animator.SetInteger("Status", status);
        }

        void CheckIfStuck()
        {
            if (!playerInAttackRange && (lastPosition.x.CompareTo(transform.position.x) == 0) && (lastPosition.y.CompareTo(transform.position.y) == 0))
            {
                stuckCounter++;
                if (stuckCounter > 20)
                {
                    lastPosition = transform.position;
                    stuckCounter = 0;
                    ChangeDirection();
                }
            }
            else
            {
                lastPosition = transform.position;
                stuckCounter = 0;
            }
        }

        bool CheckForPlayerInRange()
        {
            // Check if player is in sight or attack range
            playerInSightRange = false;
            Vector2 diagonalDir1 = transform.up;
            Vector2 diagonalDir2 = transform.up;
            Vector2 diagonalDir3 = transform.up;
            Vector2 diagonalDir4 = transform.up;
            switch (direction)
            {
                case Direction.Up:
                    diagonalDir1 = new Vector2(-1, 2);
                    diagonalDir2 = new Vector2(1, 2);
                    diagonalDir3 = new Vector2(-1, 4);
                    diagonalDir4 = new Vector2(1, 4);
                    break;
                case Direction.Down:
                    diagonalDir1 = new Vector2(-1, -2);
                    diagonalDir2 = new Vector2(1, -2);
                    diagonalDir3 = new Vector2(-1, -4);
                    diagonalDir4 = new Vector2(1, -4);
                    break;
                case Direction.Left:
                    diagonalDir1 = new Vector2(-2, 1);
                    diagonalDir2 = new Vector2(-2, -1);
                    diagonalDir3 = new Vector2(-4, 1);
                    diagonalDir4 = new Vector2(-4, -1);
                    break;
                case Direction.Right:
                    diagonalDir1 = new Vector2(2, 1);
                    diagonalDir2 = new Vector2(2, -1);
                    diagonalDir3 = new Vector2(4, 1);
                    diagonalDir4 = new Vector2(4, -1);
                    break;
            }
            
            RaycastHit2D[] sightHitList = Physics2D.RaycastAll(transform.position, dir, sightRange);
            RaycastHit2D[] sightHitList2 = Physics2D.RaycastAll(transform.position, diagonalDir1, sightRange);
            RaycastHit2D[] sightHitList3 = Physics2D.RaycastAll(transform.position, diagonalDir2, sightRange);
            RaycastHit2D[] sightHitList4 = Physics2D.RaycastAll(transform.position, diagonalDir2, sightRange);
            RaycastHit2D[] sightHitList5 = Physics2D.RaycastAll(transform.position, diagonalDir2, sightRange);
            Debug.DrawRay(transform.position, dir * sightRange, Color.green);
            Debug.DrawRay(transform.position, diagonalDir1 * sightRange/2, Color.green);
            Debug.DrawRay(transform.position, diagonalDir2 * sightRange/2, Color.green);
            Debug.DrawRay(transform.position, diagonalDir3 * sightRange/4, Color.green);
            Debug.DrawRay(transform.position, diagonalDir4 * sightRange/4, Color.green);

            foreach (RaycastHit2D sightHit in sightHitList)
            {
                if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInSightRange = true;
                }
                else if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }
            }
            foreach (RaycastHit2D sightHit in sightHitList2)
            {
                if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInSightRange = true;
                }
                else if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }
            }
            foreach (RaycastHit2D sightHit in sightHitList3)
            {
                if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInSightRange = true;
                }
                else if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }
            }
            foreach (RaycastHit2D sightHit in sightHitList4)
            {
                if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInSightRange = true;
                }
                else if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }
            }

            foreach (RaycastHit2D sightHit in sightHitList5)
            {
                if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInSightRange = true;
                }
                else if (sightHit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }
            }

            if (playerInSightRange)
            {
                ChasePlayer();
                return true;
            }

            return false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && readyToAttack)
            {
                playerInAttackRange = true;
                AttackPlayer();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                playerInAttackRange = false;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && readyToAttack)
            {
                AttackPlayer();
            }
        }
    }
}

