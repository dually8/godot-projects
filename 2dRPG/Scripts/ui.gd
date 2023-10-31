class_name UI
extends Control

@onready var level_text: Label = %LevelText
@onready var health_bar: ProgressBar = %HealthBar
@onready var exp_bar: ProgressBar = %ExpBar
@onready var gold_text: Label = %GoldText


func set_level_text(level: int) -> void:
	level_text.text = str(level)

func set_health_bar(health: int, max_health: int) -> void:
	health_bar.value = health
	health_bar.max_value = max_health

func set_exp_bar(current_exp: int, exp_to_next_level: int) -> void:
	exp_bar.value = (100.0 / float(exp_to_next_level)) * float(current_exp)

func set_gold_text(gold: int) -> void:
	gold_text.text = str("Gold: " , gold)
