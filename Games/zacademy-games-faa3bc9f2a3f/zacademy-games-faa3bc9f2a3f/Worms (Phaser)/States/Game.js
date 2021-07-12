/// <reference path = "../Lib/phaser.d.ts"/>
/// <reference path = "../Lib/p2.d.ts"/>
///<reference path="..\GameObjects\Worm.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Worms;
(function (Worms) {
    var Game = (function (_super) {
        __extends(Game, _super);
        function Game() {
            _super.apply(this, arguments);
        }
        Game.prototype.create = function () {
            this.game.physics.startSystem(Phaser.Physics.P2JS);
            this.game.physics.p2.setImpactEvents(true);
            this.levelCollisionGroup = this.game.physics.p2.createCollisionGroup();
            this.wormsCollisionGroup = this.game.physics.p2.createCollisionGroup();
            this.game.physics.p2.updateBoundsCollisionGroup();
            this.game.physics.p2.gravity.y = 1000;
            this.game.physics.p2.friction = 1;
            //this.game.physics.p2.restitution=1;
            this.game.physics.p2.restitution = 0;
            this.game.physics.p2.world.defaultContactMaterial.friction = 1;
            this.game.physics.p2.world.setGlobalStiffness(1e5);
            this.level = this.game.add.sprite(this.game.width / 2, this.game.height * 0.7, "level-0");
            this.game.physics.p2.enable(this.level);
            this.level.x = this.game.width - this.level.width / 2;
            this.level.y = this.game.height - this.level.height / 2;
            this.level.body.kinematic = true;
            this.level.body.clearShapes();
            this.level.body.loadPolygon('physicsData', 'Level0');
            this.worm1 = new Worms.Worm(this.game, [Phaser.Keyboard.LEFT, Phaser.Keyboard.RIGHT, Phaser.Keyboard.UP, Phaser.Keyboard.DOWN, Phaser.Keyboard.ENTER, Phaser.Keyboard.SPACEBAR, Phaser.Keyboard.F1, Phaser.Keyboard.F2], this.levelCollisionGroup);
            this.worm1.wormBody.body.setZeroDamping();
            this.worm1.wormBody.body.setCollisionGroup(this.wormsCollisionGroup);
            this.level.body.setCollisionGroup(this.levelCollisionGroup);
            this.level.body.collides(this.wormsCollisionGroup);
            this.wormsMaterial = this.game.physics.p2.createMaterial("wormMaterial", this.worm1.wormBody.body);
            this.levelMaterial = this.game.physics.p2.createMaterial("wormMaterial", this.level.body);
            this.contactMaterial = this.game.physics.p2.createContactMaterial(this.wormsMaterial, this.levelMaterial);
            this.contactMaterial.friction = 1; // Friction to use in the contact of these two materials.
            this.contactMaterial.restitution = 0; // Restitution (i.e. how bouncy it is!) to use in the contact of these two materials.
            this.contactMaterial.stiffness = 1e7; // Stiffness of the resulting ContactEquation that this ContactMaterial generate.
            this.contactMaterial.relaxation = 1e7; // Relaxation of the resulting ContactEquation that this ContactMaterial generate.
            this.contactMaterial.frictionStiffness = 1; // Stiffness of the resulting FrictionEquation that this ContactMaterial
        };
        Game.prototype.update = function () {
            this.worm1.update();
        };
        return Game;
    })(Phaser.State);
    Worms.Game = Game;
})(Worms || (Worms = {}));
//# sourceMappingURL=Game.js.map