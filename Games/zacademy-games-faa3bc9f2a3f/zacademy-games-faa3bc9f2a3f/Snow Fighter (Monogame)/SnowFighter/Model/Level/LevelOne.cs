using Microsoft.Xna.Framework;
using SnowFighter.Model.GameObjects;
using SnowFighter.View;
using System.Collections.Generic;

namespace SnowFighter.Model.Level
{
    public class LevelOne: Level
    {
        public LevelOne()
        {
            this.ListOfSnowballs = new List<Snowball>();

            this.ListOfSnowPiles = new List<SnowPile>
            {
                new SnowPile(new Vector2(500, 880))
            };

            this.ListOfHealthPack = new List<HealthPack>
            {
                new HealthPack(new Vector2(600, 800))
            };

            this.Blocks = new List<Block>
            {
                // BLOCKS
                // TOP BLOCK LINE
                new Block(new Vector2(0, 70), BlockType.IceCube),
                new Block(new Vector2(40, 70), BlockType.IceCube),
                new Block(new Vector2(80, 70), BlockType.IceCube),
                new Block(new Vector2(120, 70), BlockType.IceCube),
                new Block(new Vector2(160, 70), BlockType.IceCube),
                new Block(new Vector2(200, 70), BlockType.IceCube),
                new Block(new Vector2(240, 70), BlockType.IceCube),
                new Block(new Vector2(280, 70), BlockType.IceCube),
                new Block(new Vector2(320, 70), BlockType.IceCube),
                new Block(new Vector2(360, 70), BlockType.IceCube),
                new Block(new Vector2(400, 70), BlockType.IceCube),
                new Block(new Vector2(440, 70), BlockType.IceCube),
                new Block(new Vector2(480, 70), BlockType.IceCube),
                new Block(new Vector2(520, 70), BlockType.IceCube),
                new Block(new Vector2(560, 70), BlockType.IceCube),
                new Block(new Vector2(600, 70), BlockType.IceCube),
                new Block(new Vector2(640, 70), BlockType.IceCube),
                new Block(new Vector2(680, 70), BlockType.IceCube),
                new Block(new Vector2(720, 70), BlockType.IceCube),
                new Block(new Vector2(760, 70), BlockType.IceCube),
                new Block(new Vector2(800, 70), BlockType.IceCube),
                new Block(new Vector2(840, 70), BlockType.IceCube),
                new Block(new Vector2(880, 70), BlockType.IceCube),
                new Block(new Vector2(920, 70), BlockType.IceCube),
                new Block(new Vector2(960, 70), BlockType.IceCube),
                new Block(new Vector2(1000, 70), BlockType.IceCube),
                new Block(new Vector2(1040, 70), BlockType.IceCube),
                new Block(new Vector2(1080, 70), BlockType.IceCube),
                new Block(new Vector2(1120, 70), BlockType.IceCube),
                new Block(new Vector2(1160, 70), BlockType.IceCube),
                new Block(new Vector2(1200, 70), BlockType.IceCube),
                new Block(new Vector2(1240, 70), BlockType.IceCube),        
                //// BOT BLOCK LINE
                new Block(new Vector2(0, 950), BlockType.IceBlock),
                new Block(new Vector2(80, 950), BlockType.IceBlock),
                new Block(new Vector2(160, 950), BlockType.IceBlock),
                new Block(new Vector2(240, 950), BlockType.IceBlock),
                new Block(new Vector2(320, 950), BlockType.IceBlock),
                new Block(new Vector2(400, 950), BlockType.IceBlock),
                new Block(new Vector2(480, 950), BlockType.IceBlock),
                new Block(new Vector2(560, 950), BlockType.IceBlock),
                new Block(new Vector2(640, 950), BlockType.IceBlock),
                new Block(new Vector2(720, 950), BlockType.IceBlock),
                new Block(new Vector2(800, 950), BlockType.IceBlock),
                new Block(new Vector2(880, 950), BlockType.IceBlock),
                new Block(new Vector2(960, 950), BlockType.IceBlock),
                new Block(new Vector2(1040, 950), BlockType.IceBlock),
                new Block(new Vector2(1120, 950), BlockType.IceBlock),
                new Block(new Vector2(1200, 950), BlockType.IceBlock),
                //// some
                new Block(new Vector2(0, 700), BlockType.IceBlock),
                new Block(new Vector2(80, 700), BlockType.IceBlock),
                new Block(new Vector2(160, 750), BlockType.IceBlock),
                //// some
                new Block(new Vector2(1040, 750), BlockType.IceBlock),
                new Block(new Vector2(1120, 700), BlockType.IceBlock),
                new Block(new Vector2(1200, 700), BlockType.IceBlock),
                //// LEFT CUBE LINE
                new Block(new Vector2(0, 400), BlockType.IceCube),
                new Block(new Vector2(40, 400), BlockType.IceCube),
                new Block(new Vector2(0, 450), BlockType.IceCube),
                new Block(new Vector2(0, 500), BlockType.IceCube),               
                //// RIGHT CUBE LINE
                new Block(new Vector2(1200, 400), BlockType.IceCube),
                new Block(new Vector2(1240, 400), BlockType.IceCube),
                new Block(new Vector2(1240, 450), BlockType.IceCube),
                new Block(new Vector2(1240, 500), BlockType.IceCube),
                //// Central blocks
                new Block(new Vector2(480, 580), BlockType.IceBlock),
                new Block(new Vector2(560, 580), BlockType.IceBlock),
                new Block(new Vector2(640, 580), BlockType.IceBlock),
                new Block(new Vector2(720, 580), BlockType.IceBlock),
                new Block(new Vector2(420, 620), BlockType.IceBlock),
                new Block(new Vector2(480, 620), BlockType.IceBlock),
                new Block(new Vector2(560, 620), BlockType.IceBlock),
                new Block(new Vector2(640, 620), BlockType.IceBlock),
                new Block(new Vector2(720, 620), BlockType.IceBlock),
                new Block(new Vector2(780, 620), BlockType.IceBlock),

                //// Central upper
                new Block(new Vector2(600, 380), BlockType.IceBlock),
                new Block(new Vector2(520, 380), BlockType.IceBlock),
                new Block(new Vector2(680, 380), BlockType.IceBlock),
                //// Right central blocks
                new Block(new Vector2(1200, 350), BlockType.IceBlock),
                new Block(new Vector2(1040, 400), BlockType.IceBlock),
                new Block(new Vector2(1120, 400), BlockType.IceBlock),
                //// Left central blocks
                new Block(new Vector2(0, 350), BlockType.IceBlock),
                new Block(new Vector2(80, 400), BlockType.IceBlock),
                new Block(new Vector2(160, 400), BlockType.IceBlock),
            };
        }
    }
}
