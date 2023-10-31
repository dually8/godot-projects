class_name Chest
extends Interactable

@export var gold_to_give: int = 5

func interact(player: Player) -> void:
	print("chesting lol")
	player.give_gold(gold_to_give)
	queue_free()
