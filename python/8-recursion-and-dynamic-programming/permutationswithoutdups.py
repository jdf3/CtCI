# Write a method to compute all permutations of a string of unique characters

def permutations_without_dups(s):
  def h(s):
    if len(s) == 0:
      yield []
      return
    for perm in h(s[1:]):
      for i in range(len(perm) + 1):
        p = perm[:]
        p.insert(i, s[0])
        yield p

  s = list(set(s))
  return h(s)

print(list(permutations_without_dups("abcddddd")))
