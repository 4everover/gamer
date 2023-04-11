using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Both
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float attackRadius;
    [SerializeField] float rotateToEnemyRadius;
    [SerializeField] float pickUpItemRadius;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask pickupsLayer;

    [SerializeField] GameObject pickUpItemUIElement;

    [SerializeField] float attackDamage = 15;

    bool useRigidbodyMovement = false;

    bool isAttacking = false;

    bool canRotateTowardsEnemy = false;

    Animator animator;
    readonly string IS_WALKING = "isWalking";
    readonly string ATTACK = "attack";
    readonly string SPIN_ATTACK = "spinAttack";

    float originalMovementSpeed;

    // Rigidbody
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    

    // Character Controller
    [Header("Character Controller Stuff")]
    CharacterController controller;
    Vector3 charControlVelocity;
    [SerializeField] Transform cameraPos;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float turnSmoothTimeForAttack = 0.025f;
    [SerializeField] float gravityVal = Physics.gravity.y; // default value: -9.81
    [SerializeField] float onGroundRadius = 0.4f;

    bool isOnGround;
    float turnSmoothVelocity;

    PlayerSaveProfile playerSaveProfile;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rotateToEnemyRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpItemRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerSaveProfile == null) playerSaveProfile = new PlayerSaveProfile();

        pickUpItemUIElement.SetActive(false);

        originalMovementSpeed = movementSpeed;

        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        charControlVelocity.y = -2;

        if (!cameraPos)
        {
            cameraPos = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (useRigidbodyMovement) 
        {
            //if (controller) Destroy(controller);   
            //RigidbodyMovement(); 
        }
        else 
        {
            //if (rb) Destroy(rb);
            //if (capsuleCollider) Destroy(capsuleCollider);
            CharacterControllerMovement();
            if (canRotateTowardsEnemy) { LookAtEnemy(); }
        }
        HandleAttacking();
        ToggleCursorLock();
        ManageItemPickups();

    }


    void RigidbodyMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }
    }


    void CharacterControllerMovement()
    {
        CCHandleGravity();
        CCMove();
        CCJump();
    }

    void CCMove()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal"); //side to side movement
        float verticalMovement = Input.GetAxisRaw("Vertical"); //back and forth movement
        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool(IS_WALKING, true);
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraPos.eulerAngles.y;
            float directionSmoothing = Mathf.SmoothDampAngle(transform.eulerAngles.y, directionAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (!canRotateTowardsEnemy) transform.rotation = Quaternion.Euler(0, directionSmoothing, 0);
            Vector3 moveDirection = Quaternion.Euler(0, directionAngle, 0) * Vector3.forward;

            controller.Move(movementSpeed * moveDirection.normalized * Time.deltaTime);

            //isMoving = true;
        }

        //if (Mathf.Abs(horizontalMovement) >= 0.1f  || Mathf.Abs(verticalMovement) >= 0.1f) { isMoving = true; }
        else 
        {
            //isMoving = false; 
            animator.SetBool(IS_WALKING, false);
        }
    }
    void CCJump()
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");
        isOnGround = Physics.CheckSphere(transform.position, onGroundRadius, groundMask);
        if (isOnGround && charControlVelocity.y < 0)
        {
            charControlVelocity.y = -2;
        }

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            //playerAnimation.Jump();
            charControlVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityVal);
        }

        //Debug.Log("on ground: " + isOnGround);
    }
    void CCHandleGravity()
    {
        charControlVelocity.y += gravityVal * Time.deltaTime;
        controller.Move(charControlVelocity * Time.deltaTime);
    }

    void HandleAttacking()
    {
        if (!isAttacking)
        {
            if (Input.GetButtonDown("Fire1")) animator.SetTrigger(ATTACK);
            else if (Input.GetButtonDown("Fire2")) animator.SetTrigger(SPIN_ATTACK);
        }
        else
        {
            LookAtEnemy();
        }
    }

    void Attack() // animation event
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayer);
        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                //Debug.Log("Hit " + enemy.name);
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    void SpinAttack(int dmgToDeal) // animation event
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayer);
        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                //Debug.Log("Hit " + enemy.name);
                enemy.GetComponent<Health>().TakeDamage(dmgToDeal);
            }
        }
    }

    void ManageItemPickups()
    {
        Collider[] pickupsInRange = Physics.OverlapSphere(transform.position, pickUpItemRadius, pickupsLayer);
        if (pickupsInRange.Length > 0)
        {
            pickUpItemUIElement.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (var item in pickupsInRange)
                {
                    item.GetComponent<PickableItem>().PickUpItem();
                }
            }
        }
        else pickUpItemUIElement.SetActive(false);
    }

    void LookAtEnemy()
    {
        Collider[] enemiesInRadius = Physics.OverlapSphere(transform.position, rotateToEnemyRadius, enemyLayer);
        if (enemiesInRadius.Length < 1) canRotateTowardsEnemy = false;
        else
        {
            canRotateTowardsEnemy = true;
            var closestEnemy = enemiesInRadius[0];
            foreach (var enemy in enemiesInRadius)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }

            //Debug.Log("Closest enemy: " + closestEnemy.name);

            //float horizontalMovement = Input.GetAxisRaw("Horizontal"); //side to side movement
            //float verticalMovement = Input.GetAxisRaw("Vertical"); //back and forth movement
            Vector3 direction = new Vector3(closestEnemy.transform.position.x - transform.position.x, 0, closestEnemy.transform.position.z - transform.position.z).normalized; // change this

            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // change this, cameraPos.eulerAngles.y
            //float directionAngle = Mathf.Atan2(closestEnemy.transform.position.x, closestEnemy.transform.position.z) * Mathf.Rad2Deg;
            float directionSmoothing = Mathf.SmoothDampAngle(transform.eulerAngles.y, directionAngle, ref turnSmoothVelocity, turnSmoothTimeForAttack);

            transform.rotation = Quaternion.Euler(0, directionSmoothing, 0);
        }

    }
    void SetIsAttacking(int i) // animation event
    {
        if (i == 1) { 
            isAttacking = true;
            //SetCanLookAtEnemy();
        }
        else { 
            isAttacking = false;
            canRotateTowardsEnemy = false;
        }
    }

    void ToggleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void SetMovementSpeedMultiplier(float mp) // animation event
    {
        movementSpeed *= mp;
    }
    void ResetMovementSpeed() // animation event
    {
        movementSpeed = originalMovementSpeed;
    }

    public PlayerSaveProfile SavePlayer()
    {

        playerSaveProfile.currentHealth = GetComponent<Health>().GetHealth();
        playerSaveProfile.currentPositionX = gameObject.transform.position.x;
        playerSaveProfile.currentPositionY = gameObject.transform.position.y;
        playerSaveProfile.currentPositionZ = gameObject.transform.position.z;

        //Debug.Log(playerSaveProfile.currentPositionX);
        //Debug.Log(playerSaveProfile.currentPositionY);
        //Debug.Log(playerSaveProfile.currentPositionZ);

        return playerSaveProfile;

    }

    public void LoadPlayer(SaveData saveData)
    {
        playerSaveProfile = saveData.playerSaveProfile;

        transform.position = new Vector3(playerSaveProfile.currentPositionX, playerSaveProfile.currentPositionY, playerSaveProfile.currentPositionZ);

        //Debug.Log(playerSaveProfile.currentPositionX);
        //Debug.Log(playerSaveProfile.currentPositionY);
        //Debug.Log(playerSaveProfile.currentPositionZ);

        GetComponent<Health>().SetHealth(playerSaveProfile.currentHealth);
    }
}
