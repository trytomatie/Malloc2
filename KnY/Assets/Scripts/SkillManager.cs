using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Skill[] activeSkills = new Skill[6];
    private PassiveSkill[] passiveSkills = new PassiveSkill[2];
    public bool disableMovement;

    public PassiveSkill[] PassiveSkills
    {
        get
        {
            return passiveSkills;
        }

        set
        {
            passiveSkills = value;
        }
    }

    public Skill[] ActiveSkills { get => activeSkills; set => activeSkills = value; }

    public void AddActiveSkill(Skill skill,int index)
    {
        ActiveSkills[index+2] = skill;
        UI_SkillManager.UpdateSkills();
    }

    public void RemoveActiveSkill(int index)
    {
        ActiveSkills[index + 2] = null;
        UI_SkillManager.UpdateSkills();
    }

    public void AddPassiveSkill(PassiveSkill skill)
    {
        if (skill == null)
        {
            return;
        }
        if(skill.Type1 == PassiveSkill.Type.Weapon)
        {
            if(PassiveSkills[0] != null)
            {
                PassiveSkills[0].RemoveEffects(gameObject);
            }
            PassiveSkills[0] = skill;
        }
        else
        {
            if (PassiveSkills[1] != null)
            {
                PassiveSkills[1].RemoveEffects(gameObject);
            }
            PassiveSkills[1] = skill;
        }
        skill.ApplyEffects(gameObject);
        UI_SkillManager.UpdateSkills();
    }

    public void RemovePassiveSkill(PassiveSkill skill)
    {
        if(skill == null)
        {
            return;
        }
        if (skill.Type1 == PassiveSkill.Type.Weapon)
        {
            PassiveSkills[0] = null;
        }
        else
        {

            PassiveSkills[1] = null;
        }
        skill.RemoveEffects(gameObject);
        UI_SkillManager.UpdateSkills();
    }

    private void Update()
    {
        UpdateTimers();
    }

    /// <summary>
    /// Updates the Timers of skills
    /// </summary>
    private void UpdateTimers()
    {
        int spellsThatDontAllowMovementThatAreCasting = 0;
        foreach (Skill skill in ActiveSkills)
        {
            if(skill == null)
            {
                continue;
            }
            if (skill.CasttimeTimer > 0)
            {
                skill.SkillCastingPhase(gameObject);
            }
            spellsThatDontAllowMovementThatAreCasting += skill.UpdateTimers(gameObject);
        }
        if (spellsThatDontAllowMovementThatAreCasting > 0)
        {
            disableMovement = true;
        }
        else
        {
            disableMovement = false;
        }

    }
}
