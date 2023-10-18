extends Area3D

var spin_speed: float = 2.0
var bob_height: float = 0.2
var bob_speed: float = 5.0

@onready var start_y: float = global_position.y
var time_elapsed: float = 0.0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Rotate model
	rotate(Vector3.UP, spin_speed * delta)

	# Update time elapsed
	time_elapsed += delta
	# Smoothly bob model up and down
	# Create a smooth oscillation between 0 and 1
	var oscillation = (sin(time_elapsed * bob_speed) + 1) / 2
	# Move the model up and down
	global_position.y = start_y + oscillation * bob_height


func _on_body_entered(body):
	# Check to see if the body was the player
	if body.is_in_group("Player"):
		var player: Player = body
		player.add_score(1)
		# Destroy the coin
		queue_free()
