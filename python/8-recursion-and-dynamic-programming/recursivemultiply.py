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
    
