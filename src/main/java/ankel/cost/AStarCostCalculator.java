package ankel.cost;

import java.util.ArrayList;
import java.util.List;

import lombok.RequiredArgsConstructor;

import ankel.CostCalculator;
import ankel.Node;

/**
 * A simple heuristic, the estimated distance to the destination. The distance is calculated as:
 *  string_length - number_of_character_in_the_correct_order
 *
 * @author Binh Tran
 */
@RequiredArgsConstructor
public class AStarCostCalculator implements CostCalculator
{
  private final String destination;

  @Override
  public int calc(final Node old, final String next)
  {
    List<Integer> indexOfChar = new ArrayList<>();
    for (final char c : destination.toCharArray())
    {
      indexOfChar.add(next.indexOf(c));
    }

    int count = getCount(indexOfChar);

    return old.getCost() + 1
        + next.length() - count;
  }

  private static int getCount(final List<Integer> indexOfChar)
  {
    int max = -1;
    int count = 0;

    for (int i = 0; i < indexOfChar.size(); ++i)
    {
      if (indexOfChar.get(i) > max)
      {
        max = indexOfChar.get(i);
        count++;
      }
    }
    return count;
  }
}
