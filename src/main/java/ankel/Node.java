package ankel;

import java.util.List;

import lombok.Value;

/**
 * A node in the graph
 * @author Binh Tran
 */
@Value
public class Node implements Comparable<Node>
{
  private String current;
  private List<String> soFar;
  private int cost;

  @Override
  public int compareTo(final Node o)
  {
    final int x = cost;
    final int y = o.getCost();
    return (x < y) ? -1 : ((x == y) ? 0 : 1);
  }
}
