extends CharacterBody2D

var move_speed: float = 200.0

func _physics_process(delta):
	velocity.x = 0
	velocity.y = 0
	
	if Input.is_key_pressed(KEY_LEFT):
		print("Pressing left")
		velocity.x -= 1
	if Input.is_key_pressed(KEY_RIGHT):
		print("Pressing right")
		velocity.x += 1
	if Input.is_key_pressed(KEY_UP):
		print("Pressing up")
		velocity.y -= 1
	if Input.is_key_pressed(KEY_DOWN):
		print("Pressing down")
		velocity.y += 1
	
	velocity *= move_speed
	
	move_and_slide()
