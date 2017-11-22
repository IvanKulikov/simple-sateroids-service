function showGameOver(ctx, score, name) {
    ctx.font = "40px Arial";
    ctx.fillText("YOU DIED", 220, 150);
    ctx.font = "30px Arial"; 
    ctx.fillText("Your score is: " + score, 210, 200);    
    ctx.fillText("Enter your name: " + name, 120, 300);
    ctx.fillText("Press ESC to play again", 150, 450);
}

function showLeaderboard(ctx, data) {
    ctx.font = "30px Arial";    
    ctx.fillText("!!! H I G H S C O R E S !!!", 150, 50);

    for(var i = 0; i < data.length; i++) {
        ctx.fillText(data[i].name + " ..................... " + data[i].score + " pts", 140, 50 + 50 * (i + 1));
    }

    ctx.fillText("Press <SPACE> to play!", 150, 450);
}

function drawScore(ctx, score) {
    ctx.font = "12px Arial";  
    ctx.fillText('Score: ' + score,  ctx.canvas.width - 100, 30);
}
