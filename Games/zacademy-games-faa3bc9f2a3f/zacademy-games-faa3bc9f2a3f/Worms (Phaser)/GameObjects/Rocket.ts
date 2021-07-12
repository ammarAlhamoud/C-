///<reference path="Weapon.ts"/>
module Worms{
    export class Rocket extends Weapon{
        orientation:number;
        constructor(game:Phaser.Game, startPos:Phaser.Point, endPos:Phaser.Point,
                    force:number, spriteKey:string,scale:number, orientation:number) {
            super(game,startPos,endPos,force,spriteKey,scale,orientation);
            this.orientation = orientation
        }

        update(){
            var oldPos = this.sprite.previousPosition;
            var currentPos = this.sprite.position;
            var angle = Math.atan((currentPos.y-oldPos.y)/(currentPos.x-oldPos.x));
            this.sprite.body.rotation = angle;

            this.sprite.body.velocity.x -=5*this.orientation;

            this.sprite.body.allowGravityScale = true;
            this.sprite.body.data.gravityScale = 0.8;
            console.log(this.sprite.body.gravity);
        }
    }
}