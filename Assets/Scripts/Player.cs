using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float forceJump;
    public Transform position;
    public Transform testFloor;
    public LayerMask maskFloor;
    private bool isWalking;
    private bool isFloor;
    private float radio;
    private bool canJumpTwice;
    private int speed;
    private float factor;
    private Vector3 initialPosition;
    public Text txtScore;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        factor = 1;
        speed = 3;
        position = GetComponent<Transform>();
        initialPosition = position.position;
        canJumpTwice = true;
        radio = 0.07f;
        forceJump = 500f;
        isFloor = true;
        isWalking = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (position.position.y < -15)
        {
            SceneManager.LoadScene("Lose");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            factor = 1.7f;
        }
        else
        {
            factor = 1;
        }
        if (Input.GetKeyDown(KeyCode.W) && (isFloor || canJumpTwice))
        {
            rb.AddForce(new Vector2(0, forceJump));
            if(!isFloor){
                canJumpTwice = false;
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalking", true);
           
                position.Translate(new Vector3(speed * factor, 0, 0) * Time.deltaTime);
            
            
            
        }else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.A) && position.eulerAngles.y!=180){
            position.Rotate(new Vector3(0, 180, 0));
        }
        if (Input.GetKeyDown(KeyCode.D) && position.eulerAngles.y != 0)
        {
            position.Rotate(new Vector3(0, -180, 0));
        }



    }


    private void FixedUpdate()
    {
        isFloor = Physics2D.OverlapCircle(testFloor.position, radio, maskFloor);
        animator.SetBool("isJump", !isFloor);
        if (isFloor)
        {
            canJumpTwice = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "move")
        {
            transform.parent = collision.transform;
        }

        

        if (collision.transform.name == "side")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Lose");
            return;
        }
        if (collision.transform.name == "top")
        {
            Destroy(collision.transform.parent.parent.gameObject);
            return;
        }

        if (collision.transform.tag == "door")
        {
            SceneManager.LoadScene("Win");
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "move")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "gem")
        {
            Destroy(collision.gameObject);
            score++;
            txtScore.text = "Score: " + score.ToString();
        }
    }
}
