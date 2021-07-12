///<reference path = "../Lib/phaser.d.ts"/>
///<reference path = "../Lib/p2.d.ts"/>
///<reference path="ReversedAnimation.ts"/>
///<reference path="..\Worms.ts"/>
///<reference path="Rocket.ts"/>

module Worms{
    export enum WormState{Idle,Move,EquipBazooka,EquipGrenade,Jump,Shoot}
    export class Worm{
        game:Phaser.Game;

        MAX_SHOOT_TWEEN_DURATION = 1200;

        wormBody:Phaser.Sprite;

        currentState:WormState;
        lastIdleState:WormState;
        isGrounded:boolean;

        bazooka:Phaser.Sprite;
        crosshair:Phaser.Sprite;

        breathe:ReversedAnimation;
        equipBazooka:ReversedAnimation;
        fly:Phaser.Sprite;
        jump:Phaser.Sprite;
        land:Phaser.Sprite;
        //unequipBazooka:ReversedAnimation;
        walk:Phaser.Sprite;

        idleAnimations:Array<ReversedAnimation>;
        allAnimations:Array<Phaser.Sprite>;

        walkInSound:Phaser.Sound;
        walkOutSound:Phaser.Sound;
        jumpSound:Phaser.Sound;

        idleEvent:Phaser.TimerEvent;
        keys:Array<Phaser.Key>;
        orientation:number;
        scale:number;
        currentWeaponRotation:number;
        crosshairDistance:number;
        bmd:Phaser.BitmapData;
        shootCircle:Phaser.Sprite;
        allCirclesShot:Array<Phaser.Sprite>;
        shootEvent:Phaser.TimerEvent;
        shootStopEvent:Phaser.TimerEvent;
        allProjectilesShot:Array<Weapon>;

        constructor(game:Phaser.Game,keys,levelCollisionGroup){
            this.game = game;
            this.MAX_SHOOT_TWEEN_DURATION = 1200;
            this.orientation = 1;
            this.scale = 1.5;
            this.isGrounded = false;
            this.currentWeaponRotation=0;
            this.crosshairDistance = this.game.height/6;
            this.lastIdleState = WormState.Idle;
            this.allCirclesShot = [];
            this.allProjectilesShot = [];

            //Weapons
            this.bazooka = this.game.add.sprite(500,300,"bazooka");
            this.bazooka.alpha = 0;
            this.bazooka.scale.set(this.scale,this.scale);
            this.bazooka.anchor.set(0.5,0.5);

            this.crosshair = this.game.add.sprite(500,500,"crossR");
            this.crosshair.alpha = 0;
            this.crosshair.anchor.set(0.5,0.5);

            this.wormBody = this.game.add.sprite(300,500);
            this.wormBody.anchor.set(0.5,0.5);
            this.game.physics.p2.enable(this.wormBody);
            this.wormBody.body.setCircle(10*this.scale);
            this.wormBody.body.angularVelocity = 0;
            this.wormBody.body.gravity.y = 1000;

            this.initiateIdleState();

            this.walk = this.game.add.sprite(500,500,"wWalk");
            this.jump = this.game.add.sprite(500,500,"wJump");
            this.fly = this.game.add.sprite(500,500,"wFly");
            this.equipBazooka = new ReversedAnimation(this.game, 500,500,"wEBazooka",7,30,false);
            this.equipBazooka.anchor.set(0.5,0.5);
            this.equipBazooka.scale.set(this.scale,this.scale);
            this.equipBazooka.alpha = 0;
            this.equipBazooka.animations.getAnimation("in").onComplete.removeAll();
            this.equipBazooka.animations.getAnimation("out").onComplete.removeAll();
            this.equipBazooka.animations.getAnimation("in").onComplete.add(()=>{
                this.bazooka.alpha = 1;
                this.bazooka.bringToTop();
            });
            this.equipBazooka.animations.getAnimation("out").onComplete.add(()=>{
                this.hideAllAnimations();
                this.breathe.alpha = 1;
                this.breathe.play("in");
                this.activateRandomIdleAnimation();
            });


            this.allAnimations = [];
            this.allAnimations.push(this.walk,this.jump,this.fly);
            this.allAnimations.forEach((anim)=>{
                anim.alpha = 0;
                anim.anchor.set(0.5,0.5);
                anim.scale.set(this.scale,this.scale);
                anim.animations.add("in");
            });
            this.allAnimations.push(this.breathe);
            this.idleAnimations.forEach((anim)=>{
               this.allAnimations.push(anim);
            });
            this.allAnimations.push(this.equipBazooka);

            this.walkInSound = this.game.add.audio("walkOut");
            this.walkOutSound = this.game.add.audio("walkIn");
            this.jumpSound = this.game.add.audio("jump");
            this.walkInSound.onStop.add(()=>{
               this.walkOutSound.play();
            },this);
            this.walkOutSound.onStop.add(()=>{
                this.walkInSound.play();
            },this);

            this.keys = [];
            //left, right, up, down, jump, shoot, equipZook, equipGrenade
            for (var i = 0; i < keys.length; i++) {
                this.keys.push(this.game.input.keyboard.addKey(keys[i]))
            }

            this.keys[0].onDown.add(()=>{
                this.changeToMoveState(1);
            });

            this.keys[1].onDown.add(()=>{
                this.changeToMoveState(-1);
            });
            this.keys[4].onDown.add(()=>{
                this.changeToJumpState();
            });
            this.keys[6].onDown.add(()=>{

                if (this.currentState!==WormState.EquipBazooka) {
                    this.equipBazookaState();
                }
                else{
                    this.unequipBazookaState();
                }
            });

            this.shootEvent = this.game.time.events.add(12,()=>{},this);
            this.shootEvent.timer.stop(true);
            this.keys[5].onDown.add(()=>{
                if (this.currentState === WormState.EquipBazooka) {
                    this.shootEvent.timer.loop(Phaser.Timer.SECOND*0.11,this.createShootCircle,this);
                    this.shootEvent.timer.add(this.MAX_SHOOT_TWEEN_DURATION+200,this.shootBazookaProjectile,this);
                    this.shootEvent.timer.start();
                }

            });
            this.keys[5].onUp.add(()=>{
                if (this.currentState === WormState.EquipBazooka) { //stop timer + calculate force + shoot + stop animations + (unequip bazooka?)
                   this.shootBazookaProjectile();
                }
            });



            this.game.physics.p2.onBeginContact.add(this.groundWorm,this);
            //this.game.physics.p2.onEndContact.add(this.unGroundWorm,this);
            this.wormBody.body.collides(levelCollisionGroup);
            this.wormBody.body.allowGravityScale = true;

            this.createShootCircleBMD();


        }

