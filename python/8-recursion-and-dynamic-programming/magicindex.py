# A magic index in an array A[0...n-1] is defined to be an indexe such that
# A[i] = i. Given a sorted array of distinct integers, write a method to find a
# magic index, if one exists, in array A.

# Assumes distinct
def find(sorted_array):
  i = 0
  while i < len(sorted_array):
    difference = sorted_array[i] - i
    if difference > 0:
      return None
    elif difference == 0: 
      return i
    else:
      i += -difference
  return None

def findr(sorted_array):
  def h(a, l, r):
    if l > r or a[l] > l or a[r] < r:
      return None
    m = (l + r) // 2
    difference = a[m] - m
    if difference == 0:
      return m
    elif difference > 0:
      return h(sorted_array, l, m - 1)
    else:
      return h(sorted_array, m - difference, r)

  return h(sorted_array, 0, len(sorted_array) - 1)

print(find([-1, 0, 1, 2, 4, 5]))
print(findr([-1, 0, 1, 2, 4, 5]))

print(findr([3, 3, 3, 3, 3])) # should give "None"

# for duplicates, i don't really see a good way to salvage the recursive
# solution... here's an iterative one, which is O(n) average and worst case,
# but might work nicely for some inputs
def find_with_dups(sorted_array):
  i = 0
  while i < len(sorted_array):
    difference = sorted_array[i] - i
    if difference > 0:
      i += difference
    elif difference == 0: 
      return i
    else:
      i += 1
  return None

print(find_with_dups([3, 3, 3, 3, 3]))
