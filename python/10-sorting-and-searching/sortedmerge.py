# You are given two sorted arrays, A and B, where A has a large enough buffer
# at the end to hold B. Write a method to merge B into A in sorted order

def merge(a, b):
  a_i = 0
  for i in range(len(a)):
    if a[i] != None:
      a_i = i
  b_i = len(b) - 1
  current = len(a) - 1
  while not (a_i < 0 and b_i < 0):
    if b_i < 0 or a[a_i] > b[b_i]:
      a[current] = a[a_i]
      a_i -= 1
    else:
      a[current] = b[b_i]
      b_i -= 1
    current -= 1

a = [1, 3, 5, None, None, None]
b = [2, 4, 6]
merge(a, b)
print(a)
