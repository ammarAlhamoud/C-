/// <reference path = "Lib/phaser.d.ts"/>
///<reference path="States\Loader.ts"/>

function randInterval(min, max) {
    return Math.floor(Math.random() * (max - min) + min);
}
module Worms {

    class Worms extends Phaser.Game {
        game:Phaser.Game;

        constructor(width?:number, height?:number) {
            var dpr = devicePixelRatio || 1;

            if (!width) {
                width = screen.width * dpr;
            }
            if (!height) {
                height = screen.height * dpr;
            }
            super(width, height, Phaser.CANVAS, 'phaser-div', {create: this.create});
        }

        create() {
            this.game.state.add("Loader", Loader, false);
            this.game.state.add("Boot", Boot, false);
            this.game.state.add("Menu", Menu, false);
            this.game.state.add("Game", Game, false);

            this.game.state.start("Boot");
        }


    }

    window.onload = ()=> {
        new Worms(1280, 720);
    }
}