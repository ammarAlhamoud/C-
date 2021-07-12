/// <reference path = "../Lib/phaser.d.ts"/>

module Worms{
    export class Boot extends Phaser.State{
        background:Phaser.Image;
        gameLogo:Phaser.Image;
        preload(){
            //MENU SPRITES
            this.game.load.image("star-white","Graphics/Menu/star_white.png");
            this.game.load.image("star-yellow","Graphics/Menu/star_yellow.png");
            this.game.load.image("play-button","Graphics/Menu/icon.jpg");
            this.game.load.image("bg","Graphics/Menu/bg.png");
            this.game.load.image("worms-logo","Graphics/Boot/worms_logo.png");
            //MENU SOUNDS
            this.game.load.audio("click","Sounds/Menu/click.wav");
            this.game.load.audio("hover","Sounds/Menu/hover.wav");
            this.game.load.audio("walkIn","Sounds/Game/walk-in.wav");
            this.game.load.audio("walkOut","Sounds/Game/walk-out.wav");
            this.game.load.audio("jump","Sounds/Game/jump.wav");

            //LOADING SPRITES
            this.game.load.spritesheet("loading","Graphics/loading.png",160,160);
            this.game.load.image("level-0","Graphics/Game/Level0.png");
            this.game.load.image("bazooka","Graphics/Game/weapons/bazooka.png");
            this.game.load.image("rocket","Graphics/Game/weapons/rocket.png");
            this.game.load.image("crossR","Graphics/Game/Misc/crossR.png");

            //WORMS SPRITES
            this.game.load.spritesheet("wBlink","Graphics/Game/Worm/blink.png",24,27);
            this.game.load.spritesheet("wEBazooka","Graphics/Game/Worm/equipBazooka.png",36,27);
            this.game.load.spritesheet("wFly","Graphics/Game/Worm/fly.png",17,34);
            this.game.load.spritesheet("wGlance","Graphics/Game/Worm/glance.png",24,27);
            this.game.load.spritesheet("wJump","Graphics/Game/Worm/jump.png",24,31);
            this.game.load.spritesheet("wLand","Graphics/Game/Worm/land.png",24,27);
            this.game.load.spritesheet("wLookUp","Graphics/Game/Worm/looku.png",24,27);
            this.game.load.spritesheet("wScratch","Graphics/Game/Worm/scratch.png",24,27);
            this.game.load.spritesheet("wBreathe","Graphics/Game/Worm/breathe.png",25,28);
            this.game.load.spritesheet("wStache","Graphics/Game/Worm/mustache.png",28,26);
            this.game.load.spritesheet("wWalk","Graphics/Game/Worm/walk.png",31,29);

            this.game.load.physics('physicsData', 'Graphics/Game/level0.json');
        }

        create() {
            //this.game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
            this.background = this.game.add.image(0, 0, "bg");
            this.background.scale.set(this.game.width, 1);

            this.gameLogo = this.game.add.image(this.game.width / 2, 0, "worms-logo");
            this.gameLogo.anchor.set(0.5, 0.5);
            this.gameLogo.scale.set(1.5, 1.5);
            this.gameLogo.alpha = 0;
            this.game.add.tween(this.gameLogo)
                .to({y: this.game.height / 2}, 1000, Phaser.Easing.Bounce.Out, true);
            this.game.add.tween(this.gameLogo)
                .to({alpha: 1}, 300, Phaser.Easing.Default, true, 0, 0, false)
                .onComplete.add(()=> {
                    this.game.add.tween(this.gameLogo)
                        .to({alpha: 0}, 500, Phaser.Easing.Linear.None, true, 1500, 0, false)
                        .onComplete.add(()=> {
                            this.game.state.start("Menu");
                        });
                });
        }
    }
}