        update(){
            this.centerAnimationsAroundBody();
            this.wormBody.body.rotation = 0;
            this.wormBody.body.angle = 0;
            this.wormBody.body.angularDamping = 0;
            this.wormBody.body.angularVelocity = 0;

            if(this.keys[2].isDown && this.currentState===WormState.EquipBazooka){
                this.currentWeaponRotation+=0.05;
                if(this.currentWeaponRotation>Math.PI/2){
                    this.currentWeaponRotation = Math.PI/2
                }
            }
            if(this.keys[3].isDown && this.currentState===WormState.EquipBazooka){
                this.currentWeaponRotation-=0.05;
                if(this.currentWeaponRotation<-Math.PI/2){
                    this.currentWeaponRotation =-Math.PI/2
                }
            }

            this.bazooka.rotation = this.currentWeaponRotation*this.orientation;
            this.crosshair.rotation = this.currentWeaponRotation*this.orientation;

            if((this.keys[0].isDown || this.keys[1].isDown)&& this.currentState === WormState.Move){
                this.wormBody.body.velocity.x = -100*this.orientation;
                this.wormBody.body.static = false;
            }

            //Executes ONCE when going into IDLE state
            if(this.noKeyIsPressed() && this.currentState ===WormState.Move){
                this.currentState = this.lastIdleState;
                this.hideAllAnimations();
                this.walkInSound.onStop.active = false;
                this.walkOutSound.onStop.active = false;
                this.walkInSound.stop();
                this.walkOutSound.stop();
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                this.wormBody.body.static = true;


                if (this.currentState === WormState.Idle) {
                    this.breathe.alpha = 1;
                    this.breathe.play("in");
                    this.activateRandomIdleAnimation();
                }
                else if(this.currentState === WormState.EquipBazooka){
                    this.equipBazooka.alpha = 1;
                    this.equipBazooka.play("in");
                    this.crosshair.alpha = 1;
                }


            }
            this.allProjectilesShot.forEach((p)=>{
                p.update();
            });
        }

