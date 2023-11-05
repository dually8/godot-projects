class_name GoldCoin
extends Area3D

@export var gold_to_give: int = 1

var rotation_speed: float = 5.0
var coin_sound: AudioStream = preload("res://Sounds/coin_pickup.ogg")

@onready var audio_player: AudioStreamPlayer3D = $AudioStreamPlayer3D

func _ready() -> void:
	# Setup on body enter signal
	self.body_entered.connect(_on_body_entered)
	# Setup audio player
	audio_player.stream = coin_sound

func _process(delta: float) -> void:
	rotate_y(rotation_speed * delta)

func _on_body_entered(body: Node3D) -> void:
	if body is Player:
		var player: Player = body
		player.give_gold(gold_to_give)
		_hide()
		audio_player.stop()
		audio_player.play()
		audio_player.finished.connect(_destroy)


func _hide() -> void:
	self.visible = false
	$CollisionShape3D.disabled = true

func _destroy() -> void:
	queue_free()
