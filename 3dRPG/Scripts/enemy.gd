class_name Enemy
extends CharacterBody3D

#region Export Variables

#endregion Export Variables
#region Public Variables
var current_hp: int = 3
var max_hp: int = 3
var damage:int = 1
var attack_distance: float = 1.5
var attack_rate: float = 1.0
var move_speed: float = 2.5

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var timer: Timer = $Timer
@onready var player: Player = get_node("/root/Main/Player")

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	timer.timeout.connect(_on_timer_timeout)
	timer.wait_time = attack_rate
	timer.start()

func _physics_process(delta: float) -> void:
	# Get the distance to the player
	var distance_to_player = player.get_position().distance_to(get_position())
	# Chase after the player if we're outside of attack range
	if distance_to_player > attack_distance:
		# Get the direction to the player
		var direction_to_player = (player.get_position() - get_position()).normalized()
		# Set the velocity
		velocity = direction_to_player * move_speed
		velocity.y = 0
		# Move towards the player
		move_and_slide()

#endregion Built-in Godot Methods
#region Public Methods
func take_damage(amount: int) -> void:
	current_hp -= amount
	if current_hp <= 0:
		# die
		queue_free()

#endregion Public Methods
#region Private Methods
func _on_timer_timeout() -> void:
	var distance_to_player = player.get_position().distance_to(get_position())
	if distance_to_player < attack_distance:
		player.take_damage(damage)
#endregion Private Methods
