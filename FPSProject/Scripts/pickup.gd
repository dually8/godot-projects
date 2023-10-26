extends Area3D
class_name Pickup

enum PickupType {
	HEALTH,
	AMMO
}

@export var type: PickupType = PickupType.HEALTH
@export var amount: int = 10

# Bobbing
@onready var start_y_pos: float = position.y
var bob_height: float = 0.5
var bob_speed: float = 1.0
var spin_speed: float = 2.0
var is_bobbing_up: bool = true


func _process(delta: float) -> void:
	# Bob the pickup up and down
	var bob_delta: float = delta * bob_speed
	if is_bobbing_up:
		position.y += bob_delta
		if position.y > start_y_pos + bob_height:
			is_bobbing_up = false
	else:
		position.y -= bob_delta
		if position.y < start_y_pos - bob_height:
			is_bobbing_up = true
	# Spin the model
	rotate_y(bob_delta * spin_speed)

func _on_body_entered(body: Node3D) -> void:
	if body is Player:
		pickup(body)
		queue_free()

func pickup(player: Player) -> void:
	match type:
		PickupType.HEALTH:
			player.heal(amount)
		PickupType.AMMO:
			player.give_ammo(amount)
