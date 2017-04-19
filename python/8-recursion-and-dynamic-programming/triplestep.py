# A child is running up a staircase with n steps and can hop either 1 step, 
# 2 steps, or 3 steps at a time. Implement a method to count how many
# possible ways the child can run up the stairs.

def count_ways_memo(n):
  memo = [0]*(n+1)

  def help(n):
    if n == 0:
      return 0
    if n == 1:
      return 1
    if n == 2:
      return 2
    if n == 3:
      return 3
    if memo[n] == 0:
      memo[n] = help(n-1) + help(n-2) + help(n-3)
    return memo[n]
  
  return help(n)

def count_ways_bottom_up(n):
  p = [1, 2, 3]
  while n > 0:
    if n <= 3:
      return p[n-1]
    p[0] = p[0] + p[1] + p[2]
    p[1] = p[1] + p[2] + p[0]
    p[2] = p[2] + p[0] + p[1]
    n -= 3
  
print(count_ways_memo(5))

for i in range(30):
  print(count_ways_bottom_up(i))
