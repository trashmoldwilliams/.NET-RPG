﻿$(document).ready(function () {
    //define as user
    var player1 = $("#player1").val();

    //define as set enemy
    var player2 = $("#player2").val();

        
    var posting = $.post("/GetFighters", { player1: player1, player2: player2 });
    posting.done(function (data) {
        var player1 = $(data).find("#player1");
        var player2 = $(data).find("#player2");

        player1 = new Fighter(player1.attr("name"), parseInt(player1.attr("healthpoints")), parseInt(player1.attr("attack")));
        player2 = new Fighter(player2.attr("name"), parseInt(player2.attr("healthpoints")), parseInt(player2.attr("attack")));

        //draw player status
        player1.draw();
        player2.draw();

        battle = new Battle(player1, player2);
        $("#jab").click(function () {
            battle.AddMoves(jab, battle.AI());
            battle.ExecuteMove(battle.isFirst, battle.isSecond);
            battle.ExecuteMove(battle.isSecond, battle.isFirst);
            player1.draw();
            player2.draw();
            battle.checkDead();
        });

        $("#hook").click(function () {
            battle.AddMoves(hook, battle.AI());
            battle.ExecuteMove(battle.isFirst, battle.isSecond);
            battle.ExecuteMove(battle.isSecond, battle.isFirst);
            player1.draw();
            player2.draw()
            battle.checkDead();
        });

        $("#uppercut").click(function () {
            battle.AddMoves(uppercut, battle.AI());
            battle.ExecuteMove(battle.isFirst, battle.isSecond);
            battle.ExecuteMove(battle.isSecond, battle.isFirst);
            player1.draw();
            player2.draw()
            battle.checkDead();
        });
    });
});

var player1 = null;
var player2 = null;
var battle = null;

var Fighter = function (name, hp, attack) {
    this.name = name.toUpperCase();
    this.hp = (20 * hp) + 120;
    this.maxHp = this.hp;
    this.attack = (23 + (2 * attack));
}

Fighter.prototype.draw = function () {
    if (this.player === 1) {
        $("#player1HpCurrent").html(this.hp);
        $("#player1HpMax").html(this.maxHp);
        $("#player1Attack").html(this.attack);
    } else {
        $("#player2HpCurrent").html(this.hp);
        $("#player2HpMax").html(this.maxHp);
        $("#player2Attack").html(this.attack);
    }
};

var Battle = function (leftFighter, rightFighter) {
    this.leftFighter = leftFighter;
    this.rightFighter = rightFighter;
    this.moveDocket = [];
    this.isFirst = null;
    this.isSecond = null;

}

var Move = function (id, method, punchType) {
    this.id = id;
    this.method = method;
    this.punchType = punchType;
}

var Punch = function (name, multiplier, accuracy) {
    this.name = name;
    this.multiplier = multiplier;
    this.accuracy = accuracy;
}

var jabPunch = new Punch("JAB", 0.5, 100);
var hookPunch = new Punch("HOOK", 1, 50);
var uppercutPunch = new Punch("UPPERCUT", 1.5, 20);

var jab = new Move(1, "executePunch", jabPunch);
var hook = new Move(2, "executePunch", hookPunch);
var uppercut = new Move(3, "executePunch", uppercutPunch);


Battle.prototype.AddMoves = function (LeftMove, RightMove) {
    $("#gameLogList").html("");
    randomNumber = Math.floor((Math.random() * 100) + 1);

    if (randomNumber <= 50) {
        this.isFirst = this.leftFighter;
        this.isSecond = this.rightFighter;
        this.moveDocket.push(LeftMove);
        this.moveDocket.push(RightMove);
    } else {
        this.isFirst = this.rightFighter;
        this.isSecond = this.leftFighter;
        this.moveDocket.push(RightMove);
        this.moveDocket.push(LeftMove);
    }
};

Battle.prototype.ExecuteMove = function (User, Opponent) {
    var move = this.moveDocket[0];

    if (move.method === "executePunch") {
        $("#gameLogList").append("<li>" + User.name + " used " + move.punchType.name + ",</li>");
        var output = executePunch(User, Opponent, move.punchType);
        if (output === 0) {
            $("#gameLogList").append("<li>" + User.name + " missed!</li>");
        } else {
            $("#gameLogList").append("<li>" + Opponent.name + " took " + output + " damage!</li>");
        }
    }

    this.moveDocket.splice(0, 1);
};

var executePunch = function (User, Target, Punch) {
    randomNumber = Math.floor((Math.random() * 100) + 1);

    if (randomNumber <= Punch.accuracy) {
        var damage = 0;

        if (randomNumber <= 95) {
            $("#gameLogList").append('<li class="critical">CRITICAL!</li>');
            damage = Math.floor(2 * (User.attack * Punch.multiplier));
        } else {
            damage = Math.floor(User.attack * Punch.multiplier);
        }
        Target.hp -= damage;
        if (Target.hp < 0) {
            Target.hp = 0;
        }
        return damage;

    } else {
        return 0;
    }
}

Battle.prototype.AI = function () {
    player = this.leftFighter;
    AI = this.rightFighter;

    if (player.maxHp * 0.25 >= player.hp) {
        console.log(jab);
        return jab;
    } else if (AI.hp >= (player.hp * 2)) {
        console.log(hook);
        return hook;
    } else if (AI.hp >= (player.hp * 2)) {
        console.log(jab);
        return jab;
    } else if ((AI.hp < player.attack)) {
        console.log(uppercut);
        return uppercut;
    } else if (((AI.hp * 0.5) < AI.maxHp) && (player.hp > AI.hp)) {
        console.log(hook);
        return hook;
    } else if ((AI.hp <= (AI.maxHp * 0.25) && (player.hp >= (player.maxHp * 0.5)))) {
        console.log(uppercut);
        return uppercut;
    } else {
        console.log(hook);
        return jab;
    }
}

Battle.prototype.checkDead = function () {
    if (this.leftFighter.hp <= 0) {
        //player wins (progress)
    } else if (this.rightFighter.hp <= 0) {
        //enemy wins (return to index)
    }
};