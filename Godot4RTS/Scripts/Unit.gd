class_name Unit
extends CharacterBody2D

#region Export variables
@export var health: int = 100
@export var damage: int = 20
@export var move_speed: float = 50.0
@export var attack_range: float = 20.0
@export var attack_rate: float = 0.5 # time to wait between attacks
@export var is_player: bool = false
#endregion

#region Public Variables
var last_attack_time: float = 0.0
var target: Unit = null
var agent: NavigationAgent2D = null
var sprite: Sprite2D = null
#endregion Public Variables

#region Built-in Godot methods
func _ready() -> void:
	agent = %NavigationAgent2D as NavigationAgent2D
	sprite = %Sprite as Sprite2D

	var gm = get_node("/root/Main") as GameManager

	if is_player:
		gm.players.append(self)
	else:
		gm.enemies.append(self)

	# For testing
	# move_to_location(Vector2(-16, 24))

func _process(_delta: float) -> void:
	_target_check()

func _physics_process(_delta: float) -> void:
	if agent.is_navigation_finished():
		return

	# Get direction to next node and move to that location
	var direction = global_position.direction_to(agent.get_next_path_position())
	velocity = direction * move_speed
	move_and_slide()

#endregion Built-in Godot methods

#region Public Methods

func move_to_location(location: Vector2) -> void:
	target = null
	agent.target_position = location

func set_target(new_target: Unit) -> void:
	target = new_target

func take_damage(damage_to_take: int) -> void:
	print(str(self.name, " took ", damage_to_take, " damage"))
	health -= damage_to_take
	if health <= 0:
		print(str(self.name, " has died"))
		queue_free()

	# Modulate color to red for a brief moment to indicate damage
	sprite.modulate = Color.RED
	await get_tree().create_timer(0.1).timeout
	sprite.modulate = Color.WHITE

#endregion Public Methods

#region Private Methods

func _target_check() -> void:
	if target != null:
		var distance_to_target = global_position.distance_to(target.global_position)
		if distance_to_target <= attack_range:
			agent.target_position = global_position
			_try_attack_target()
		else:
			agent.target_position = target.global_position

func _try_attack_target() -> void:
	var current_time = Time.get_unix_time_from_system()
	if current_time - last_attack_time > attack_rate:
		last_attack_time = current_time
		target.take_damage(damage)

#endregion Private Methods
