# You are given an array-like data structure Listy which lacks a size method.
# It does, however, have an elementAt(i) method that returns the element at
# index i in O(1) time. If i is beyond the bounds of the data structure, it
# returns -1. (For this reason, the data structure only supports positive
# integers.) Given a Listy which contains sorted, positive integers, find the
# index at which an element x occurs. If x occurs multiple times, you may
# return any index.

class Listy():
  def __init__(self, array):
    self.len = len(array)
    self.array = array

  def element_at(self, i):
    if i >= self.len:
      return -1
    else:
      return self.array[i]

def sorted_no_size(listy, n):
  def binary_search(a, n, l, r):
    if l > r: return -1
    elif l == r: 
      if a.element_at(l) == n: return l
      else: return -1
    m = (l + r) // 2
    if a.element_at(m) == n:
      return m
    elif a.element_at(m) > n:
      return binary_search(a, n, l, m - 1)
    else:
      return binary_search(a, n, m + 1, r)

  def find_first_negone(a, l, r):
    if r - l < 2:
      if a.element_at(l) == -1: return l
      else: return r
    m = (l + r) // 2
    if a.element_at(m) == -1:
      return find_first_negone(a, l, m)
    else:
      return find_first_negone(a, m + 1, r)

  i = 1
  while listy.element_at(i) != -1:
    i <<= 1
  length = find_first_negone(listy, i >> 1, i)

  return binary_search(listy, n, 0, length - 1)

print(sorted_no_size(Listy([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18]), 13))
