using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticlesCollision : MonoBehaviour
{
    ParticleSystem ps;
    public GameObject player;
    private playerSC playerSc;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        playerSc = player.GetComponent<playerSC>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(0, 0, 0, 0);
            playerSc.AddMana();
          //p.remainingLifetime= 0;
  
            enter[i] = p;

        }
        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

}
