using MasterBlaster.Components;
using MasterBlaster.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class SoundService : IUpdatableComponent
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

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public bool Destroyed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
