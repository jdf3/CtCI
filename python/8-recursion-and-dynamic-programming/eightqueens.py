# Write an algorithm to print all ways of arranging eight queens on an 8x8
# chess board so that none of them share the same row, column, or diagonal. In
# this case, "diagonal" means all diagonals, not just the two that bisect the
# board.

def queens():
  # Assuming that (x, y) positions given by columns[x] = y are valid,
  # determine whether placing a piece at (row, col) is valid
  # We don't need to be concerned about sentinel values, since this method only
  # looks to rows below "row".
  def isvalid(columns, row, col):
    for r in range(row):
      if col == columns[r]: return False
      if (row - r) == abs(col - columns[r]): return False
    return True

  def place(row, columns):
    if row == 8:
      return 1
    else:
      ways = 0
      for col in range(8):
        if isvalid(columns, row, col):
          columns[row] = col
          ways += place(row + 1, columns)
      return ways

  return place(0, [0]*8)

def printboard(columns):
  for r in columns:
    col = columns[r]
    print(" "*col + "x" + " "*(8 - 1 - col))
  print()

print(queens())
