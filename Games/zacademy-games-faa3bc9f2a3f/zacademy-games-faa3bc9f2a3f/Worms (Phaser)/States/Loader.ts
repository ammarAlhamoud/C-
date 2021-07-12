/// <reference path = "../Lib/phaser.d.ts"/>
///<reference path="Boot.ts"/>
///<reference path="Menu.ts"/>
///<reference path="Game.ts"/>

module Worms{
    export class Loader extends Phaser.State{

        loader:Phaser.Sprite;
        animation: Phaser.Animation;
        loadingSound:Phaser.Sound;
        changeableValue:number;
        create(){
            this.loader = this.game.add.sprite(this.game.width/2,this.game.height/2,"loading");
            this.loader.anchor.set(0.5,0.5);
            this.loader.scale.set(1.7,1.7);
            this.loadingSound = this.game.add.sound("click");
            this.changeableValue = 1;

            this.animation = this.loader.animations.add("anim",null,20);

            this.animation.play();

            this.animation.enableUpdate=true;

            this.animation.onUpdate.add(()=>{
                this.loadingSound.play();
                this.loadingSound._sound.playbackRate.value= this.changeableValue;
                this.changeableValue+=0.02;
            },this);

            this.animation.onComplete.add(()=>{
                this.game.state.start("Game");
            },this)


        }
    }
}