extends Area3D


@export var move_speed: float = 2.0
@export var move_dir: Vector3

var start_position: Vector3
var target_position: Vector3

# Called when the node enters the scene tree for the first time.
func _ready():
	start_position = global_position
	target_position = start_position + move_dir


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Move towards the target_position
	global_position = global_position.move_toward(target_position, move_speed * delta)

	_check_and_swap_positions()

func _check_and_swap_positions():
	if global_position == target_position:
		if global_position == start_position:
			target_position = start_position + move_dir
		else:
			target_position = start_position


func _on_body_entered(body: Node3D) -> void:
	# If body is in group "Player" execute game_over
	if body.is_in_group("Player"):
		# Cast body to Player, then execute game_over
		var player: Player = body
		player.game_over()
