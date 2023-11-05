class_name GoldCoin
extends Area3D

@export var gold_to_give: int = 1

var rotation_speed: float = 5.0

func _ready() -> void:
	# Setup on body enter signal
	self.body_entered.connect(_on_body_entered)

func _process(delta: float) -> void:
	rotate_y(rotation_speed * delta)

func _on_body_entered(body: Node3D) -> void:
	if body is Player:
		var player: Player = body
		player.give_gold(gold_to_give)
		# TODO: Play coin sound
		queue_free()
