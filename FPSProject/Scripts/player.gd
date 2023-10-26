extends CharacterBody3D
class_name Player

# Stats
var health: int = 10
var max_health: int = 10
var ammo: int = 15
var score: int = 0

# Physics
var move_speed: float = 5.0
var jump_velocity: float = 5.0
var gravity: float = 12.0 # 9.8

# Camera
var min_look_angle: float = -90.0
var max_look_angle: float = 90.0
var look_sensitivity: float = 10.0

# Vectors
var mouse_delta: Vector2 = Vector2()

# Components
@onready var camera: Camera3D = %Camera
@onready var muzzle: Node3D = %Muzzle
@onready var bullet =  preload("res://Prefabs/bullet.tscn")
@onready var main_scene = get_node("/root/Main")
@onready var hud: HUD = get_node("/root/Main/UI/HUD")

func _ready() -> void:
	# Hide and lock the mouse cursor
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	# Camera looks at horizon on start
	camera.rotation_degrees.x = 0
	# Setup HUD
	update_hud()


func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		mouse_delta = event.relative
	if event.is_action_pressed("quit"):
		get_tree().quit()
	if event.is_action_pressed("shoot"):
		shoot()

func _process(delta: float) -> void:
	# Rotate camera along the x axis
	camera.rotation_degrees.x -= mouse_delta.y * look_sensitivity * delta

	# Clamp camera x rotation
	camera.rotation_degrees.x = clamp(
		camera.rotation_degrees.x,
		min_look_angle,
		max_look_angle
	)

	# Rotate player along the y axis
	rotation_degrees.y -= mouse_delta.x * look_sensitivity * delta

	# Reset mouse delta
	mouse_delta = Vector2()


func _physics_process(delta: float) -> void:
	_handle_gravity(delta)
	_handle_jump(delta)
	_handle_movement(delta)

	# Move the player
	move_and_slide()

func _handle_gravity(delta: float) -> void:
	if not is_on_floor():
		velocity.y -= gravity * delta

func _handle_jump(_delta: float) -> void:
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_velocity

func _handle_movement(_delta: float) -> void:
	# Reset velocity
	velocity.x = 0
	velocity.z = 0

	var input_dir := Input.get_vector(
		"strafe_left",
		"strafe_right",
		"move_forward",
		"move_backward"
	)
	input_dir = input_dir.normalized()

	var forward = global_transform.basis.z
	var right = global_transform.basis.x

	var relative_dir = (forward * input_dir.y) + (right * input_dir.x)

	velocity.x = relative_dir.x * move_speed
	velocity.z = relative_dir.z * move_speed

func shoot():
	if ammo > 0:
		var bullet_instance = bullet.instantiate()
		bullet_instance.global_transform = muzzle.global_transform
		main_scene.add_child(bullet_instance)
		ammo -= 1
	print("Ammo: ", ammo)
	update_hud()

func take_damage(_damage: int) -> void:
	health -= _damage
	print("Health: ", health)
	if health <= 0:
		health = 0
		print("You died!")
		die()
	update_hud()

func die() -> void:
	# Reboot game
	get_tree().reload_current_scene()

func update_hud() -> void:
	hud.set_health_bar(health, max_health)
	hud.set_ammo_text(ammo)
	hud.set_score_text(score)

func add_score(_score: int) -> void:
	score += _score
	print("Score: ", score)
	update_hud()

func heal(_amount: int) -> void:
	health += _amount
	if health > max_health:
		health = max_health
	print("Healed: ", health)
	update_hud()

func give_ammo(_amount: int) -> void:
	ammo += _amount
	print("Ammo: ", ammo)
	update_hud()
