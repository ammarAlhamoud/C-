/// <reference path = "../Lib/phaser.d.ts"/>
///<reference path="Boot.ts"/>
///<reference path="Menu.ts"/>
///<reference path="Game.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Worms;
(function (Worms) {
    var Loader = (function (_super) {
        __extends(Loader, _super);
        function Loader() {
            _super.apply(this, arguments);
        }
        Loader.prototype.create = function () {
            var _this = this;
            this.loader = this.game.add.sprite(this.game.width / 2, this.game.height / 2, "loading");
            this.loader.anchor.set(0.5, 0.5);
            this.loader.scale.set(1.7, 1.7);
            this.loadingSound = this.game.add.sound("click");
            this.changeableValue = 1;
            this.animation = this.loader.animations.add("anim", null, 20);
            this.animation.play();
            this.animation.enableUpdate = true;
            this.animation.onUpdate.add(function () {
                _this.loadingSound.play();
                _this.loadingSound._sound.playbackRate.value = _this.changeableValue;
                _this.changeableValue += 0.02;
            }, this);
            this.animation.onComplete.add(function () {
                _this.game.state.start("Game");
            }, this);
        };
        return Loader;
    })(Phaser.State);
    Worms.Loader = Loader;
})(Worms || (Worms = {}));
//# sourceMappingURL=Loader.js.map