///<reference path = "../Lib/phaser.d.ts"/>
///<reference path = "../Lib/p2.d.ts"/>
///<reference path="ReversedAnimation.ts"/>
///<reference path="..\Worms.ts"/>
///<reference path="Rocket.ts"/>
var Worms;
(function (Worms) {
    (function (WormState) {
        WormState[WormState["Idle"] = 0] = "Idle";
        WormState[WormState["Move"] = 1] = "Move";
        WormState[WormState["EquipBazooka"] = 2] = "EquipBazooka";
        WormState[WormState["EquipGrenade"] = 3] = "EquipGrenade";
        WormState[WormState["Jump"] = 4] = "Jump";
        WormState[WormState["Shoot"] = 5] = "Shoot";
    })(Worms.WormState || (Worms.WormState = {}));
    var WormState = Worms.WormState;
    var Worm = (function () {
        function Worm(game, keys, levelCollisionGroup) {
            var _this = this;
            this.MAX_SHOOT_TWEEN_DURATION = 1200;
            this.game = game;
            this.MAX_SHOOT_TWEEN_DURATION = 1200;
            this.orientation = 1;
            this.scale = 1.5;
            this.isGrounded = false;
            this.currentWeaponRotation = 0;
            this.crosshairDistance = this.game.height / 6;
            this.lastIdleState = 0 /* Idle */;
            this.allCirclesShot = [];
            this.allProjectilesShot = [];
            //Weapons
            this.bazooka = this.game.add.sprite(500, 300, "bazooka");
            this.bazooka.alpha = 0;
            this.bazooka.scale.set(this.scale, this.scale);
            this.bazooka.anchor.set(0.5, 0.5);
            this.crosshair = this.game.add.sprite(500, 500, "crossR");
            this.crosshair.alpha = 0;
            this.crosshair.anchor.set(0.5, 0.5);
            this.wormBody = this.game.add.sprite(300, 500);
            this.wormBody.anchor.set(0.5, 0.5);
            this.game.physics.p2.enable(this.wormBody);
            this.wormBody.body.setCircle(10 * this.scale);
            this.wormBody.body.angularVelocity = 0;
            this.wormBody.body.gravity.y = 1000;
            this.initiateIdleState();
            this.walk = this.game.add.sprite(500, 500, "wWalk");
            this.jump = this.game.add.sprite(500, 500, "wJump");
            this.fly = this.game.add.sprite(500, 500, "wFly");
            this.equipBazooka = new Worms.ReversedAnimation(this.game, 500, 500, "wEBazooka", 7, 30, false);
            this.equipBazooka.anchor.set(0.5, 0.5);
            this.equipBazooka.scale.set(this.scale, this.scale);
            this.equipBazooka.alpha = 0;
            this.equipBazooka.animations.getAnimation("in").onComplete.removeAll();
            this.equipBazooka.animations.getAnimation("out").onComplete.removeAll();
            this.equipBazooka.animations.getAnimation("in").onComplete.add(function () {
                _this.bazooka.alpha = 1;
                _this.bazooka.bringToTop();
            });
            this.equipBazooka.animations.getAnimation("out").onComplete.add(function () {
                _this.hideAllAnimations();
                _this.breathe.alpha = 1;
                _this.breathe.play("in");
                _this.activateRandomIdleAnimation();
            });
            this.allAnimations = [];
            this.allAnimations.push(this.walk, this.jump, this.fly);
            this.allAnimations.forEach(function (anim) {
                anim.alpha = 0;
                anim.anchor.set(0.5, 0.5);
                anim.scale.set(_this.scale, _this.scale);
                anim.animations.add("in");
            });
            this.allAnimations.push(this.breathe);
            this.idleAnimations.forEach(function (anim) {
                _this.allAnimations.push(anim);
            });
            this.allAnimations.push(this.equipBazooka);
            this.walkInSound = this.game.add.audio("walkOut");
            this.walkOutSound = this.game.add.audio("walkIn");
            this.jumpSound = this.game.add.audio("jump");
            this.walkInSound.onStop.add(function () {
                _this.walkOutSound.play();
            }, this);
            this.walkOutSound.onStop.add(function () {
                _this.walkInSound.play();
            }, this);
            this.keys = [];
            for (var i = 0; i < keys.length; i++) {
                this.keys.push(this.game.input.keyboard.addKey(keys[i]));
            }
            this.keys[0].onDown.add(function () {
                _this.changeToMoveState(1);
            });
            this.keys[1].onDown.add(function () {
                _this.changeToMoveState(-1);
            });
            this.keys[4].onDown.add(function () {
                _this.changeToJumpState();
            });
            this.keys[6].onDown.add(function () {
                if (_this.currentState !== 2 /* EquipBazooka */) {
                    _this.equipBazookaState();
                }
                else {
                    _this.unequipBazookaState();
                }
            });
            this.shootEvent = this.game.time.events.add(12, function () {
            }, this);
            this.shootEvent.timer.stop(true);
            this.keys[5].onDown.add(function () {
                if (_this.currentState === 2 /* EquipBazooka */) {
                    _this.shootEvent.timer.loop(Phaser.Timer.SECOND * 0.11, _this.createShootCircle, _this);
                    _this.shootEvent.timer.add(_this.MAX_SHOOT_TWEEN_DURATION + 200, _this.shootBazookaProjectile, _this);
                    _this.shootEvent.timer.start();
                }
            });
            this.keys[5].onUp.add(function () {
                if (_this.currentState === 2 /* EquipBazooka */) {
                    _this.shootBazookaProjectile();
                }
            });
            this.game.physics.p2.onBeginContact.add(this.groundWorm, this);
            //this.game.physics.p2.onEndContact.add(this.unGroundWorm,this);
            this.wormBody.body.collides(levelCollisionGroup);
            this.wormBody.body.allowGravityScale = true;
            this.createShootCircleBMD();
        }
        Worm.prototype.update = function () {
            this.centerAnimationsAroundBody();
            this.wormBody.body.rotation = 0;
            this.wormBody.body.angle = 0;
            this.wormBody.body.angularDamping = 0;
            this.wormBody.body.angularVelocity = 0;
            if (this.keys[2].isDown && this.currentState === 2 /* EquipBazooka */) {
                this.currentWeaponRotation += 0.05;
                if (this.currentWeaponRotation > Math.PI / 2) {
                    this.currentWeaponRotation = Math.PI / 2;
                }
            }
            if (this.keys[3].isDown && this.currentState === 2 /* EquipBazooka */) {
                this.currentWeaponRotation -= 0.05;
                if (this.currentWeaponRotation < -Math.PI / 2) {
                    this.currentWeaponRotation = -Math.PI / 2;
                }
            }
            this.bazooka.rotation = this.currentWeaponRotation * this.orientation;
            this.crosshair.rotation = this.currentWeaponRotation * this.orientation;
            if ((this.keys[0].isDown || this.keys[1].isDown) && this.currentState === 1 /* Move */) {
                this.wormBody.body.velocity.x = -100 * this.orientation;
                this.wormBody.body.static = false;
            }
            //Executes ONCE when going into IDLE state
            if (this.noKeyIsPressed() && this.currentState === 1 /* Move */) {
                this.currentState = this.lastIdleState;
                this.hideAllAnimations();
                this.walkInSound.onStop.active = false;
                this.walkOutSound.onStop.active = false;
                this.walkInSound.stop();
                this.walkOutSound.stop();
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                this.wormBody.body.static = true;
                if (this.currentState === 0 /* Idle */) {
                    this.breathe.alpha = 1;
                    this.breathe.play("in");
                    this.activateRandomIdleAnimation();
                }
                else if (this.currentState === 2 /* EquipBazooka */) {
                    this.equipBazooka.alpha = 1;
                    this.equipBazooka.play("in");
                    this.crosshair.alpha = 1;
                }
            }
            this.allProjectilesShot.forEach(function (p) {
                p.update();
            });
        };
        Worm.prototype.initiateIdleState = function () {
            var _this = this;
            this.currentState = 0 /* Idle */;
            this.breathe = new Worms.ReversedAnimation(this.game, 500, 500, "wBreathe", 20, 30, true);
            this.breathe.play("in");
            this.breathe.anchor.set(0.5, 0.5);
            this.breathe.scale.set(this.scale, this.scale);
            this.idleAnimations = [];
            this.idleAnimations.push(new Worms.ReversedAnimation(this.game, 500, 500, "wBlink", 16, 16, false));
            this.idleAnimations.push(new Worms.ReversedAnimation(this.game, 500, 500, "wGlance", 15, 15, false));
            this.idleAnimations.push(new Worms.ReversedAnimation(this.game, 500, 500, "wLookUp", 15, 15, false));
            this.idleAnimations.push(new Worms.ReversedAnimation(this.game, 500, 500, "wScratch", 54, 30, true, false));
            this.idleAnimations.push(new Worms.ReversedAnimation(this.game, 500, 500, "wStache", 60, 30, true, false));
            this.idleAnimations.forEach(function (anim) {
                anim.anchor.set(0.5, 0.5);
                anim.alpha = 0;
                anim.scale.set(_this.scale, _this.scale);
            });
            this.idleAnimations[4].anchor.set(0.55, 0.45);
            this.activateRandomIdleAnimation();
        };
        Worm.prototype.activateRandomIdleAnimation = function () {
            var _this = this;
            if (this.currentState === 0 /* Idle */) {
                this.idleEvent = this.game.time.events.add(randInterval(5, 10) * Phaser.Timer.SECOND, function () {
                    if (_this.currentState === 0 /* Idle */) {
                        _this.breathe.alpha = 0;
                        _this.breathe.animations.stop();
                        var randAnim = Math.floor(randInterval(0, _this.idleAnimations.length));
                        _this.idleAnimations[randAnim].alpha = 1;
                        _this.idleAnimations[randAnim].play("in");
                        _this.idleAnimations[randAnim].animations.getAnimation("out").onComplete.addOnce(function () {
                            _this.idleAnimations[randAnim].alpha = 0;
                            _this.idleAnimations[randAnim].animations.stop();
                            _this.breathe.alpha = 1;
                            _this.breathe.play("in");
                            _this.activateRandomIdleAnimation();
                        });
                    }
                }, this);
            }
        };
        Worm.prototype.hideAllAnimations = function () {
            this.allAnimations.forEach(function (anim) {
                anim.alpha = 0;
                anim.animations.stop();
            });
            this.bazooka.alpha = 0;
            this.crosshair.alpha = 0;
        };
        Worm.prototype.centerAnimationsAroundBody = function () {
            var _this = this;
            this.allAnimations.forEach(function (anim) {
                anim.position.set(_this.wormBody.x, _this.wormBody.y);
            });
            this.bazooka.position.set(this.wormBody.x + 4 * this.orientation, this.wormBody.y + 4);
            this.crosshair.position.x = this.wormBody.x - this.orientation * Math.cos(this.currentWeaponRotation * this.orientation) * this.crosshairDistance;
            this.crosshair.position.y = this.wormBody.y - this.orientation * Math.sin(this.currentWeaponRotation * this.orientation) * this.crosshairDistance;
        };
        Worm.prototype.changeAnimationsOrientation = function () {
            var _this = this;
            this.allAnimations.forEach(function (anim) {
                anim.scale.x = _this.orientation * _this.scale;
            });
            this.bazooka.scale.x = this.orientation * this.scale;
        };
        Worm.prototype.changeToMoveState = function (orientation) {
            if (this.currentState !== 1 /* Move */ && this.currentState !== 4 /* Jump */) {
                this.currentState = 1 /* Move */;
                this.orientation = orientation;
                this.idleEvent.callback = function () {
                };
                this.changeAnimationsOrientation();
                this.hideAllAnimations();
                this.walk.alpha = 1;
                this.walk.animations.play("in", 50, true);
                this.walkInSound.onStop.active = true;
                this.walkOutSound.onStop.active = true;
                this.walkInSound.play();
            }
        };
        Worm.prototype.groundWorm = function () {
            if (!this.isGrounded) {
                this.hideAllAnimations();
                this.isGrounded = true;
                this.currentState = this.lastIdleState;
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                this.wormBody.body.static = true;
                this.wormBody.body.velocity.x = 0;
                this.wormBody.body.velocity.y = 0;
                if (this.currentState === 0 /* Idle */) {
                    this.breathe.alpha = 1;
                    this.breathe.play("in");
                    this.activateRandomIdleAnimation();
                }
                else if (this.currentState === 2 /* EquipBazooka */) {
                    this.equipBazooka.alpha = 1;
                    this.equipBazooka.play("in");
                }
            }
        };
        Worm.prototype.noKeyIsPressed = function () {
            for (var i = 0; i < this.keys.length; i++) {
                if (this.keys[i].isDown) {
                    return false;
                }
            }
            return true;
        };
        Worm.prototype.changeToJumpState = function () {
            var _this = this;
            if (this.currentState !== 4 /* Jump */) {
                this.currentState = 4 /* Jump */;
                this.idleEvent.callback = function () {
                };
                this.wormBody.body.static = false;
                this.hideAllAnimations();
                this.jump.alpha = 1;
                this.jump.play("in", 45);
                this.jump.animations.getAnimation("in").onComplete.addOnce(function () {
                    _this.jump.alpha = 0;
                    _this.isGrounded = false;
                    _this.wormBody.body.moveUp(350);
                    _this.wormBody.body.moveLeft(_this.orientation * 200);
                    _this.fly.alpha = 1;
                    _this.fly.play("in", 20);
                    _this.jumpSound.play();
                }, this);
            }
        };
        Worm.prototype.equipBazookaState = function () {
            if (this.currentState !== 2 /* EquipBazooka */) {
                this.currentState = 2 /* EquipBazooka */;
                this.lastIdleState = this.currentState;
                this.idleEvent.callback = function () {
                };
                this.hideAllAnimations();
                this.equipBazooka.alpha = 1;
                this.equipBazooka.play("in");
                this.crosshair.alpha = 1;
            }
        };
        Worm.prototype.unequipBazookaState = function () {
            this.currentState = 0 /* Idle */;
            this.lastIdleState = this.currentState;
            this.bazooka.alpha = 0;
            this.crosshair.alpha = 0;
            this.equipBazooka.play("out");
        };
        Worm.prototype.createShootCircleBMD = function () {
            var width = Math.abs(this.crosshair.width);
            this.bmd = this.game.make.bitmapData(width, width, "shootCircle", true);
            this.bmd.ctx.fillStyle = "#FFFFFF";
            this.bmd.ctx.arc(width / 2, width / 2, width / 2, 0, 2 * Math.PI);
            this.bmd.ctx.fill();
        };
        Worm.prototype.createShootCircle = function () {
            this.shootCircle = this.game.add.sprite(this.wormBody.x, this.wormBody.y, this.game.cache.getBitmapData("shootCircle"));
            this.shootCircle.anchor.set(0.5, 0.5);
            this.shootCircle.sendToBack();
            this.tweenShootCircle(this.shootCircle, 0xaa0000, 0xf4df68, this.MAX_SHOOT_TWEEN_DURATION);
            this.allCirclesShot.push(this.shootCircle);
        };
        Worm.prototype.tweenShootCircle = function (spriteToTween, startColour, endColour, duration) {
            var colourBlend = { step: 0 };
            this.game.add.tween(colourBlend).to({ step: 100 }, duration, Phaser.Easing.Default, false).onUpdateCallback(function () {
                spriteToTween.tint = Phaser.Color.interpolateColor(startColour, endColour, 100, colourBlend.step, 1);
            }).start().onComplete.addOnce(function () {
                spriteToTween.tint = endColour;
                colourBlend = null;
            });
            spriteToTween.tint = startColour;
            this.game.add.tween(spriteToTween).to({ x: this.crosshair.x, y: this.crosshair.y }, duration, Phaser.Easing.Default, true);
            this.game.add.tween(spriteToTween.scale).from({ x: 0.35, y: 0.35 }, duration, Phaser.Easing.Default, true);
        };
        Worm.prototype.shootBazookaProjectile = function () {
            this.allCirclesShot.forEach(function (c) {
                c.destroy();
                c = null;
            });
            this.allCirclesShot = [];
            this.unequipBazookaState();
            var elapsedShootTime = this.shootEvent.timer.ms;
            var shootForce = elapsedShootTime / (this.MAX_SHOOT_TWEEN_DURATION + 200);
            this.allProjectilesShot.push(new Worms.Rocket(this.game, new Phaser.Point(this.wormBody.x, this.wormBody.y), new Phaser.Point(this.crosshair.x, this.crosshair.y), shootForce, "rocket", this.scale, this.orientation));
            console.log(shootForce);
            this.shootEvent.timer.stop(true);
        };
        return Worm;
    })();
    Worms.Worm = Worm;
})(Worms || (Worms = {}));
//# sourceMappingURL=Worm.js.map