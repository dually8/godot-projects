extends Area3D

var speed: float = 30.0
var damage: int = 1

func _process(delta: float) -> void:
	position += global_transform.basis.z * -speed * delta

func destroy() -> void:
	queue_free()


func _on_body_entered(body: Node3D) -> void:
	if body is Enemy:
		var enemy: Enemy = body
		enemy.take_damage(damage)
		destroy()
