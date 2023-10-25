extends Unit
class_name EnemyUnit


@export var detect_range: float = 100.0
@onready var gm: GameManager = get_node("/root/Main")

func _process(delta: float) -> void:
	if target == null:
		for player in gm.players:
			if player == null:
				continue
			var dist = global_position.distance_to(player.global_position)
			if dist < detect_range:
				set_target(player)
				break
	super._process(delta)
