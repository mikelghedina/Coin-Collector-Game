using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    private float movementSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode runKey;
    

    private bool isJumping;
    float horizInput;
    float vertInput;
    Vector3 forwardMovement;
    Vector3 rightMovement;
    public float runSpeed;
    public float walkSpeed;

    public int damage = 1;
    public ScoreManager scoreManager;
    bool damaged;

    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        

    }
    private void Update()
    {
       
        PlayerMovement();
        
        JumpInput();

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;


    }
    private void PlayerMovement()
    {
        horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        forwardMovement = transform.forward * vertInput;
        rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);
        if (Input.GetKeyDown(runKey))
        {

            movementSpeed = runSpeed;
        }
        if (Input.GetKeyUp(runKey))
        {
            movementSpeed = walkSpeed;
        }

    }
    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            damaged = true;
            
            scoreManager.health -= damage;
            if(scoreManager.health <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
                      
        }
    }
}
