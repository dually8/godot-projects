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
var whack_sound: AudioStream = preload("res://Sounds/whack.ogg")

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var camera: Node3D = %CameraOrbit
@onready var attack_raycast: RayCast3D = %AttackRaycast
@onready var sword_anim: AnimationPlayer = %SwordAnimator
@onready var ui: UI = get_node("/root/Main/CanvasLayer/UI")
@onready var audio_player: AudioStreamPlayer3D = $AudioStreamPlayer3D

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	audio_player.stream = whack_sound
	ui.set_gold(gold)
	ui.set_health(current_hp, max_hp)

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("attack"):
		_attack()

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
	ui.set_gold(gold)

func take_damage(amount: int) -> void:
	current_hp -= amount
	print(str("Player took ", amount, " damage"))
	ui.set_health(current_hp, max_hp)
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

func _attack() -> void:
	if Time.get_ticks_msec() - last_attack_time < attack_rate * 1000:
		return
	last_attack_time = Time.get_ticks_msec()
	sword_anim.stop()
	sword_anim.play("attack")
	if attack_raycast.is_colliding():
		var collider = attack_raycast.get_collider()
		if collider is Enemy:
			audio_player.play()
			var enemy: Enemy = collider
			enemy.take_damage(damage)

#endregion Private Methods
