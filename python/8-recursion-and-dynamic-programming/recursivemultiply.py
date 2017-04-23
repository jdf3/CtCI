# Write a recursive function to multiply two positive integers without using
# the * operator. You can use addition, subtraction, and bitshifting, but you
# should minimize the number of those operations.

# assumes a and b are positive
def mult(a, b):
  small, big = sorted([a, b])
  product = 0
  pos = 0
  while small > 0:
    if small & 1:
      product += (big << pos)
    small >>= 1
    pos += 1
  return product

print("100 x 16 =", mult(100, 16), "\n(Should be", 100*16, ")")
print("653 x 242 =", mult(653, 242), "\n(Should be", 653*242, ")")
    
def multr(a, b):
  def h(l, g):
    if l == 0: return 0
    result = 0
    if l & 1:
      result += g
    return result + (h(l >> 1, g) << 1)

  small, big = sorted([a, b])
  return h(small, big)

print("100 x 16 =", multr(100, 16), "\n(Should be", 100*16, ")")
print("653 x 242 =", multr(653, 242), "\n(Should be", 653*242, ")")

# for improvement, think about how this performs for (e.g.) powers of 2, or
# powers of 2 minus 1. Can I use subtraction somehow?
