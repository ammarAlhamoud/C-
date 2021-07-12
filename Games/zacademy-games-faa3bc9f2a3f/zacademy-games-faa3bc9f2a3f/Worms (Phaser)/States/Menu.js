/// <reference path = "../Lib/phaser.d.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Worms;
(function (Worms) {
    var Menu = (function (_super) {
        __extends(Menu, _super);
        function Menu() {
            _super.apply(this, arguments);
        }
        Menu.prototype.create = function () {
            this.background = this.game.add.image(0, 0, "bg");
            this.background.scale.set(this.game.width, 1);
            this.emitterCreate();
            this.clickSound = this.game.add.audio("click");
            this.hoverSound = this.game.add.audio("hover");
            this.logo = this.game.add.image(this.game.width / 2, this.game.height * 0.13, "worms-logo");
            this.logo.anchor.set(0.5, 0.5);
            this.playButtonGroup = this.game.add.group();
            this.playButtonCreate();
            this.quitButtonCreate();
        };
        Menu.prototype.playButtonCreate = function () {
            var _this = this;
            this.borderWidth = 4;
            this.playButton = this.game.add.sprite(this.game.width / 2, this.game.height * 0.6, "play-button");
            this.playButton.anchor.set(0.5, 0.5);
            this.playButton.scale.set(2, 2);
            this.bmd = this.game.make.bitmapData(this.playButton.width + this.borderWidth * 2, this.playButton.height + this.borderWidth * 2, "bmdBorder", true);
            this.bmd.ctx.strokeStyle = "#ffffff";
            this.bmd.ctx.lineWidth = this.borderWidth;
            this.bmd.ctx.lineJoin = "round";
            this.bmd.ctx.strokeRect(this.borderWidth / 2, this.borderWidth / 2, this.playButton.width + this.borderWidth, this.playButton.height + this.borderWidth);
            this.borderSprite = this.game.add.sprite(this.playButton.x, this.playButton.y, this.game.cache.getBitmapData("bmdBorder"));
            this.borderSprite.anchor.set(0.5, 0.5);
            this.borderSprite.tint = 0x737373;
            this.playButtonGroup.add(this.playButton);
            this.playButtonGroup.add(this.borderSprite);
            this.playButton.bringToTop();
            this.playButton.inputEnabled = true;
            this.playButton.events.onInputOver.add(function () {
                _this.hoverSound.play();
                _this.borderSprite.tint = 0xffffff;
            }, this);
            this.playButton.events.onInputOut.add(function () {
                _this.borderSprite.tint = 0x737373;
            }, this);
            this.playButton.events.onInputDown.add(function () {
                _this.clickSound.play();
                _this.game.state.start("Loader");
            }, this);
        };
        Menu.prototype.quitButtonCreate = function () {
            var _this = this;
            this.bmd = this.game.make.bitmapData(this.game.width * 0.15 + this.borderWidth * 2, this.game.height * 0.08 + this.borderWidth * 2, "bmdButton", true);
            this.bmd.ctx.strokeStyle = "#ffffff";
            this.bmd.ctx.fillStyle = "#000000";
            this.bmd.ctx.lineWidth = this.borderWidth;
            this.bmd.ctx.lineJoin = "round";
            this.bmd.ctx.fillRect(this.borderWidth / 2, this.borderWidth / 2, this.game.width * 0.15 + this.borderWidth, this.game.height * 0.08 + this.borderWidth);
            this.bmd.ctx.strokeRect(this.borderWidth / 2, this.borderWidth / 2, this.game.width * 0.15 + this.borderWidth, this.game.height * 0.08 + this.borderWidth);
            this.quitButton = this.game.add.sprite(this.game.width * 0.8, this.game.height * 0.9, this.game.cache.getBitmapData("bmdButton"));
            this.quitButton.anchor.set(0.5, 0.5);
            this.quitButton.tint = 0x737373;
            this.quitButton.inputEnabled = true;
            this.quitButton.events.onInputOver.add(function () {
                _this.hoverSound.play();
                _this.quitButton.tint = 0xffffff;
            }, this);
            this.quitButton.events.onInputOut.add(function () {
                _this.quitButton.tint = 0x737373;
            }, this);
            this.quitButton.events.onInputDown.add(function () {
                _this.clickSound.play();
                _this.game.destroy();
            }, this);
            this.quitText = this.game.add.text(this.quitButton.x, this.quitButton.y, "Quit", { font: "26px Montserrat-Regular", fill: "#737373" });
            this.quitText.anchor.set(0.5, 0.5);
        };
        Menu.prototype.emitterCreate = function () {
            this.starEmitter = this.game.add.emitter(0, 0, 200);
            this.starEmitter.width = this.game.width * 0.8;
            this.starEmitter.makeParticles(["star-white", "star-yellow"]);
            this.starEmitter.minParticleSpeed.set(200, 200);
            this.starEmitter.maxParticleSpeed.set(700, 600);
            this.starEmitter.minParticleScale = 0.3;
            this.starEmitter.maxParticleScale = 0.5;
            this.starEmitter.setRotation(30, 250);
            this.starEmitter.start(false, 3000, 200, 1000, false);
        };
        return Menu;
    })(Phaser.State);
    Worms.Menu = Menu;
})(Worms || (Worms = {}));
//# sourceMappingURL=Menu.js.map