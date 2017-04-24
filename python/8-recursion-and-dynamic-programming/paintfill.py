# Implement the "paint fill" function that one might see on many image editing
# programs. That is, given a screen (represented by a two-dimensional array of
# colors), a point, and a new color, fill in the surrounding area until the
# color changes from the original color.

def paintfill(screen, x, y, newcolor):
  def paintfill_r(screen, x, y, new_c, orig_c):
    if x < 0 or x > len(screen) - 1: return
    if y < 0 or y > len(screen[0]) - 1: return
    if screen[x][y] == orig_c:
      screen[x][y] = new_c
      paintfill_r(screen, x + 1, y, new_c, orig_c)
      paintfill_r(screen, x - 1, y, new_c, orig_c)
      paintfill_r(screen, x, y + 1, new_c, orig_c)
      paintfill_r(screen, x, y - 1, new_c, orig_c)

  paintfill_r(screen, x, y, newcolor, screen[x][y])

a = [[0, 0, 3, 0, 0],
     [0, 3, 0, 0, 0],
     [3, 0, 0, 0, 0],
     [0, 3, 0, 0, 0],
     [0, 0, 3, 0, 0]]
      
paintfill(a, 2, 2, 1)
print(a)
