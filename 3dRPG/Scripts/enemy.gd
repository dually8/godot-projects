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
var gravity: float = 15.0
var grunt_sound: AudioStream = preload("res://Sounds/enemy_grunt.ogg")

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var timer: Timer = $Timer
@onready var player: Player = get_node("/root/Main/Player")
@onready var model: MeshInstance3D = $Model
@onready var audio_player: AudioStreamPlayer3D = $AudioStreamPlayer3D

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	audio_player.stream = grunt_sound
	timer.timeout.connect(_on_timer_timeout)
	timer.wait_time = attack_rate
	timer.start()

func _physics_process(_delta: float) -> void:
	_chase_player()
	_face_player()

#endregion Built-in Godot Methods
#region Public Methods
func take_damage(amount: int) -> void:
	current_hp -= amount
	print("Enemy hp: ", current_hp)
	if current_hp <= 0:
		# die
		queue_free()

#endregion Public Methods
#region Private Methods
func _on_timer_timeout() -> void:
	audio_player.play()
	var distance_to_player = player.get_position().distance_to(get_position())
	if distance_to_player < attack_distance:
		player.take_damage(damage)

func _chase_player() -> void:
	# Get the distance to the player
	var distance_to_player = player.get_position().distance_to(get_position())
	# Chase after the player if we're outside of attack range
	if distance_to_player > attack_distance:
		# Get the direction to the player
		var direction_to_player = (player.get_position() - get_position()).normalized()
		# Set the velocity
		velocity = direction_to_player * move_speed
		# Fall if not on floor
		if is_on_floor():
			velocity.y = 0
		else:
			velocity.y = -1 * gravity
		# Move towards the player
		move_and_slide()

func _face_player() -> void:
	var dir = (player.get_position() - get_position()).normalized()
	rotation.y = atan2(dir.x, dir.z)

#endregion Private Methods
