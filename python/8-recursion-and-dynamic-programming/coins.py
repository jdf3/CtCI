# Given an infinite number of quarters (25 cents), dimes (10 cents), nickels (5
# cents), and pennies (1 cent), write code to calculate the number of ways of
# representing n cents

def coins(n):
  ways = [1] + [0]*n
  coins = [1, 5, 10, 25]
  for coin in coins:
    for i in range(coin, n + 1):
      ways[i] += ways[i - coin]
  return ways[n]

print(coins(500))
