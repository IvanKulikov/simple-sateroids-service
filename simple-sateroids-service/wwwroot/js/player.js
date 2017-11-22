var player = function(x, y, direction) {
	var x = x;
	var y = y;
	var size = 20;
	var direction = direction;
	var angles = [];
	var poly = [];
	var speedx = 0;
	var speedy = 0;
	var PLAYER_MAX_SPEED = 5;
	var PLAYER_ACCELERATION = 1;
	var SPACE_FRICTION = 0.05;
	// public 
	this.dead = false;
	
	var recalculateGeometry = function() {
		poly[0] = x + size * Math.cos((0 + direction) * Math.PI / 180);		poly[1] = y + size * Math.sin((0 + direction)* Math.PI / 180);
		poly[2] = x + size * Math.cos((140 + direction) * Math.PI / 180);	poly[3] = y + size * Math.sin((140 + direction) * Math.PI / 180);
		poly[4] = x + size * Math.cos((220 + direction) * Math.PI / 180);	poly[5] = y + size * Math.sin((220 + direction)* Math.PI / 180);
	};
	
	this.draw = function(ctx) {
		ctx.strokeStyle = '#FFFFFF';
		ctx.beginPath();
		ctx.moveTo(poly[0], poly[1]);
		for (var i = 0; i < poly.length; i+=2) {
			ctx.lineTo(poly[i], poly[i + 1]);
		}
		ctx.lineTo(poly[0], poly[1]);
		ctx.stroke();
		
		// ctx.fillStyle = '#FFFFFF';
		// ctx.fillText('Player x: ' + x.toString().substr(0, 10) + ' y: ' + y.toString().substr(0, 10), ctx.canvas.width - 200, 30);
	};
	
	this.move = function() {
		x += speedx;
		y += speedy;
			
		if (speedx < 0)
			speedx += SPACE_FRICTION;
		else 
			speedx -= SPACE_FRICTION;
		if (speedy < 0)
			speedy += SPACE_FRICTION;
		else 
			speedy -= SPACE_FRICTION;
		
		if (x > ctx.canvas.width + size)
			x = 0;
		if (y > ctx.canvas.height + size)
			y = 0;
		if (x < 0)
			x = ctx.canvas.width;
		if (y < 0)
			y = ctx.canvas.height;

		
		recalculateGeometry();
	};
	
	this.accelerate = function() {
		speedx += PLAYER_ACCELERATION * Math.cos(direction *  Math.PI / 180);
		speedy += PLAYER_ACCELERATION * Math.sin(direction *  Math.PI / 180);

		if (speedx > PLAYER_MAX_SPEED)
			speedx = PLAYER_MAX_SPEED;
		if (speedx > PLAYER_MAX_SPEED)
			speedy = PLAYER_MAX_SPEED;
	}
	
	this.rotate = function(dir) 
	{
		switch(dir)
		{
		case 0:
				direction += 10; // Right
			break;
		case 1:
				direction -= 10; // Left
			break;
		}
	
		if (direction > 360)
			direction = 0;
		if (direction < 0)
			direction = 360;
		
		recalculateGeometry();
	}
	
	this.contains = function (x, y) {
		if (x > x * width && y > y * height && y < y * height + height && x < x * width + width)
			return true;
		else
			return false;
	};	

	this.getBullet = function() {
		return new bullet(x, y, direction)
	}

	this.getCoords = function() {
		return {x: x, y :y}
	}	
};