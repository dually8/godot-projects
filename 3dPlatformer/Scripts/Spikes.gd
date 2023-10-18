extends Area3D

func _on_body_entered(body) -> void:
	if body.is_in_group("Player"):
		var player: Player = body
		player.game_over()
