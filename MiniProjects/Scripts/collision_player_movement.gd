extends RigidBody3D


@export var move_speed: float = 2.0

func _physics_process(delta):
	# If press left, move left
	if Input.is_key_pressed(KEY_LEFT) or Input.is_key_pressed(KEY_A):
		linear_velocity.x = -move_speed;
	# If press right, move right
	if Input.is_key_pressed(KEY_RIGHT) or Input.is_key_pressed(KEY_D):
		linear_velocity.x = move_speed;


func _on_body_entered(body):
	if body.is_in_group("Tree"):
		get_tree().reload_current_scene()
