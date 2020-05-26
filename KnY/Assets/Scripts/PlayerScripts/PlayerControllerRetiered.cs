
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRetiered : MonoBehaviour
{



    public Vector2 movementDirection = Vector2.zero;
    public Vector2 aimDirection = Vector2.zero;

    private float movementSpeedMod;
    private const float DIAGONAL_SPEED = 0.66f;

    private Rigidbody2D rb;
    private Animator anim;
    private Cursor cursor;
    private GameObject _interactionRadius;
    private Statusmanager _myStatus;

    public float castTime;
    private Vector2 attackDirection;
    private float attackMoveMod;
    private Vector2 mousePosition;
    private Vector2 cursorPositon;


    private List<GameObject> targetList;
    private List<GameObject> style3Effects;
    private bool isCasting = false;

    private int lastUsedStyle = 0;

    private Spline2DComponent splineHandler;

    public float style1Cd = 0;

    private float style1CdStatic = 1;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cursor = Director.GetInstance().cursor.GetComponent<Cursor>();
        splineHandler = GameObject.Find("SplineHandler").GetComponent<Spline2DComponent>();
        _myStatus = GetComponent<Statusmanager>();
        _interactionRadius = GameObject.Find("InteractionRadius");
        GetComponent<Inventory>().AddItem(new Item_BlackMarble());
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
    /// Handles Input for Interaction
    /// </summary>
    private void InteractionInput()
    {
        if(Input.GetAxis("Interact") == 1)
        {
            Interact();
        }
    }

    /// <summary>
    /// Interacts with interactabale
    /// </summary>
    private void Interact()
    {
        if (_interactionRadius.GetComponent<InteractionRadius>()._target != null)
        {
            _interactionRadius.GetComponent<InteractionRadius>()._target.Interact(gameObject);
        }
    }

    void LateUpdate()
    {
        ResetAnimationParameters();
    }

    // Resets Parameters for Animator
    private void ResetAnimationParameters()
    {
        if (castTime == 0)
        {
            anim.SetBool("IsAttacking", false);
        }
    }


    // Handles Movement for the Player
    private void MovementInput()
    {
        float moveModifier = 1;
        if (castTime > 0)
        {
            moveModifier = attackMoveMod;
        }
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
        rb.velocity = movementDirection * movementSpeedMod * moveModifier;
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
        _interactionRadius.transform.localPosition = new Vector3(transform.position.x + anim.GetFloat("LastDirX") * 0.09f, transform.position.y + anim.GetFloat("LastDirY") * 0.09f, 0);
    }

    // Handles Battle for the Player
    private void BattleInput()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 heading = mousePosition - PlayerPosition2D();
        float distance = heading.magnitude;
        attackDirection = heading / distance;
        if (Input.GetAxis("Attack1") == 1 && castTime == 0) // Style 7 (SOME STAB)
        {
            SetAttackParameters(attackDirection.x, attackDirection.y, 1);

            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D() + attackDirection * 0.15f, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 7);
            damageObject.transform.up = mousePosition - (Vector2)transform.position;
            damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage + 10, GetComponent<Statusmanager>().CriticalStrikeChance, 0.2f, 0.4f, gameObject, 1);

            attackMoveMod = 0.15f;
            castTime = 0.4f;
            lastUsedStyle = 7;
        }
        if (Input.GetAxis("Attack2") == 1 && castTime == 0 && style1Cd == 0) // Style 1 (BASICATTACK)
        {
            SetAttackParameters(attackDirection.x, attackDirection.y, 2);
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D() + attackDirection * 0.15f, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
            damageObject.transform.up = mousePosition - (Vector2)transform.position;
            damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage, GetComponent<Statusmanager>().CriticalStrikeChance, 0.1f, 0.2f, gameObject, 2);
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.4f, 0.25f);
            attackMoveMod = 0.5f;
            // Calculate Attackspeed
            float factor = (float)GetComponent<Statusmanager>().TotalAttackSpeed() / 100;
            castTime = (float)(0.4f / factor);
            style1Cd = (float)(style1CdStatic / factor);
            anim.SetFloat("SpeedIncrease", (float)((1 * factor) - 1) * 0.4f);
   
            lastUsedStyle = 1;
        }
        if (Input.GetAxis("Attack3") == 1 && castTime == 0) // Style 9 ?
        {
            attackMoveMod = 3f;
            castTime = 0.3f;
            StartCoroutine(Style3Effects());
            lastUsedStyle = 9;
        }
        if (Input.GetAxis("Attack4") == 1 && castTime == 0) // Style 2 (ROUNDHOUSE AOE ATTACK)
        {
            SetAttackParameters(attackDirection.x, attackDirection.y, 3);
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D(), Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 2);
            damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage, GetComponent<Statusmanager>().CriticalStrikeChance, 0.2f, 0.4f, gameObject, 3);
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(1.2f, 0.15f);
            GameObject damageObject2 = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D(), Quaternion.identity);
            damageObject2.GetComponent<Animator>().SetFloat("DamageAnimation", 2);
            damageObject2.transform.up = mousePosition - (Vector2)transform.position;
            attackMoveMod = 0.15f;
            castTime = 0.15f;
            lastUsedStyle = 2;
        }
        if (Input.GetAxis("Attack5") == 1) // Style 3 (Dash Dash Dash)
        {
            if (!cursor.gameObject.activeSelf)
            {
                cursor.gameObject.SetActive(true);
                cursor.StartTargeting();
                lastUsedStyle = 3;
            }
        }
        if (Input.GetAxis("Attack5") == 0 && cursor.gameObject.activeSelf && lastUsedStyle == 3) // Style continued 3
        {
            targetList = cursor.GetTargetList();
            if (targetList.Count > 0)
            {
                cursorPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isCasting = true;
                StartCoroutine(Style3Casting());
            }
            cursor.ResetTargetList();
            cursor.gameObject.SetActive(false);

        }
        if (Input.GetAxis("Attack6") == 1) // Style 10
        {

            if (!cursor.gameObject.activeSelf)
            {
                cursor.gameObject.SetActive(true);
                cursor.StartTargeting();
                lastUsedStyle = 10;
            }
        }
        if (Input.GetAxis("Attack6") == 0 && cursor.gameObject.activeSelf && lastUsedStyle == 10) // Style continued 10
        {
            cursorPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isCasting = true;
            targetList = cursor.GetTargetList();
            StartCoroutine(Style10Casting());
            cursor.ResetTargetList();
            cursor.gameObject.SetActive(false);
        }
    }

    private void SetAttackParameters(float x, float y, int type)
    {
        anim.SetFloat("AttackDirX", x);
        anim.SetFloat("AttackDirY", y);
        anim.SetFloat("AttackType", type);
        anim.SetBool("IsAttacking", true);
    }

    private Vector2 PlayerPosition2D()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    private void UpdateTimers()
    {
        if (castTime > 0 && !isCasting)
        {
            castTime -= Time.deltaTime * Director.TimeScale();
            //rb.velocity = attackDirection * attackMoveMod;
            if (castTime < 0)
            {
                castTime = 0;
            }
        }

        // Update Cooldown UI
        if(style1Cd > 0)
        {
            
            style1Cd -= Time.deltaTime * Director.TimeScale();
            GameObject.Find("CooldownSlider1").GetComponent<Slider>().value = style1Cd / style1CdStatic;
            GameObject.Find("CooldownSlider1").GetComponent<Slider>().maxValue = (float)(style1CdStatic - (style1CdStatic / 100 * GetComponent<Statusmanager>().TotalAttackSpeed())); ;
            GameObject.Find("CooldownText1").GetComponent<Text>().text = style1Cd.ToString("0");
            if(style1Cd <= 0)
            {
                GameObject.Find("CooldownText1").GetComponent<Text>().text = "";
            }
        }

        // Set timer to 0 if under 0
        if (style1Cd < 0)
        {
            style1Cd = 0;
        }
    }

    IEnumerator Style3Effects()
    {
        while (castTime > 0.05f)
        {
            SpawnWatertrailEffect();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SpawnWatertrailEffect()
    {
        GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D() + attackDirection * 0.15f, Quaternion.identity);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 3);
        damageObject.transform.up = mousePosition - (Vector2)transform.position;
        damageObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    private void SpawnWatertrailEffect(Vector2 direction, Vector2 target)
    {
        Vector2 playerPos = (Vector2)transform.position;
        float distance = Vector2.Distance(target, playerPos);
        int parts = 6;
        for (int i = 1; i < parts; i++)
        {
            Vector2 normalized = (playerPos - target).normalized;
            Vector2 spawnPosition = target + (normalized * (((float)i / parts) * distance));
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, spawnPosition, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 3);
            damageObject.GetComponent<Animator>().SetFloat("CycleOffset", UnityEngine.Random.Range(0, 3));
            damageObject.GetComponent<Animator>().SetBool("LoopAnimation", true);
            damageObject.transform.up = target - playerPos;
            damageObject.GetComponent<DamageObject>().lingeringTime = 100f;
            damageObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            damageObject.transform.localScale = new Vector3(0.75f, 0.75f,1);
            style3Effects.Add(damageObject);
        }
    }

    private void SpawnWatercurveEffect(int i)
    {
        Vector2 playerPos = (Vector2)transform.position;
        Vector2 lastPos = playerPos;
        foreach (Vector2 v in splineHandler.GetSplinePositions(i))
        {
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, v, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 3);
            damageObject.GetComponent<Animator>().SetFloat("CycleOffset", UnityEngine.Random.Range(0, 3));
            damageObject.GetComponent<Animator>().SetBool("LoopAnimation", true);
            damageObject.transform.up = lastPos - v;
            damageObject.transform.eulerAngles += new Vector3(0, 0, 180);
            damageObject.GetComponent<DamageObject>().lingeringTime = 100f;
            damageObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            damageObject.transform.localScale = new Vector3(0.75f, 0.75f, 1);
            style3Effects.Add(damageObject);
            lastPos = v;
        }
    }


    IEnumerator Style3Casting()
    {
        // Initializaion
        #region Initializaion()
        splineHandler.ClearAllPoints();
        splineHandler.AddPoint(transform.position);
        style3Effects = new List<GameObject>();
        Vector2 direction = Vector2.zero;
        float distance;
        Vector2 heading;
        Vector2 playerPos = (Vector2)transform.position;
        Vector2 lastPos = playerPos;
        float test = 0;
        List<StatusEffect_Stunned> effectInstances = new List<StatusEffect_Stunned>();
        #endregion

        // Apply Stun Effect to all targets and add them to splinePoints
        foreach (GameObject t in targetList)
        {
            splineHandler.AddPoint(t.transform.position);
            StatusEffect_Stunned effect = new StatusEffect_Stunned(100);
            effectInstances.Add(effect);
            t.GetComponent<Statusmanager>().statusEffects.Add(effect);
        }

        #region waitForSeconds(0.15f);
        for (float timeElapsed = 0; timeElapsed <= 0.15f / Director.TimeScale(); timeElapsed += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }
        #endregion

        #region ExecuteAction();
        // Spawn Water effects
        for (int i = 0; i < targetList.Count; i++)
        {
            GetComponent<DepthSorter>().offset = UnityEngine.Random.Range(0, 1);
            heading = (Vector2)targetList[i].transform.position - PlayerPosition2D();
            distance = Vector2.Distance(transform.position, targetList[i].transform.position);
            if (distance != 0)
            { 
                direction = heading / distance;
            }
            else
            {
                direction = heading;
            }
            playerPos = (Vector2)transform.position;
            lastPos = playerPos;
            foreach (Vector2 v in splineHandler.GetSplinePositions(i))
            {
                GameObject damageObject2 = Instantiate(PublicGameResources.GetResource().damageObject, v, Quaternion.identity);
                damageObject2.GetComponent<Animator>().SetFloat("DamageAnimation", 3);
                damageObject2.GetComponent<Animator>().SetFloat("CycleOffset", UnityEngine.Random.Range(0, 3));
                damageObject2.GetComponent<Animator>().SetBool("LoopAnimation", true);
                damageObject2.transform.up = lastPos - v;
                damageObject2.transform.eulerAngles += new Vector3(0, 0, 180);
                damageObject2.GetComponent<DamageObject>().lingeringTime = 100f;
                damageObject2.GetComponent<SpriteRenderer>().sortingOrder = -1;
                test += Time.deltaTime;
                damageObject2.transform.localScale = new Vector3(Mathf.Sin(test * 2) / 12.5f + 0.75f, Mathf.Sin(test * 2) / 12.5f + 0.75f,1);
                style3Effects.Add(damageObject2);
                lastPos = v;
                yield return new WaitForSeconds(0.0165f / Director.TimeScale());
            }
            transform.position = (Vector2)targetList[i].transform.position + new Vector2(0, -0.01f);
            anim.StopPlayback();
            SetAttackParameters(direction.x, direction.y, 2);
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D() + attackDirection * 0.15f, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
            damageObject.GetComponent<DamageObject>().lingeringTime = 0.5f * Director.TimeScale();
            damageObject.transform.up = direction;
            damageObject.name = ("Effect");
        }
        // Repeat Spawning for the lastTarget
        foreach (Vector2 v in splineHandler.GetSplinePositions(targetList.Count))
        {
            GameObject damageObject2 = Instantiate(PublicGameResources.GetResource().damageObject, v, Quaternion.identity);
            damageObject2.GetComponent<Animator>().SetFloat("DamageAnimation", 3);
            damageObject2.GetComponent<Animator>().SetFloat("CycleOffset", UnityEngine.Random.Range(0, 3));
            damageObject2.GetComponent<Animator>().SetBool("LoopAnimation", true);
            damageObject2.transform.up = lastPos - v;
            damageObject2.transform.eulerAngles += new Vector3(0, 0, 180);
            damageObject2.GetComponent<DamageObject>().lingeringTime = 100f;
            damageObject2.GetComponent<SpriteRenderer>().sortingOrder = -1;
            damageObject2.transform.localScale = new Vector3(Mathf.Sin(test) / 3 + 0.9f, Mathf.Sin(test) / 3 + 0.9f,1);
            style3Effects.Add(damageObject2);
            lastPos = v;
            for (float timeElapsed = 0; timeElapsed <= 0.0075f / Director.TimeScale(); timeElapsed += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        #endregion
        GetComponent<DepthSorter>().offset = 0;
        heading = (Vector2)transform.position - cursorPositon;
        distance = Vector2.Distance(transform.position, cursorPositon);
        if (distance != 0)
        {
            direction = heading / distance;
        }
        else
        {
            direction = heading;
        }
        SetAttackParameters(direction.x, direction.y, 2);
        transform.position = (Vector2)transform.position + direction * -0.35f;
        for (float timeElapsed = 0; timeElapsed <= 0.3f / Director.TimeScale(); timeElapsed += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }
        foreach (GameObject go in style3Effects)
        {
            DamageObject.SoftDestory(go);
        }
        for (float timeElapsed = 0; timeElapsed <= 0.05f / Director.TimeScale(); timeElapsed += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < targetList.Count; i++)
        {
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, targetList[i].transform.position, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
            damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage * 5, GetComponent<Statusmanager>().CriticalStrikeChance, 0.0f, 0.4f, gameObject, 4);
        }
        // Remove Stun Effect from all targets
        foreach(StatusEffect_Stunned effect in effectInstances)
        {
            effect.duration = 0;
        }
        isCasting = false;
    }
    IEnumerator Style10Casting()
    {
        style3Effects = new List<GameObject>();
        yield return new WaitForSeconds(0.15f);
        GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D(), Quaternion.identity);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 2);
        damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage, GetComponent<Statusmanager>().CriticalStrikeChance, 0.2f, 0.4f, gameObject, 3);
        for (int i = 0; i < targetList.Count; i++)
        {
            float distance = 1000;
            while (distance > 0.2f)
            {

                Vector2 heading = (Vector2)targetList[i].transform.position - PlayerPosition2D();
                Vector2 direction = heading / distance;
                SpawnWatertrailEffect(direction, targetList[i].transform.position);
                transform.position = targetList[i].transform.position;
                distance = Vector2.Distance(transform.position, targetList[i].transform.position);
                SetAttackParameters(direction.x, direction.y, 3);
                damageObject = Instantiate(PublicGameResources.GetResource().damageObject, PlayerPosition2D(), Quaternion.identity);
                damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 2);
                damageObject.GetComponent<DamageObject>().SetValues(_myStatus.totalAttackDamage * i, GetComponent<Statusmanager>().CriticalStrikeChance, 0.2f, 0.4f, gameObject, 3);
                damageObject.transform.up = direction;
                yield return new WaitForSeconds(0.25f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject go in style3Effects)
        {
            DamageObject.SoftDestory(go);
        }
        yield return new WaitForSeconds(0.4f);
        isCasting = false;
    }
}
