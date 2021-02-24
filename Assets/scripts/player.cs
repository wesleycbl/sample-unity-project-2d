using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Animator _playerAnimator;
    private Rigidbody2D _playerRb;
    private float _horizontal;
    private float _vertical;
    public bool grounded;
    public bool attacking;
    public int idAnimation;
    public Transform groundCheck; 
    public float speed; 
    public float jumpForce;
    public bool lookLeft;
    // start

    void Start()
    {
        _playerAnimator = GetComponent<Animator>(); // inicializando compoennte
        _playerRb       = GetComponent<Rigidbody2D>(); // inicializando compoennte
    }
    // comandos que utilizam de manuceio de Fisica, deve ser utilizado esse lifecyrcle
    void FixedUpdate() { //taxa de atualização fixa de 0.02
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        _playerRb.velocity = new Vector2(_horizontal * speed, _playerRb.velocity.y);
    } 
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal"); 
        _vertical   = Input.GetAxisRaw("Vertical"); 

        if (lookLeft && _horizontal > 0 && attacking == false) {
            flip();
        } else if (!lookLeft && _horizontal < 0  && attacking == false) {
            flip();
        }
        
        if (_vertical < 0) {
            idAnimation = 2;
            if (grounded) {
                _horizontal = 0;
            }
            _horizontal = 0;
        }
        else if (_horizontal != 0) {
            idAnimation = 1;
        } else {
            idAnimation = 0;
        }

        if(Input.GetButtonDown("Fire1") && _vertical >= 0 && attacking == false) {
            _playerAnimator.SetTrigger("attack");
        }

        if(Input.GetButtonDown("Jump") && grounded == true && attacking == false) {
            _playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if(attacking && grounded){
            _horizontal = 0;
        }

        _playerAnimator.SetBool("grounded", grounded);
        _playerAnimator.SetInteger("idAnimation", idAnimation);
        _playerAnimator.SetFloat("speedY", _playerRb.velocity.y);
    }

    void flip() {
        lookLeft = !lookLeft;

        float x = transform.localScale.x;
        x *= -1;
        transform.localScale 
            = new Vector3(
                x,
                transform.localScale.y,
                transform.localScale.z
            );
    }

    void attack(int attack){
        switch(attack) {
            case 0:
                attacking = false;
                break;
            case 1:
                attacking = true;
                break;
        }
    }
}
