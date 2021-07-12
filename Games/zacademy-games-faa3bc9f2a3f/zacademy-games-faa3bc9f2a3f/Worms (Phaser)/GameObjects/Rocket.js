var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
///<reference path="Weapon.ts"/>
var Worms;
(function (Worms) {
    var Rocket = (function (_super) {
        __extends(Rocket, _super);
        function Rocket(game, startPos, endPos, force, spriteKey, scale, orientation) {
            _super.call(this, game, startPos, endPos, force, spriteKey, scale, orientation);
            this.orientation = orientation;
        }
        Rocket.prototype.update = function () {
            var oldPos = this.sprite.previousPosition;
            var currentPos = this.sprite.position;
            var angle = Math.atan((currentPos.y - oldPos.y) / (currentPos.x - oldPos.x));
            this.sprite.body.rotation = angle;
            this.sprite.body.velocity.x -= 5 * this.orientation;
            this.sprite.body.allowGravityScale = true;
            this.sprite.body.data.gravityScale = 0.8;
            console.log(this.sprite.body.gravity);
        };
        return Rocket;
    })(Worms.Weapon);
    Worms.Rocket = Rocket;
})(Worms || (Worms = {}));
//# sourceMappingURL=Rocket.js.map