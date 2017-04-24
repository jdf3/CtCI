# Write a method to compute all permutations of a string whose characters are
# not necessarily unique. The list of permutations should not have duplicates.

from collections import defaultdict

def permutations_with_dups(s):
  def h(d):
    if len(d) == 0:
      yield []
      return
    keys = list(d.keys())
    for key in keys:
      d[key] -= 1
      if d[key] == 0: del d[key]
      for perm in h(d):
        p = perm[:]
        p.append(key)
        yield p
      d[key] += 1

  d = defaultdict(int)
  for c in s:
    d[c] += 1
  return h(d)

print(list(permutations_with_dups("abc")))
