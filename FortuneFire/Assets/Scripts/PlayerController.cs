using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject inventory;
    public float freezeTime;

    [Header("Physics")]
    private float groundDrag = 5f;
    private float airDrag = 0f;
    private bool grounded = false;
    [SerializeField]
    private float speed;
    [SerializeField]
    public float maxSpeed = 10f;

    [Header("Grabbing Stuff")]
    private bool currentlyHolding = false;
    public Transform attach;
    private float useCooldown = 1f;
    private bool canUse;

    [Header("Attack and Stun")]
    public bool isInvulnerable = false;
    private float invulnerabilityTime = 2f;
    private bool canMove = true;

    [Header("Team")]
    [SerializeField] public int team = 1;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    public Transform orientation;

    private Vector2 movementInput = Vector2.zero;
    private bool pickup = false;
    private bool putdown = false;
    
    [SerializeField]
    private GameObject[] swordArr;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //orientation = GetComponent<Transform>();
        canMove = false;
        canUse = true;
        rb.freezeRotation = true;
        Invoke(nameof(UnFreeze), freezeTime); //19.5 seconds for correct interval
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 2f * 0.5f + 0.2f))
        { //Checks to see if player is on ground
            rb.drag = airDrag;
        }
        else
        {
            rb.drag = groundDrag;
        }

        if (rb.velocity.magnitude > maxSpeed) //Limits the max speed the player can move
        {
            Vector3 limitVel = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }

        if(inventory != null) //Attaches held object to player
        {
            inventory.transform.position = attach.position;
        }

        
    }

    private void FixedUpdate()
    {
        Move(); //Player Movement and Rotation
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        pickup = context.action.triggered;
        if(context.action.WasReleasedThisFrame())
        {
            canUse = true;

        }

    }

    public void OnPutDown(InputAction.CallbackContext context)
    {
        putdown = context.action.triggered;
    }

    private IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(.5f);
        canUse = true;
    }

    private IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(.5f);
        canUse = false;
    }

    private void Move()
    {
        if(canMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
            //rb.velocity += moveDirection * speed * 0.1f; Old System
            rb.AddForce(moveDirection * speed, ForceMode.Force);

            if (movementInput.y > 0 && Mathf.Abs(movementInput.y) > Mathf.Abs(movementInput.x)) //Player orientation
            {
                orientation.rotation = Quaternion.Euler(0, -90, 0); //Right
            }
            else if (movementInput.y < 0 && Mathf.Abs(movementInput.y) > Mathf.Abs(movementInput.x))
            {
                orientation.rotation = Quaternion.Euler(0, 90, 0); //Left
            }
            if (movementInput.x > 0 && Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            {
                orientation.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movementInput.x < 0 && Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            {
                orientation.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        
    }

    private void OnTriggerStay(Collider other) //Checks and determines what object a player is interacting with
    {

        if(other.TryGetComponent<Receptical>(out Receptical receptical)) //Receptical
        {
            //Put object down in receptical
            if(currentlyHolding && pickup && canUse)
            {
                if (receptical.recepticalInventory == null) {
                    inventory.GetComponent<ItemAttributes>().inInventory = true;

                    receptical.canGrab = true;
                    Rigidbody inventoryRb = inventory.GetComponent<Rigidbody>();
                    inventoryRb.freezeRotation = true;

                    if(receptical.PutDown(inventory))
                    {
                        inventory = null;
                        inventoryRb = null;
                        currentlyHolding = false;
                    }
                    canUse = false;
                } else if (inventory.GetComponent<ItemAttributes>().swordState == SwordState.HANDLE && receptical.recepticalInventory.GetComponent<ItemAttributes>().swordState == SwordState.FINISHED) {
                    //Logic for combining a handle to a full sword
                    Destroy(inventory);
                    currentlyHolding = false;
                    GameObject blade = receptical.recepticalInventory;
                    blade.GetComponent<ItemAttributes>().handleType = HandleType.METAL;
                    GameObject fullSword = Instantiate(swordArr[blade.GetComponent<ItemAttributes>().metal], receptical.attachPoint.transform.position, Quaternion.identity);
                    fullSword.GetComponent<ItemAttributes>().sharpenNum = blade.GetComponent<ItemAttributes>().sharpenNum;
                    Destroy(receptical.recepticalInventory);
                    receptical.recepticalInventory = fullSword;
                } 

            }

            //Pick object up from receptical
            if (!currentlyHolding && pickup && canUse && receptical.recepticalInventory != null && receptical.canGrab)
            {
                if(receptical.GetComponentInChildren<ProgressBar>()) {
                    receptical.GetComponentInChildren<ProgressBar>().progressBar.fillAmount = 0f;
                }

                inventory = receptical.recepticalInventory;
                inventory.GetComponent<ItemAttributes>().inInventory = false;
                receptical.recepticalInventory = null; 
                receptical.canGrab = false;
                currentlyHolding = true;
                canUse = false;
            }
        }

        //Pick object up in general
        if(other.TryGetComponent<IGrabable>(out IGrabable attachPos)) //Grabable objects
        {

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            bool inInventory = other.gameObject.GetComponent<ItemAttributes>().inInventory;
            
            if(!currentlyHolding && pickup && !inInventory) {
                attachPos.PickUp(attach);
                inventory = other.gameObject;

                other.gameObject.transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll;

                currentlyHolding = true;
            }
        }

        if(other.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            if(!currentlyHolding && canUse && pickup && pc.isInvulnerable == false)
            {
                try
                {
                    Rigidbody rb = pc.inventory.GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.None;
                } catch
                {

                }
                
                
                pc.TakeDamage(orientation.right, 2f);
            }
        }


    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            try {
                Rigidbody rb = inventory.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
            } catch {}
            TakeDamage(orientation.right, 1f);
        }
    }

    public void TakeDamage(Vector3 hitDirection, float _stunTime = 2f)
    {
        currentlyHolding = false;
        if(inventory != null) {
            inventory.GetComponent<ItemAttributes>().inInventory = false;
        }
        inventory = null;
        rb.AddForce(hitDirection * 50, ForceMode.Impulse);
        StartCoroutine(Stun(_stunTime));
    }

    private IEnumerator Stun(float stunDuration)
    {
        canMove = false;
        isInvulnerable = true;
        yield return new WaitForSeconds(stunDuration);
        canMove = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;

    }

    public void Freeze()
    {
        canMove = false;
    }

    public void UnFreeze()
    {
        canMove = true;
    }
}
