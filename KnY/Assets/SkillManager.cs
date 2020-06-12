using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> activeSkills = new List<Skill>();
    public List<PassiveSkill> passiveSkill = new List<PassiveSkill>();
    public bool disableMovement;
    public void AddActiveSkill(Skill skill)
    {
        activeSkills.Add(skill);
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
