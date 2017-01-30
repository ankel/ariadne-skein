package ankel.cost;

import ankel.CostCalculator;
import ankel.Node;

/**
 * @author Binh Tran
 */
public class ACostCalculator implements CostCalculator
{
  @Override
  public int calc(final Node old, final String next)
  {
    return old.getCost() + 1;
  }
}
