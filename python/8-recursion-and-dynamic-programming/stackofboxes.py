# You have a stack of n boxes, with widths w_i, heights h_i, and depths d_i.
# The boxes cannot be rotated and can only be stack on top of one another if
# each box in the stack is strictly larger than the box above it in width,
# height, and depth. Implement a method to compute the height of the tallest
# possible stack. The height of a stack is the sum of the heights of each box.

def stackofboxes(stack):
  memo = [-1]*len(stack)
  def r(stack, stackpos, basewidth, baseheight, basedepth):
    if stackpos >= len(stack): return 0
    poss = [0]
    for i in range(stackpos, len(stack)):
      w, h, d = stack[i]
      if w < basewidth and h < baseheight and d < basedepth:
        if memo[i] != -1:
          result = memo[i]
        else:
          memo[i] = r(stack, i+1, w, h, d)
          result = memo[i]
        poss.append(h + result)

    return max(poss)

  copy = stack[:]
  copy.sort(reverse=True)
  return r(copy, 0, 1 << 32, 1 << 32, 1 << 32)

print(stackofboxes([(1, 1, 1), (2, 2, 2), (2.5, 1, 1.75)]))
