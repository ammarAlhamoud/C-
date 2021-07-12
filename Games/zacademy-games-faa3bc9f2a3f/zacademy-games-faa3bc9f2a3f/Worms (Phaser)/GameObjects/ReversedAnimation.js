var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
/// <reference path = "../Lib/phaser.d.ts"/>
/// <reference path = "../Lib/p2.d.ts"/>
/// <reference path = "../Lib/phaser.comments.d.ts"/>
var Worms;
(function (Worms) {
    var ReversedAnimation = (function (_super) {
        __extends(ReversedAnimation, _super);
        function ReversedAnimation(game, x, y, name, frames, frameRate, loop, single) {
            var _this = this;
            this.game = game;
            _super.call(this, this.game, x, y, name);
            var arr = [];
            for (var i = 0; i < frames; i++) {
                arr.push(i);
            }
            var arrRev = [];
            for (var i = frameRate - 1; i >= 0; i--) {
                arrRev.push(i);
            }
            if (single === false) {
                arrRev = [0];
            }
            this.animations.add("in", arr, frameRate, false);
            this.animations.add("out", arrRev, frameRate, false);
            this.animations.getAnimation("in").onComplete.add(function () {
                _this.play("out");
            });
            if (loop) {
                this.animations.getAnimation("out").onComplete.add(function () {
                    _this.play("in");
                });
            }
            this.game.add.existing(this);
        }
        return ReversedAnimation;
    })(Phaser.Sprite);
    Worms.ReversedAnimation = ReversedAnimation;
})(Worms || (Worms = {}));
//# sourceMappingURL=ReversedAnimation.js.map