# Given a sorted array of n integers that has been rotated an unknown number of times, write code to find an element in the array. You may assume that the array was originally sorted in increasing order.
# EXAMPLE
# Input: find 5 in [15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14]
# Output: 8 (the index of 5 in the array)

# O(n) solution is obvious, but that doesn't use the fact that it's sorted.
# O(log n) should still be possible here.
# Algorithm: like binary search, choose the middle. if it needs to be smaller,
# it has to be on the left side, unless we "fall off" - in that case, need to
# check the right side.
# But this is kind of bad, because after we've checked the left side, we have
# to repeat the same process again...
# we could also just do binary search to find the "seam", then perform binary
# search on the result, but this is also doing binary search twice...
# so basically, we need to check the usual side, and if we don't find it (or
# know we won't find it, because we wrapped around), we'll have to check the
# other side, too.

def find_in_rotated(n, a):
  def r(n, a, l, h):
    if l == h:
      if a[l] == n: return l
      return -1
    m = (l + h) // 2
    if n == a[m]:
      return m
    elif n < a[m]:
      if a[l] < n:
        return r(n, a, l, m - 1)
      elif a[l] == a[m]:
        return max(r(n, a, l, m-1), r(n, a, m+1, h))
      else:
        return r(n, a, m + 1, h)
    else:
      if a[h] > n:
        return r(n, a, m + 1, h)
      elif a[h] == a[m]:
        return max(r(n, a, l, m-1), r(n, a, m+1, h))
      else:
        return r(n, a, l, m - 1)

  return r(n, a, 0, len(a) - 1)

print("Should be 8:", find_in_rotated(5, [15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14]))
