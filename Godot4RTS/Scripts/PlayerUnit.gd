extends Unit
class_name PlayerUnit

@onready var selection_visual: Sprite2D = %SelectionVisual

func toggle_selection_visual(_visible: bool) -> void:
		selection_visual.visible = _visible
