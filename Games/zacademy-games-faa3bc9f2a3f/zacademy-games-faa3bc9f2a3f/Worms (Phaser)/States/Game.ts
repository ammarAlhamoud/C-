/// <reference path = "../Lib/phaser.d.ts"/>
/// <reference path = "../Lib/p2.d.ts"/>
///<reference path="..\GameObjects\Worm.ts"/>

module Worms{
    export class Game extends Phaser.State{
        level:Phaser.Sprite;
        worm1:Worm;

        levelCollisionGroup;
        wormsCollisionGroup;

        wormsMaterial;
        levelMaterial;
        contactMaterial;
        create(){
            this.game.physics.startSystem(Phaser.Physics.P2JS);
            this.game.physics.p2.setImpactEvents(true);
            this.levelCollisionGroup = this.game.physics.p2.createCollisionGroup();
            this.wormsCollisionGroup = this.game.physics.p2.createCollisionGroup();
            this.game.physics.p2.updateBoundsCollisionGroup();
            this.game.physics.p2.gravity.y = 1000;
            this.game.physics.p2.friction = 1;
            //this.game.physics.p2.restitution=1;
            this.game.physics.p2.restitution=0;

            this.game.physics.p2.world.defaultContactMaterial.friction = 1;
            this.game.physics.p2.world.setGlobalStiffness(1e5);

            this.level = this.game.add.sprite(this.game.width/2,this.game.height*0.7,"level-0");
            this.game.physics.p2.enable(this.level);
            this.level.x = this.game.width - this.level.width/2;
            this.level.y = this.game.height - this.level.height/2;
            this.level.body.kinematic = true;
            this.level.body.clearShapes();
            this.level.body.loadPolygon('physicsData','Level0');

            this.worm1 = new Worm(this.game,[Phaser.Keyboard.LEFT,Phaser.Keyboard.RIGHT,Phaser.Keyboard.UP,Phaser.Keyboard.DOWN,Phaser.Keyboard.ENTER,
                Phaser.Keyboard.SPACEBAR,Phaser.Keyboard.F1,Phaser.Keyboard.F2],this.levelCollisionGroup);
            this.worm1.wormBody.body.setZeroDamping();


            this.worm1.wormBody.body.setCollisionGroup(this.wormsCollisionGroup);
            this.level.body.setCollisionGroup(this.levelCollisionGroup);
            this.level.body.collides(this.wormsCollisionGroup);

            this.wormsMaterial = this.game.physics.p2.createMaterial("wormMaterial",this.worm1.wormBody.body);
            this.levelMaterial = this.game.physics.p2.createMaterial("wormMaterial",this.level.body);

            this.contactMaterial = this.game.physics.p2.createContactMaterial(this.wormsMaterial,this.levelMaterial);

            this.contactMaterial.friction = 1;     // Friction to use in the contact of these two materials.
            this.contactMaterial.restitution = 0;  // Restitution (i.e. how bouncy it is!) to use in the contact of these two materials.
            this.contactMaterial.stiffness = 1e7;    // Stiffness of the resulting ContactEquation that this ContactMaterial generate.
            this.contactMaterial.relaxation = 1e7;     // Relaxation of the resulting ContactEquation that this ContactMaterial generate.
            this.contactMaterial.frictionStiffness = 1;    // Stiffness of the resulting FrictionEquation that this ContactMaterial

        }

        update(){
            this.worm1.update();
        }


        //render(){
        //    if(this.worm1){
        //        this.game.debug.text("Rotation: "+this.worm1.currentWeaponRotation.toString(),20,20,"#FFFFFF", "26px Arial");
        //        this.game.debug.text("State: "+this.worm1.currentState.toString(),20,50,"#FFFFFF", "26px Arial");
        //    }
        //}
    }
}