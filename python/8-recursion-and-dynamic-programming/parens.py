# Implement an algorithm to print all valid (e.g., properly opened and closed)
# combinations of n pairs of parentheses.

def parens(n):
  def h(current_open, opens, closes):
    if opens == 0 and closes == 0:
      yield ""
      return
    if opens > 0:
      yield from map(lambda s: "(" + s, h(current_open + 1, opens - 1, closes))
    if current_open > 0 and closes > 0:
      yield from map(lambda s: ")" + s, h(current_open - 1, opens, closes - 1))
  
  return h(0, n, n)

print(list(parens(2)))
print(list(parens(3)))

