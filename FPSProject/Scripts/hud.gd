extends Control
class_name HUD

@onready var health_bar: TextureProgressBar = %HealthBar
@onready var ammo_text: Label = %AmmoText
@onready var score_text: Label = %ScoreText

func set_health_bar(current_health: int, max_health: int) -> void:
	health_bar.max_value = max_health
	health_bar.value = current_health

func set_ammo_text(ammo_amount: int) -> void:
	ammo_text.text = str("Ammo: ", ammo_amount)

func set_score_text(score: int) -> void:
	score_text.text = str("Score: ", score)
