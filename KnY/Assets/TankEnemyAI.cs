using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyAI : GenericEnemyAI {


    // Update is called once per frame
    void Update()
    {
        handleEffects();
        if (cancleUpdate)
        {
            return;
        }
        if (AttackPlayer())
        {
            return;
        }
        HopToPlayer();
    }

    internal void HopToPlayer()
    {
        if (isHopping == false && randomChance < 35)
        {
            Vector2 alteredDirection = new Vector2(Random.Range(direction.x - 0.3f, direction.x + 0.3f), Random.Range(direction.y - 0.3f, direction.y + 0.3f));
            StartCoroutine(hop(alteredDirection, 1, Random.Range(0.2f,0.7f)));
        }
    }
}
