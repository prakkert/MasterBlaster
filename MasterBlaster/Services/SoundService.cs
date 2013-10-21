﻿using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class SoundService
    {
        public Dictionary<string, SoundEffect> SoundEffects;

        public SoundService()
        {
            SoundEffects = new Dictionary<string, SoundEffect>();
        }

        public void LoadContent(Dictionary<string,SoundEffect> soundEffects)
        {
            SoundEffects = soundEffects;
        }

        public void PlaySoundEffect(string soundEffect)
        {
            SoundEffects[soundEffect].Play();
        }

        public void PlayExplosion()
        {
            PlaySoundEffect("Explosion");
        }
    }
}