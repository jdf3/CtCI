# Write a method to return all subsets of a set

# returns an iterator, assumes s is indexable
def powersets(s):
  if len(s) == 0:
    yield []
    return
  for ps in powersets(s[1:]):
    yield ps
    ps2 = ps[:]
    ps2.append(s[0])
    yield ps2

print("HI")
print(list(powersets([1, 2, 3])))

# does not assume such things! but also, does not return an iterator
def powersets_iter(collection):
  lists = [[]]
  for item in collection:
    clone = []
    for l in lists:
      clone.append(l[:])
    for l in clone:
      l.append(item)
    lists.extend(clone)
  return lists

print(list(powersets_iter([1, 2, 3])))
