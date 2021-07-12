/// <reference path = "Lib/phaser.d.ts"/>
///<reference path="States\Loader.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
function randInterval(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}
var Worms;
(function (_Worms) {
    var Worms = (function (_super) {
        __extends(Worms, _super);
        function Worms(width, height) {
            var dpr = devicePixelRatio || 1;
            if (!width) {
                width = screen.width * dpr;
            }
            if (!height) {
                height = screen.height * dpr;
            }
            _super.call(this, width, height, Phaser.CANVAS, 'phaser-div', { create: this.create });
        }
        Worms.prototype.create = function () {
            this.game.state.add("Loader", _Worms.Loader, false);
            this.game.state.add("Boot", _Worms.Boot, false);
            this.game.state.add("Menu", _Worms.Menu, false);
            this.game.state.add("Game", _Worms.Game, false);
            this.game.state.start("Boot");
        };
        return Worms;
    })(Phaser.Game);
    window.onload = function () {
        new Worms(1280, 720);
    };
})(Worms || (Worms = {}));
//# sourceMappingURL=Worms.js.map