        initiateIdleState(){
            this.currentState = WormState.Idle;

            this.breathe = new ReversedAnimation(this.game,500,500,"wBreathe",20,30,true);
            this.breathe.play("in");
            this.breathe.anchor.set(0.5,0.5);
            this.breathe.scale.set(this.scale,this.scale);

            this.idleAnimations = [];

            this.idleAnimations.push(new ReversedAnimation(this.game,500,500,"wBlink",16,16,false));
            this.idleAnimations.push(new ReversedAnimation(this.game,500,500,"wGlance",15,15,false));
            this.idleAnimations.push(new ReversedAnimation(this.game,500,500,"wLookUp",15,15,false));
            this.idleAnimations.push(new ReversedAnimation(this.game,500,500,"wScratch",54,30,true,false));
            this.idleAnimations.push(new ReversedAnimation(this.game,500,500,"wStache",60,30,true,false));

            this.idleAnimations.forEach((anim)=>{
                anim.anchor.set(0.5,0.5);
                anim.alpha = 0;
                anim.scale.set(this.scale,this.scale);
            });
            this.idleAnimations[4].anchor.set(0.55,0.45);

            this.activateRandomIdleAnimation();
        }

        activateRandomIdleAnimation(){
            if(this.currentState === WormState.Idle){
                this.idleEvent = this.game.time.events.add(randInterval(5,10)*Phaser.Timer.SECOND,()=>{
                    if(this.currentState === WormState.Idle) {
                        this.breathe.alpha = 0;
                        this.breathe.animations.stop();

                        var randAnim = Math.floor(randInterval(0, this.idleAnimations.length));
                        this.idleAnimations[randAnim].alpha = 1;
                        this.idleAnimations[randAnim].play("in");
                        this.idleAnimations[randAnim].animations.getAnimation("out").onComplete.addOnce(()=> {
                            this.idleAnimations[randAnim].alpha = 0;
                            this.idleAnimations[randAnim].animations.stop();
                            this.breathe.alpha = 1;
                            this.breathe.play("in");
                            this.activateRandomIdleAnimation();
                        });
                    }
                },this);
            }
        }

        hideAllAnimations(){
            this.allAnimations.forEach((anim)=>{
                anim.alpha = 0;
                anim.animations.stop();
            });
            this.bazooka.alpha = 0;
            this.crosshair.alpha = 0;
        }

        centerAnimationsAroundBody(){
            this.allAnimations.forEach((anim)=>{
                anim.position.set(this.wormBody.x,this.wormBody.y);
            });

            this.bazooka.position.set(this.wormBody.x+4*this.orientation,this.wormBody.y+4);
            this.crosshair.position.x = this.wormBody.x - this.orientation*Math.cos(this.currentWeaponRotation*this.orientation)*this.crosshairDistance;
            this.crosshair.position.y = this.wormBody.y - this.orientation*Math.sin(this.currentWeaponRotation*this.orientation)*this.crosshairDistance;
        }

        changeAnimationsOrientation(){
            this.allAnimations.forEach((anim)=>{
                anim.scale.x = this.orientation*this.scale;
            });
            this.bazooka.scale.x = this.orientation*this.scale;
        }

        changeToMoveState(orientation:number){
            if (this.currentState !== WormState.Move && this.currentState!==WormState.Jump) {
                this.currentState = WormState.Move;
                this.orientation = orientation;
                this.idleEvent.callback = ()=> {
                };
                this.changeAnimationsOrientation();
                this.hideAllAnimations();
                this.walk.alpha = 1;
                this.walk.animations.play("in", 50, true);
                this.walkInSound.onStop.active = true;
                this.walkOutSound.onStop.active = true;
                this.walkInSound.play();
            }
        }

        groundWorm(){
            if(!this.isGrounded){
                this.hideAllAnimations();
                this.isGrounded = true;
                this.currentState = this.lastIdleState;
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                this.wormBody.body.static = true;
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                if (this.currentState === WormState.Idle) {
                    this.breathe.alpha = 1;
                    this.breathe.play("in");
                    this.activateRandomIdleAnimation();
                }
                else if(this.currentState === WormState.EquipBazooka){
                    this.equipBazooka.alpha = 1;
                    this.equipBazooka.play("in");
                    //this.crosshair.alpha = 1;
                }
            }
        }

