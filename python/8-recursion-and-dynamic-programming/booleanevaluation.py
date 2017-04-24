# Given a boolean expression consisting of the symbols 0 (false), 1 (true), &
# (AND), | (OR), and ^ (XOR), and a desired boolean result value result,
# implement a function to count the number of ways of parenthesizing the
# expression such that it evaluates to result. The expression should be fully
# parenthesized (e.g., (0)^(1)), but not extraneously (((0))^(1)).
#
# EXAMPLE
# countEval("1^0|0|1", false) -> 2
# countEval("0&0&0&1^1|0", true) -> 10

from collections import defaultdict

def count_eval(exp, result):
  memo = defaultdict(lambda: -1)
  def r(e, res):
    if len(e) == 1: return int(e) == res
    if memo[(e, res)] != -1: return memo[(e, res)]
    ways = 0
    for op_index in range(1, len(e), 2):
      op = e[op_index]
      before = e[:op_index]
      after = e[op_index+1:]
      if op == "&" and res == True:
        ways += (r(before, True) * r(after, True))
      elif op == "&" and res == False:
        after_result = r(after, False)
        before_result = r(before, False)
        ways += (r(before, True) * after_result)
        ways += (before_result * r(after, True))
        ways += (before_result * after_result)
      elif op == "|" and res == False:
        ways += (r(before, False) * r(after, False))
      elif op == "|" and res == True:
        after_result = r(after, True)
        before_result = r(before, True)
        ways += (before_result * after_result)
        ways += (before_result * r(after, False))
        ways += (r(before, False) * after_result)
      elif op == "^" and res == True:
        ways += (r(before, False) * r(after, True))
        ways += (r(before, True) * r(after, False))
      elif op == "^" and res == False:
        ways += (r(before, False) * r(after, False))
        ways += (r(before, True) * r(after, True))
    memo[(e, res)] = ways
    return memo[(e, res)]

  return r(exp, result)

print("Should be 2:", count_eval("1^0|0|1", False))
print("Should be 10:", count_eval("0&0&0&1^1|0", True))

