extends Area2D

var bob_height: float = 5.0
var bob_speed: float = 5.0

@onready var start_y: float = global_position.y

var _timeElapsed: float = 0.0

func _process(delta: float):
	_timeElapsed += delta

	# Create sine wave
	var y_sin = (sin(_timeElapsed * bob_speed) + 1) / 2
	var y_adjustment = y_sin * bob_height
	global_position.y = start_y + y_adjustment


func _on_body_entered(body: Player):
	if body.is_in_group("Player"):
		body.add_score(1)
		queue_free()
