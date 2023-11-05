class_name Player
extends CharacterBody3D

#region Export Variables

#endregion Export Variables
#region Public Variables
var current_hp: int = 10
var max_hp: int = 10
var damage: int = 1
var gold: int = 0
var attack_rate: float = 0.3
var last_attack_time: int = 0
var move_speed: float = 5.0
var jump_force: float = 10.0
var gravity: float = 15.0

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var camera: Node3D = %CameraOrbit
@onready var attack_raycast: RayCast3D = %AttackRaycast

#endregion OnReady Variables
#region Built-in Godot Methods
func _physics_process(delta: float) -> void:
	# Reset movement
	velocity = Vector3(0, velocity.y, 0)
	_handle_gravity(delta)
	_handle_jump()
	_handle_movement()
	# Finish movement
	move_and_slide()




#endregion Built-in Godot Methods
#region Public Methods
func give_gold(amount: int) -> void:
	gold += amount
	print(str("Player has ", gold, " gold"))

func take_damage(amount: int) -> void:
	current_hp -= amount
	print(str("Player took ", amount, " damage"))
	if current_hp <= 0:
		print("You died!")
		get_tree().reload_current_scene()

#endregion Public Methods
#region Private Methods
func _handle_gravity(delta: float) -> void:
	velocity.y -= gravity * delta

func _handle_jump() -> void:
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_force

func _handle_movement() -> void:
	var _input = Vector3.ZERO
	if Input.is_action_pressed("move_forward"):
		_input.z += 1
	if Input.is_action_pressed("move_backward"):
		_input.z -= 1
	if Input.is_action_pressed("move_left"):
		_input.x += 1
	if Input.is_action_pressed("move_right"):
		_input.x -= 1
	_input = _input.normalized()
	var _direction = (transform.basis.z * _input.z + transform.basis.x * _input.x).normalized()
	velocity.x = _direction.x * move_speed
	velocity.z = _direction.z * move_speed

#endregion Private Methods
