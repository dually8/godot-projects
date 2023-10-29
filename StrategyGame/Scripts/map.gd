extends Node
class_name Map


#region Export Variables

#endregion Export Variables
#region Public Variables
var all_tiles: Array[Tile] = []
var tiles_with_buildings: Array[Tile] = []
var tile_size: float = 64.0 # 64x64 pixels

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	var tiles = get_tree().get_nodes_in_group(Constants.TILE_GROUP)
	for tile in tiles:
		if tile is Tile:
			all_tiles.append(tile)

	# Find the start tile
	var start_tile: Tile = null
	for tile in all_tiles:
		if tile.is_start_tile:
			start_tile = tile
			break
	assert(start_tile != null, "No start tile found!")
	place_building_at_tile(
		BuildingData.base.icon_texture,
		start_tile
	)

#endregion Built-in Godot Methods
#region Public Methods

# Loop through all tiles and return the one at the given position, granted it doesn't have a building on it
func get_tile_at_position(position: Vector2) -> Tile:
	for tile in all_tiles:
		if tile.position == position and tile.has_building == false:
			return tile
	return null

# Loop through all tiles and disable their highlights
func disable_tile_highlights():
	for tile in all_tiles:
		tile.set_highlight(false)

# Loop through all tiles and highlight the ones that are available for building
func highlight_available_tiles():
	for tile in tiles_with_buildings:
		var north_tile = get_tile_at_position(tile.position + Vector2(0, tile_size))
		if north_tile != null:
			north_tile.set_highlight(true)

		var south_tile = get_tile_at_position(tile.position + Vector2(0, -tile_size))
		if south_tile != null:
			south_tile.set_highlight(true)

		var east_tile = get_tile_at_position(tile.position + Vector2(tile_size, 0))
		if east_tile != null:
			east_tile.set_highlight(true)

		var west_tile = get_tile_at_position(tile.position + Vector2(-tile_size, 0))
		if west_tile != null:
			west_tile.set_highlight(true)

func place_building_at_tile(building_texture: Texture, tile: Tile):
	tiles_with_buildings.append(tile)
	tile.place_building(building_texture)
	disable_tile_highlights()

#endregion Public Methods
#region Private Methods

#endregion Private Methods
