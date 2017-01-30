package ankel;

import java.util.ArrayList;
import java.util.List;
import java.util.Set;

import lombok.RequiredArgsConstructor;

import org.apache.commons.lang3.ArrayUtils;

import com.google.common.collect.Lists;

/**
 * @author Binh Tran
 */
@RequiredArgsConstructor
public class Action
{
  public static final char[] ALPHABET = "abcdefghijklmnopqrstuvwxyz".toCharArray();

  private final Set<String> dictionary;
  private final CostCalculator costCalculator;
  private final char[] alphabet;

  public List<Node> swap(final Node node)
  {
    final char[] currentCharArr = node.getCurrent().toCharArray();

    final List<Node> ret = new ArrayList<>();

    for (int i = 0; i < currentCharArr.length; ++i)
    {
      char oldChar = currentCharArr[i];
      for (char c : alphabet)
      {
        if (oldChar != c)
        {
          currentCharArr[i] = c;
          final String newString = new String(currentCharArr);

          if (dictionary.contains(newString) && !node.getSoFar().contains(newString))
          {
            final ArrayList<String> newSoFar = Lists.newArrayList(node.getSoFar());
            newSoFar.add(node.getCurrent());

            int newCost = costCalculator.calc(node, newString);

            ret.add(new Node(newString, newSoFar, newCost));
          }

          currentCharArr[i] = oldChar;
        }
      }
    }

    return ret;
  }

  public List<Node> add(final Node node)
  {
    final char[] currentCharArr = node.getCurrent().toCharArray();

    final List<Node> ret = new ArrayList<>();

    for (int i = 0; i <= currentCharArr.length; ++i)
    {
      for (char c : alphabet)
      {
        final String newString = new String(ArrayUtils.add(currentCharArr, i, c));

        if (dictionary.contains(newString) && !node.getSoFar().contains(newString))
        {
          final ArrayList<String> newSoFar = Lists.newArrayList(node.getSoFar());
          newSoFar.add(node.getCurrent());

          int newCost = costCalculator.calc(node, newString);

          ret.add(new Node(newString, newSoFar, newCost));
        }
      }
    }

    return ret;
  }

  public List<Node> remove(final Node node)
  {
    final char[] currentCharArr = node.getCurrent().toCharArray();

    final List<Node> ret = new ArrayList<>();

    for (int i = 0; i < currentCharArr.length; ++i)
    {
      final String newString = new String(ArrayUtils.remove(currentCharArr, i));

      if (dictionary.contains(newString) && !node.getSoFar().contains(newString))
      {
        final ArrayList<String> newSoFar = Lists.newArrayList(node.getSoFar());
        newSoFar.add(node.getCurrent());

        int newCost = costCalculator.calc(node, newString);

        ret.add(new Node(newString, newSoFar, newCost));
      }
    }

    return ret;
  }
}
