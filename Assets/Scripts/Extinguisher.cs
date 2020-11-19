﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (other.tag == "Fire" || other.name =="Fire")
            {
                print("Extinguish!");
                other.SetActive(false);
            }
            i++;
        }
    }
}