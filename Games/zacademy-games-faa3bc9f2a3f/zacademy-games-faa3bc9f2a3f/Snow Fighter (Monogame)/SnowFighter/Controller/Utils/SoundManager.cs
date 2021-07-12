namespace SnowFighter.Controller.Utils
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Media;
    using Microsoft.Xna.Framework.Audio;

    public class SoundManager
    {
        private Dictionary<string, SoundEffectInstance> effects;

        public SoundManager() {
            this.effects = new Dictionary<string, SoundEffectInstance>();
        }

        public void LoadContent()
        {
            SoundEffect menuSound = Globals.Content.Load<SoundEffect>("MenuSound");
            SoundEffect sound1 = Globals.Content.Load<SoundEffect>("Sound1");
            SoundEffect sound2 = Globals.Content.Load<SoundEffect>("Sound2");
            SoundEffect sound3 = Globals.Content.Load<SoundEffect>("Sound3");
            SoundEffect sound4 = Globals.Content.Load<SoundEffect>("Sound4");
            SoundEffect sound5 = Globals.Content.Load<SoundEffect>("Sound5");
            SoundEffect sound6 = Globals.Content.Load<SoundEffect>("Sound6");
            SoundEffect snowballHit = Globals.Content.Load<SoundEffect>("SnowImpactOnBlock");
            SoundEffect snowballHitBlock = Globals.Content.Load<SoundEffect>("SnowballHit");
            SoundEffect menuMoveSound = Globals.Content.Load<SoundEffect>("MenuMoveSound");
            SoundEffect errorSound = Globals.Content.Load<SoundEffect>("ErrorSound");
            SoundEffect healthPickUp = Globals.Content.Load<SoundEffect>("HealthPackSound");
            
            this.Add("MenuSound", menuSound);
            this.Add("Sound1", sound1);
            this.Add("Sound2", sound2);
            this.Add("Sound3", sound3);
            this.Add("Sound4", sound4);
            this.Add("Sound5", sound5);
            this.Add("Sound6", sound6);
            this.Add("SnowballHit", snowballHit);
            this.Add("SnowballHitBlock", snowballHitBlock);
            this.Add("MenuMove", menuMoveSound);
            this.Add("ErrorSound", errorSound);
            this.Add("HealthPack", healthPickUp);

            Play("MenuSound", 1f);
        }

        public void Add(string name, SoundEffect effect)
        {
            this.effects.Add(name, effect.CreateInstance());
        }

        public void Play(string name)
        {
            Play(name, 0.2f);
        }

        public void Play(string name, float volume)
        {
            this.effects[name].Volume = volume;
            this.effects[name].Play();
        }

        public void Stop(string name)
        {
            this.effects[name].Stop();
        }

        public void Pause(string name)
        {
            this.effects[name].Pause();
        }

        public void Resume(string name)
        {
            this.effects[name].Resume();
        }

        public SoundState GetState(string name)
        {
            return this.effects[name].State;
        }
    }
}
