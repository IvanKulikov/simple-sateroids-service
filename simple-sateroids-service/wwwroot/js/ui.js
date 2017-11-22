function drawText(ctx, str) {
    ctx.font = "30px Arial";
    var lines = str.split("\r\n");
    for (var i = 0; i < lines.length; i++) {
        ctx.fillText(
            lines[i],            
            (ctx.canvas.width) / 2 - (lines[i].length * 5),
            (ctx.canvas.height / 2) + (i * 30)
        )
    }
}
