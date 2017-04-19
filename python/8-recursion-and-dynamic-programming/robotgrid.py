# Imagine a robot sitting on the upper left corner of grid with r rows and c
# columns. The robot can only move in two directions, right and down, but
# certain cells are "off limits" such that the robot cannot step on them.
# Design an algorithm to find a path for the robot from the top left to the
# bottom right.

# will need memoization, bottom-up solution
def find_path(grid, pos, target):
  if pos == target:
    return [pos]

  if grid[pos[0]][pos[1]] is 1:
    return None

  if pos[0] < len(grid) - 1:
    p = find_path(grid, (pos[0] + 1, pos[1]), target)
    if p:
      p.append(pos)
      return p

  if pos[1] < len(grid[0]) - 1:
    p = find_path(grid, (pos[0], pos[1] + 1), target)
    if p:
      p.append(pos)
      return p
  
def get_path(grid):
  p = find_path(grid, (0, 0), (len(grid) - 1, len(grid[0]) - 1))
  if p: p.reverse()
  return p

grid = [[0, 0, 0, 0, 1],
        [0, 0, 0, 1, 0],
        [0, 0, 0, 0, 0],
        [0, 1, 0, 0, 0],
        [1, 0, 0, 0, 0]]

print(get_path(grid))
