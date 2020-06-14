using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> activeSkills = new List<Skill>();
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

    public void AddActiveSkill(Skill skill)
    {
        activeSkills.Add(skill);
        UI_SkillManager.UpdateSkills();
    }

    public void RemoveActiveSkill(Skill skill)
    {
        activeSkills.Remove(skill);
        activeSkills.RemoveAll(item => item == null);
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
            PassiveSkills[0] = skill;
        }
        else
        {
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
        foreach (Skill skill in activeSkills)
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
