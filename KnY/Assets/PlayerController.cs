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
    private Statusmanager myStatus;
    private SkillManager skillManager;
    public List<Color> swordGlowColor = new List<Color>();
    private Color baseSwordGlowColor;
    public Color blended;

    // Start is called before the first frame update
    void Start()
    {
        baseSwordGlowColor = GetComponent<SpriteRenderer>().material.GetColor("_color");
        interactionRadius = GameObject.Find("InteractionRadius");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myStatus = GetComponent<Statusmanager>();
        skillManager = GetComponent<SkillManager>();
        skillManager.AddActiveSkill(new Skill_BasicAttack(0.4f, 0.4f, false));
        skillManager.AddActiveSkill(new Skill_Dodge(0.7f, 0.4f, false));
        skillManager.AddActiveSkill(new Skill_AoeDash(10f, 0.4f, false));
        skillManager.AddActiveSkill(new Skill_ThunderStrike(8f, 0.8f,0.25f,3, false, GetComponent<SpriteRenderer>().material));
        //skills.Add(new Skill_Laser(1f, 1.8f, 0.25f, 3, false));
        foreach (Skill skill in skillManager.activeSkills)
        {
            skill.Anim = anim;
        }
        //GetComponent<Inventory>().AddItem(new Item_BlackMarble());
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        BattleInput();
        InteractionInput();
    }


    /// <summary>
    /// Handles movement for the player
    /// </summary>
    private void MovementInput()
    {
        float x = 0;
        float y = 0;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (x == 0 || y == 0) // Checks if the player is moving diagonaly, Reduces Movement speed acordingly
        {
            movementSpeedMod = GetComponent<Statusmanager>().TotalMovementSpeed;
        }
        else
        {

            movementSpeedMod = GetComponent<Statusmanager>().TotalMovementSpeed * DIAGONAL_SPEED;
        }
        movementDirection = new Vector2(x, y);
        if (!skillManager.disableMovement)
        {
            rb.velocity = movementDirection * movementSpeedMod;
        }
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
        if (Input.GetAxis("Attack2") == 1 && skillManager.activeSkills[0].CooldownTimer <= 0 && skillManager.activeSkills[0].SpCost <= myStatus.Sp) // Style 1 (BASICATTACK)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mousePosition - (Vector2)transform.position;
            float distance = heading.magnitude;
            Vector2 attackDirection = heading / distance;
            skillManager.activeSkills[0].ActivateSkill(gameObject, attackDirection, null);
        }
        if (Input.GetAxis("Attack1") == 1 && skillManager.activeSkills[1].CooldownTimer <= 0 && skillManager.activeSkills[1].SpCost <= myStatus.Sp) // Dash
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mousePosition - (Vector2)transform.position;
            float distance = heading.magnitude;
            Vector2 attackDirection = heading / distance;
            skillManager.activeSkills[1].ActivateSkill(gameObject, movementDirection, null);
        }
        if (Input.GetAxis("Attack3") == 1 && skillManager.activeSkills[2].CooldownTimer <= 0 && skillManager.activeSkills[2].SpCost <= myStatus.Sp) // Dash and Spin
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mousePosition - (Vector2)transform.position;
            float distance = heading.magnitude;
            Vector2 attackDirection = heading / distance;
            skillManager.activeSkills[2].ActivateSkill(gameObject, attackDirection, null);
        }
        if (Input.GetAxis("Attack4") == 1 && skillManager.activeSkills[3].CooldownTimer <= 0 && skillManager.activeSkills[3].SpCost <= myStatus.Sp) // Test
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mousePosition - (Vector2)transform.position;
            float distance = heading.magnitude;
            Vector2 attackDirection = heading / distance;
            skillManager.activeSkills[3].ActivateSkill(gameObject, mousePosition, null);
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
        if (Input.GetAxis("Interact2") == 1)
        {
            AlternateInteract();
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
    /// Interacts with interactabale and calls the AlternateInteraction
    /// </summary>
    private void AlternateInteract()
    {
        if (interactionRadius.GetComponent<InteractionRadius>()._target != null)
        {
            interactionRadius.GetComponent<InteractionRadius>()._target.AlternateInteract(gameObject);
        }
    }


    /// <summary>
    /// Changes Color of an Material
    /// </summary>
    /// <param name="m"></param>
    /// <param name="colors"></param>
    public void ChangeSwordGlow()
    {
        Color result = new Color(0, 0, 0, 0);
        Material m = GetComponent<SpriteRenderer>().material;
        if(swordGlowColor.Count > 0)
        { 
            foreach (Color c in swordGlowColor)
            {
                result += c;
            }
            result /= swordGlowColor.Count;
            float h;
            float s;
            float v;
            Color.RGBToHSV(result, out h, out s, out v);
            v = 1;
            result = Color.HSVToRGB(h, s, v);
            m.SetColor("_color", result);
            blended = result;
        }
        else
        {
            m.SetColor("_color", baseSwordGlowColor);
        }
    }

}
