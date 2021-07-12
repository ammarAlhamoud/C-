namespace SnowFighter.Controller.States
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Controller.Utils;
    using Model.Level;
    using Model.Player;
    using View;
    using View.UI;
    using Model.GameObjects;

    public class UpdateState : State
    {
        private Sprite levelBackground;
        private Level level;

        private List<Player> players;
        private List<Animation> playerAnimations;

        private List<Sprite> snowballSprites;
        private List<Sprite> snowPileSprites;
        private List<Animation> healthPackAnimations;

        //private List<Animation> listOfPlayerAnimations;

        public UpdateState(InputHandler inputHandler, UIFactory uiFactory, SoundManager soundManager, List<Player> playerData = null)
            : base(inputHandler, uiFactory, soundManager)
        {
            this.levelBackground = UIFactory.CreateSprite("Level1Background");
            this.level = new LevelOne();


            if (playerData == null)
            {
                Player player1 = new Player(Keys.A, Keys.D, Keys.W, Keys.Space, new Vector2(100, 800), true);
                Player player2 = new Player(Keys.Left, Keys.Right, Keys.Up, Keys.NumPad0, new Vector2(1100, 800), false);

                this.players = new List<Player>();
                this.players.Add(player1);
                this.players.Add(player2);
            }
            else
            {
                this.players = playerData;
            }

            this.snowballSprites = new List<Sprite>();

            this.Initialize();
        }

        public void Initialize()
        {
            this.SpritesInState.Add(this.levelBackground);

            foreach (var block in this.level.Blocks)
            {
                Sprite sprite = UIFactory.CreateSprite(block.Type.ToString());
                sprite.Position = block.Position;
                block.Bounds = new Rectangle((int)block.Position.X, (int)block.Position.Y,
                    sprite.Texture.Width, sprite.Texture.Height);
                this.SpritesInState.Add(sprite);
            }

            this.playerAnimations = new List<Animation>();

            Animation playerAnimation1 = AnimationFactory.CreatePlayerAnimation(new Color(Color.White, 0.9f));
            this.SpritesInState.Add(playerAnimation1);
            this.playerAnimations.Add(playerAnimation1);

            Animation playerAnimation2 = AnimationFactory.CreatePlayerAnimation(Color.PaleVioletRed);
            this.SpritesInState.Add(playerAnimation2);
            this.playerAnimations.Add(playerAnimation2);


            this.snowPileSprites = new List<Sprite>();
            foreach (var snowPile in this.level.ListOfSnowPiles)
            {
                Sprite snowPileSprite = UIFactory.CreateSprite("PileOfSnow");
                snowPileSprite.Position = snowPile.Position;
                snowPile.Bounds = new Rectangle((int)snowPile.Position.X, (int)snowPile.Position.Y, 
                    snowPileSprite.Texture.Width, snowPileSprite.Texture.Height);
                this.SpritesInState.Add(snowPileSprite);
                this.snowPileSprites.Add(snowPileSprite);
            }

            this.healthPackAnimations = new List<Animation>();
            foreach (var healthPack in this.level.ListOfHealthPack)
            {
                Animation healthPackAnim = AnimationFactory.CreateHealthPack();
                healthPackAnim.Position = healthPack.Position;
                healthPack.IsDrawn = true;
                healthPack.IsActive = true;

                this.SpritesInState.Add(healthPackAnim);
                this.healthPackAnimations.Add(healthPackAnim);
            }

            // Player 1
            this.uiFactory.Player1Name.Position = new Vector2(20, 0);
            this.SpritesInState.Add(this.uiFactory.Player1Name);

            this.uiFactory.HealthBarEmptyPlayer1.Position = new Vector2(175, 5);
            this.SpritesInState.Add(this.uiFactory.HealthBarEmptyPlayer1);

            this.uiFactory.HealthbarFullPlayer1.Position = new Vector2(180, 10);
            this.SpritesInState.Add(this.uiFactory.HealthbarFullPlayer1);

            this.uiFactory.SnowballBarEmptyPlayer1.Position = new Vector2(180, 45);
            this.SpritesInState.Add(this.uiFactory.SnowballBarEmptyPlayer1);

            this.uiFactory.SnowballBarFullPlayer1.Position = new Vector2(180, 45);
            this.SpritesInState.Add(this.uiFactory.SnowballBarFullPlayer1);

            // Player 2
            this.uiFactory.Player2Name.Position = new Vector2(790, 0);
            this.SpritesInState.Add(this.uiFactory.Player2Name);

            this.uiFactory.HealthBarEmptyPlayer2.Position = new Vector2(945, 5);
            this.SpritesInState.Add(this.uiFactory.HealthBarEmptyPlayer2);

            this.uiFactory.HealthbarFullPlayer2.Position = new Vector2(950, 10);
            this.SpritesInState.Add(this.uiFactory.HealthbarFullPlayer2);

            this.uiFactory.SnowballBarEmptyPlayer2.Position = new Vector2(950, 45);
            this.SpritesInState.Add(this.uiFactory.SnowballBarEmptyPlayer2);

            this.uiFactory.SnowballBarFullPlayer2.Position = new Vector2(950, 45);
            this.SpritesInState.Add(this.uiFactory.SnowballBarFullPlayer2);

            //// Player animation
            //foreach (var animation in this.listOfPlayerAnimations)
            //{
            //    this.SpritesInState.Add(animation);
            //}
        }

        public override void Update()
        {
            base.Update();

            if (!this.isDone)
            {
                this.CheckForPause();
                this.CheckGameOver();


                this.UpdateHealthPacks();

                for (int i = 0; i < this.players.Count; i++)
                {
                    this.UpdateUI(i);
                    this.UpdatePlayer(i);
                    this.PlayerShoot(i);
                    this.CheckPlayerSnowPileCollision(i);
                }

                for (int i = 0; i < this.level.ListOfSnowballs.Count; i++)
                {
                    this.CheckSnowballBlockCollision(i);
                    this.CheckSnowballPlayerCollision(i);

                    this.UpdateSnowball(i);
                }
            }
        }

        private void CheckForPause()
        {
            foreach (var key in this.inputHandler.ActiveKeys)
            {
                if (key.Button == Keys.Escape && key.ButtonState == Utils.KeyState.Clicked)
                {
                    this.isDone = true;
                    this.NextState = new PausedState(this.inputHandler, this.uiFactory, this.soundManager, this.players);
                }
            }
        }

        private void CheckGameOver()
        {
            for (int i = 0; i < this.players.Count; i++)
            {
                if (this.players[i].Health <= 0)
                {
                    this.isDone = true;
                    this.NextState = new GameOverState(this.inputHandler, this.uiFactory, this.soundManager, i);
                }
            }
        }

        private void UpdateHealthPacks()
        {
            for (int i = 0; i < this.level.ListOfHealthPack.Count; i++)
            {
                HealthPack healthPack = this.level.ListOfHealthPack[i];

                this.healthPackAnimations[i].Update();
                this.healthPackAnimations[i].Position = healthPack.Position;

                healthPack.Bounds = new Rectangle((int)healthPack.Position.X, (int)healthPack.Position.Y, 
                    this.healthPackAnimations[i].SourceRectangle.Width, this.healthPackAnimations[i].SourceRectangle.Height);

                if (healthPack.IsActive)
                {
                   if (!healthPack.IsDrawn)
                    {
                        healthPack.IsDrawn = true;
                        this.SpritesInState.Add(this.healthPackAnimations[i]);
                    }

                    foreach (var player in this.players)
                    {
                        if (healthPack.Bounds.Intersects(player.Bounds))
                        {
                            player.AddHealth(healthPack.HealthToGive);
                            healthPack.ActOnCollision();
                        }
                    }
                }
                else
                {
                    if (healthPack.IsDrawn)
                    {
                        this.SpritesInState.Remove(this.healthPackAnimations[i]);
                        healthPack.IsDrawn = false;
                    }
                    else
                    {
                        healthPack.UpdateTimer();
                    }
                }
            }
        }

        private void CheckPlayerSnowPileCollision(int i)
        {
            foreach (var snowPile in this.level.ListOfSnowPiles)
            {
                if (snowPile.Bounds.Intersects(this.players[i].Bounds))
                {
                    snowPile.ActOnCollision();

                    if (snowPile.IsCharged)
                    {
                        this.players[i].AddSnowball();
                        snowPile.IsCharged = false;
                    }
                }
            }
        }

        private void UpdateUI(int i)
        {
            this.uiFactory.ListOfSnowballBars[i].SourceRectangle = new Rectangle(this.uiFactory.ListOfSnowballBars[i].SourceRectangle.X,
                this.uiFactory.ListOfSnowballBars[i].SourceRectangle.Y, 20 * this.players[i].Snowballs,
                this.uiFactory.ListOfSnowballBars[i].SourceRectangle.Height);

            this.uiFactory.ListOfHealthbars[i].SourceRectangle = new Rectangle(this.uiFactory.ListOfHealthbars[i].SourceRectangle.X,
                this.uiFactory.ListOfHealthbars[i].SourceRectangle.Y, 3 * this.players[i].Health, this.uiFactory.ListOfHealthbars[i].SourceRectangle.Height);
        }

        private void CheckSnowballPlayerCollision(int i)
        {
            foreach (var player in this.players)
            {
                if (this.level.ListOfSnowballs[i].Bounds.Intersects(player.Bounds))
                {
                    this.level.ListOfSnowballs[i].ActOnCollision();

                    player.Health -= this.level.ListOfSnowballs[i].Damage;
                }
            }
        }

        private void CheckSnowballBlockCollision(int i)
        {
            foreach (var block in this.level.Blocks)
            {
                if (this.level.ListOfSnowballs[i].Bounds.Intersects(block.Bounds))
                {
                    this.level.ListOfSnowballs[i].ActOnCollision();
                    this.soundManager.Play("SnowballHitBlock");
                }
            }
        }

        private void UpdateSnowball(int i)
        {
            Snowball snowball = this.level.ListOfSnowballs[i];

            if (!snowball.IsMelting)
            {
                snowball.Move();

                this.snowballSprites[i].Position = snowball.Position;

                snowball.Bounds = new Rectangle((int)snowball.Position.X, (int)snowball.Position.Y,
                    this.snowballSprites[i].Texture.Width, this.snowballSprites[i].Texture.Height);
            }
            else
            {
                this.SpritesInState.Remove(this.snowballSprites[i]);
                this.level.ListOfSnowballs.Remove(snowball);
                this.snowballSprites.Remove(this.snowballSprites[i]);
            }
        }

        private void UpdatePlayer(int i)
        {
            this.players[i].Move(this.level.Blocks, this.inputHandler.ActiveKeys);
            this.playerAnimations[i].Update();
            this.playerAnimations[i].Position = this.players[i].Position;
            this.playerAnimations[i].IsFacingRight = this.players[i].IsFacingRight;
            this.players[i].Bounds = new Rectangle((int)this.players[i].Position.X, (int)this.players[i].Position.Y,
                (int)this.playerAnimations[i].SourceRectangle.Width, (int)this.playerAnimations[i].SourceRectangle.Height);
            this.playerAnimations[i].ChangeAnimation(this.players[i].State.ToString());
        }

        private void PlayerShoot(int i)
        {
            if (this.players[i].IsShooting)
            {
                this.players[i].IsShooting = false;

                Vector2 snowballPosition;

                if (this.players[i].IsFacingRight)
                {
                    snowballPosition = new Vector2(this.players[i].Bounds.Right,
                        this.players[i].Position.Y + (this.players[i].Bounds.Height * 0.2f));
                }
                else
                {
                    snowballPosition = new Vector2(this.players[i].Bounds.Left - 40,
                        this.players[i].Position.Y + (this.players[i].Bounds.Height * 0.2f));
                }

                Snowball newSnowball = new Snowball(snowballPosition, this.players[i].IsFacingRight);
                this.level.ListOfSnowballs.Add(newSnowball);

                Sprite snowballSprite = UIFactory.CreateSprite("Snowball");
                this.snowballSprites.Add(snowballSprite);
                this.SpritesInState.Add(snowballSprite);
            }
        }
    }
}
