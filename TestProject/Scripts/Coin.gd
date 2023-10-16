extends Area2D

func _on_body_entered(body):
	# Make player bigger
	body.scale.x += 0.2
	body.scale.y += 0.2
	
	# Destroy coin
	queue_free()
