class_name Enemy
extends CharacterBody2D



#region Export Variables
@export var exp_to_give: int = 30

#endregion Export Variables
#region Public Variables
var current_hp: int = 5
var max_hp: int = 5
var move_speed: int = 150
var attack_damage: int = 1
var attack_rate: float = 1.0
var attack_distance: int = 80
var chase_distance: int = 400

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var timer: Timer = %Timer
@onready var target: Player = get_node("/root/Main/Player")

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	timer.wait_time = attack_rate
	timer.start()

func _physics_process(_delta: float) -> void:
	var distance_to_player = position.distance_to(target.position)

	if distance_to_player > attack_distance and distance_to_player < chase_distance:
		var direction = (target.position - position).normalized()
		velocity = direction * move_speed
		move_and_slide()
#endregion Built-in Godot Methods
#region Public Methods
func take_damage(amount: int) -> void:
	current_hp -= amount
	if current_hp <= 0:
		die()

func die() -> void:
	target.gain_exp(exp_to_give)
	queue_free()

#endregion Public Methods
#region Private Methods
func _on_timer_timeout() -> void:
	# Check to see if the player is in range
	var distance_to_player = position.distance_to(target.position)
	# if in range, attack
	if distance_to_player <= attack_distance:
		target.take_damage(attack_damage)

#endregion Private Methods
