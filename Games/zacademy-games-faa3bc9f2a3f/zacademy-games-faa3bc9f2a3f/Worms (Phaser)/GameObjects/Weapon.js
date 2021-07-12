///<reference path = "../Lib/phaser.d.ts"/>
///<reference path = "../Lib/p2.d.ts"/>
var Worms;
(function (Worms) {
    var Weapon = (function () {
        function Weapon(game, startPos, endPos, force, spriteKey, scale, orientation) {
            this.game = game;
            this.MAX_FORCE = 1000;
            this.sprite = this.game.add.sprite(startPos.x, startPos.y, spriteKey);
            this.game.physics.p2.enable(this.sprite);
            this.sprite.body.setCircle(10 * scale);
            this.sprite.anchor.set(0.5, 0.5);
            this.sprite.scale.set(-orientation * scale, scale);
            var directionVector = new Phaser.Point(endPos.x - startPos.x, endPos.y - startPos.y);
            var directionUnitVector = directionVector.normalize();
            this.sprite.body.moveRight(directionUnitVector.x * force * this.MAX_FORCE);
            this.sprite.body.moveDown(directionUnitVector.y * force * this.MAX_FORCE);
            this.sprite.body.rotation = startPos.angle(endPos);
        }
        Weapon.prototype.update = function () {
        };
        return Weapon;
    })();
    Worms.Weapon = Weapon;
})(Worms || (Worms = {}));
//# sourceMappingURL=Weapon.js.map