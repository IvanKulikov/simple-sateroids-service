var bullet = function(x, y, direction) {
    var x = x;
    var y = y;
    var direction = direction;	
    var speed = 10;
    var life = 60;
    
    this.draw = function(ctx) {
		ctx.strokeStyle = '#FFFFFF';
		ctx.beginPath();
		ctx.moveTo(x - 1, y - 1);		
		ctx.lineTo(x + 1, y + 1);
		ctx.stroke();
	};

    this.move = function() {        
		x += speed * Math.cos(direction * Math.PI / 180);
		y += speed * Math.sin(direction * Math.PI / 180);
		if (x > ctx.canvas.width)
			x = 0;
		if (y > ctx.canvas.height)
            y = 0;
        if (x < 0)
			x = ctx.canvas.width;
		if (y < 0)
			y = ctx.canvas.height;
        life--;
    };
    
    this.isOld = function() {
        return life < 0;
    }

    this.getCoords = function() {
		return {x: x, y :y}
	}
}