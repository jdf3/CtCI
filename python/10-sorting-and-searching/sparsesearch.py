# Given a sorted array of strings that is interspersed with empty strings,
# write a method to find the location of a given string.

def sparse_search(needle, haystack):
  def rec(needle, haystack, l, r):
    if l > r:
      return -1
    elif l == r:
      if haystack[l] == needle: return l
      else: return -1
    m = (l + r) // 2
    if haystack[m] == needle:
      return m
    m_l = m
    m_r = m
    while m_l >= 0 and m_r < len(haystack) and len(haystack[m_l]) == 0 and len(haystack[m_r]) == 0:
      m_l -= 1
      m_r += 1
    if m_l < 0:
      return rec(needle, haystack, m_r, r)
    elif m_r >= len(haystack):
      return rec(needle, haystack, l, m_l)
    elif len(haystack[m_l]) != 0:
      if haystack[m_l] == needle:
        return m_l
      elif haystack[m_l] > needle:
        return rec(needle, haystack, l, m_l - 1)
      else:
        return rec(needle, haystack, m_r, r)
    else:
      if haystack[m_r] == needle:
        return m_r
      elif haystack[m_r] > needle:
        return rec(needle, haystack, l, m_l - 1)
      else:
        return rec(needle, haystack, m_r + 1, r)

  return rec(needle, haystack, 0, len(haystack))

print(sparse_search("ball", ["at", "", "", "", "ball", "", "", "car", "", "", "dad", "", ""]))

