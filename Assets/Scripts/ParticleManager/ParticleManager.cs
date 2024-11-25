using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager InstanceParticle;

    public ParticleSystem[] particleSystemWin;

    public void StartParticle()
    {
        for (int i = 0; i < particleSystemWin.Length; i++)
        {
            particleSystemWin[i].Play();    
        }
    }
}