        noKeyIsPressed(){
            for (var i = 0; i < this.keys.length; i++) {
                if(this.keys[i].isDown){
                    return false;
                }
            }
            return true;
        }

        changeToJumpState(){
            if(this.currentState!==WormState.Jump){
                this.currentState = WormState.Jump;
            this.idleEvent.callback = ()=>{};
                this.wormBody.body.static = false;
                this.hideAllAnimations();
                this.jump.alpha = 1;
                this.jump.play("in",45);
                this.jump.animations.getAnimation("in").onComplete.addOnce(()=>{
                this.jump.alpha = 0;
                this.isGrounded = false;
                this.wormBody.body.moveUp(350);
                this.wormBody.body.moveLeft(this.orientation*200);
                this.fly.alpha = 1;
                this.fly.play("in",20);
                this.jumpSound.play();
            },this);
            }
        }

        equipBazookaState(){
            if(this.currentState!==WormState.EquipBazooka){
                this.currentState = WormState.EquipBazooka;
                this.lastIdleState = this.currentState;
                this.idleEvent.callback = ()=>{};
                this.hideAllAnimations();
                this.equipBazooka.alpha = 1;
                this.equipBazooka.play("in");
                this.crosshair.alpha = 1;
            }
        }

        unequipBazookaState(){
            this.currentState = WormState.Idle;
            this.lastIdleState = this.currentState;
            this.bazooka.alpha = 0;
            this.crosshair.alpha = 0;
            this.equipBazooka.play("out");
        }

        createShootCircleBMD(){
            var width = Math.abs(this.crosshair.width);
            this.bmd = this.game.make.bitmapData(width,width,"shootCircle",true);
            this.bmd.ctx.fillStyle = "#FFFFFF";
            this.bmd.ctx.arc(width/2,width/2,width/2,0,2*Math.PI);
            this.bmd.ctx.fill();
        }

        createShootCircle(){
            this.shootCircle = this.game.add.sprite(this.wormBody.x,this.wormBody.y,this.game.cache.getBitmapData("shootCircle"));
            this.shootCircle.anchor.set(0.5,0.5);
            this.shootCircle.sendToBack();
            this.tweenShootCircle(this.shootCircle,0xaa0000,0xf4df68, this.MAX_SHOOT_TWEEN_DURATION);
            this.allCirclesShot.push(this.shootCircle);
        }

        tweenShootCircle(spriteToTween:Phaser.Sprite, startColour:number, endColour:number,duration){
            var colourBlend = {step:0};
            this.game.add.tween(colourBlend).to({step:100}, duration,Phaser.Easing.Default,false)
                .onUpdateCallback(()=>{
                  spriteToTween.tint = Phaser.Color.interpolateColor(startColour,endColour,100,colourBlend.step,1);
                })
            .start()
            .onComplete.addOnce(()=>{
                    spriteToTween.tint = endColour;
                    colourBlend = null;
                });

            spriteToTween.tint = startColour;

            this.game.add.tween(spriteToTween).to({x:this.crosshair.x,y:this.crosshair.y},duration,Phaser.Easing.Default,true);
            this.game.add.tween(spriteToTween.scale).from({x:0.35,y:0.35},duration,Phaser.Easing.Default,true);

        }

        shootBazookaProjectile(){
            this.allCirclesShot.forEach((c)=> {
                c.destroy();
                c = null;
            });
            this.allCirclesShot = [];
            this.unequipBazookaState();
            var elapsedShootTime = this.shootEvent.timer.ms;
            var shootForce = elapsedShootTime/(this.MAX_SHOOT_TWEEN_DURATION+200);
            this.allProjectilesShot.push(new Rocket(this.game,new Phaser.Point(this.wormBody.x,this.wormBody.y),
                new Phaser.Point(this.crosshair.x,this.crosshair.y),
                shootForce,"rocket",this.scale,this.orientation));
            console.log(shootForce);
            this.shootEvent.timer.stop(true);
        }

    }

}






