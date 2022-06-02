using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Hero
{
    public abstract class PressHero : Hero
    {
        public abstract void StartTouch();

        public abstract void EndTouch();
    }
}