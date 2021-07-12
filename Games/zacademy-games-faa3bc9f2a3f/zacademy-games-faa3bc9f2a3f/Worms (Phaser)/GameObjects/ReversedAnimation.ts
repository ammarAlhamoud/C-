/// <reference path = "../Lib/phaser.d.ts"/>
/// <reference path = "../Lib/p2.d.ts"/>
/// <reference path = "../Lib/phaser.comments.d.ts"/>
module Worms{

    export class ReversedAnimation extends Phaser.Sprite{
        game:Phaser.Game;
        constructor(game:Phaser.Game,x:number, y:number, name:string,frames,frameRate,loop:boolean, single?:boolean){
            this.game = game;
            super(this.game,x,y,name);
            var arr = [];
            for (var i = 0; i < frames; i++) {
                arr.push(i);
            }
            var arrRev = [];
            for (var i = frameRate-1; i >= 0; i--) {
                arrRev.push(i);
            }
            if(single===false){
                arrRev = [0];
            }

            this.animations.add("in",arr,frameRate,false);
            this.animations.add("out",arrRev,frameRate,false);

            this.animations.getAnimation("in").onComplete.add(()=>{
                this.play("out");
            });
            if(loop) {
                this.animations.getAnimation("out").onComplete.add(()=> {
                    this.play("in");
                });
            }


            this.game.add.existing(this);

        }
    }

}