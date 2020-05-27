using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movementDirection = Vector2.zero;
    public Vector2 aimDirection = Vector2.zero;
    private float movementSpeedMod;
    private const float DIAGONAL_SPEED = 0.66f;
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject interactionRadius;
    public List<Skill> skills = new List<Skill>();


    // Start is called before the first frame update
    void Start()
    {
        interactionRadius = GameObject.Find("InteractionRadius");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        skills.Add(new Skill_BasicAttack(1, 0.4f, true));
        skills.Add(new Skill_Dodge(1, 0.4f, false));
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        BattleInput();
        InteractionInput();
        UpdateTimers();
    }


    /// <summary>
    /// Handles movement for the player
    /// </summary>
    private void MovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x == 0 || y == 0) // Checks if the player is moving diagonaly, Reduces Movement speed acordingly
        {
            movementSpeedMod = GetComponent<Statusmanager>().TotalMovementSpeed;
        }
        else
        {

            movementSpeedMod = GetComponent<Statusmanager>().TotalMovementSpeed * DIAGONAL_SPEED;
        }
        movementDirection = new Vector2(x, y);
        rb.velocity = movementDirection * movementSpeedMod;
        // Set Parameter for Animator
        if (x != 0 || y != 0)
        {
            anim.SetFloat("LastDirX", anim.GetFloat("DirX"));
            anim.SetFloat("LastDirY", anim.GetFloat("DirY"));
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        anim.SetFloat("DirX", x);
        anim.SetFloat("DirY", y);
        // Set Position for Interaction Radius
        interactionRadius.transform.localPosition = new Vector3(transform.position.x + anim.GetFloat("LastDirX") * 0.09f, transform.position.y + anim.GetFloat("LastDirY") * 0.09f, 0);
    }


    /// <summary>
    /// Handles Input for SkillComands
    /// </summary>
    private void BattleInput()
    {
        if (Input.GetAxis("Attack2") == 1 && skills[0].CooldownTimer <= 0) // Style 1 (BASICATTACK)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mousePosition - (Vector2)transform.position;
            float distance = heading.magnitude;
            Vector2 attackDirection = heading / distance;
            skills[0].ActivateSkill(gameObject, attackDirection, null);
        }
        if (Input.GetAxis("Attack1") == 1 && skills[1].CooldownTimer <= 0) // Dash
        {
            //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 heading = mousePosition - (Vector2)transform.position;
            //float distance = heading.magnitude;
            //Vector2 direction = heading / distance;
            skills[1].ActivateSkill(gameObject, movementDirection, null);
        }
    }


    /// <summary>
    /// Handles Input for Interaction
    /// </summary>
    private void InteractionInput()
    {
        if (Input.GetAxis("Interact") == 1)
        {
            Interact();
        }
    }


    /// <summary>
    /// Interacts with interactabale
    /// </summary>
    private void Interact()
    {
        if (interactionRadius.GetComponent<InteractionRadius>()._target != null)
        {
            interactionRadius.GetComponent<InteractionRadius>()._target.Interact(gameObject);
        }
    }


    /// <summary>
    /// Updates the Timers of skills
    /// </summary>
    private void UpdateTimers()
    {
        foreach(Skill skill in skills)
        {
            if(skill.CasttimeTimer > 0)
            {
                skill.CastSkill(gameObject);
            }
            skill.UpdateTimers();
        }
        
    }

}
