extends Node2D

@export var spawn_count: int = 200
var star_scene = preload("res://Prefabs/prefab_star.tscn")
var min_x_bound = -280
var min_y_bound = -155
var max_x_bound = 280
var max_y_bound = 155
var min_scale = 0.5
var max_scale = 1.0

# Called when the node enters the scene tree for the first time.
func _ready():
	for i in spawn_count:
		var star: Sprite2D = star_scene.instantiate()
		# add_child here adds this to the Main (root) node since
		# this script is attached to the Main node
		add_child(star)
		
		star.position.x = randi_range(min_x_bound, max_x_bound)
		star.position.y = randi_range(min_y_bound, max_y_bound)
		
		var star_size: float = randf_range(min_scale, max_scale)
		star.scale.x = star_size
		star.scale.y = star_size
