class_name UI
extends Control

@onready var health_bar: ProgressBar = $HealthBar
@onready var gold_text: Label = $GoldText

func set_gold(amount: int) -> void:
	gold_text.text = str("Gold: ", amount)
	
func set_health(current_hp: int, max_hp: int) -> void:
	health_bar.value = current_hp
	health_bar.max_value = max_hp
