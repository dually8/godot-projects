extends Area3D

@export_file("*.tscn") var next_scene

func _on_body_entered(body):
	if body.is_in_group("Player"):
		print("Winner!")
		await _show_win_screen()
		get_tree().change_scene_to_file(next_scene)

func _show_win_screen():
	print("show win screen")
	var win_screen = get_tree().get_first_node_in_group("WinScreen") as Control
	get_tree().paused = true
	win_screen.visible = true
	await get_tree().create_timer(2.0).timeout
	win_screen.visible = false
	get_tree().paused = false
