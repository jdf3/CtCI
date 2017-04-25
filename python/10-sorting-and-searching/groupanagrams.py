# Write a method to sort an array of strings so that all the anagrams are next
# to each other

# this works, but isn't time or space efficient
def sortanagrams(a):
  a = list(map(lambda s: sorted(s), a))
  a.sort()
  return a

print(sortanagrams(["abc", "e", "k", "de", "cba", "cbd"]))

def anasort(a):
  # (low, high)
  # can't use a built in sort here, because we want to be able to build
  # buckets...
  buckets = [] 

  a.sort(key=lambda s: len(s))
  i = 0
  while i < len(a):
    j = i
    length = len(a[i])
    while j+1 < len(a) and len(a[j+1]) == length:
      j += 1
    buckets.append((i, j))
    i = j + 1

  for low, high in buckets:
    if high - low < 2: continue
    a[low:high+1].sort(key=lambda s: sorted(s))

  return a

print(anasort(["abc", "e", "k", "de", "cba", "cbd"]))

