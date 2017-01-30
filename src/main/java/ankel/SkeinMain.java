package ankel;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.PriorityQueue;
import java.util.Set;
import java.util.stream.Stream;

import lombok.extern.slf4j.Slf4j;

import ankel.cost.ACostCalculator;

/**
 * @author Binh Tran
 */
@Slf4j
public class SkeinMain
{
  public static void main(final String[] args) throws Exception
  {
    final long start = System.currentTimeMillis();

    final Set<String> dictionary = readDict(args[0]);

    Action action = new Action(dictionary, new ACostCalculator(), Action.ALPHABET);

    PriorityQueue<Node> frontier = new PriorityQueue<>();

    frontier.add(new Node(args[1], Collections.emptyList(), 0));

    boolean found = false;

    bigLoop:
    while (!frontier.isEmpty())
    {
      final Node node = frontier.remove();

      List<Node> newNodes = new ArrayList<>();

      newNodes.addAll(action.add(node));
      newNodes.addAll(action.remove(node));
      newNodes.addAll(action.swap(node));

      for (final Node n : newNodes)
      {
        if (n.getCurrent().equals(args[2]))
        {
          n.getSoFar().add(args[2]);
          log.info("Found one path: {}", n.getSoFar());
          found = true;
          break bigLoop;
        }
      }

      frontier.addAll(newNodes);
    }

    if (!found)
    {
      log.info("Cannot find any path");
    }
    log.info("Took: [{}ms]", System.currentTimeMillis() - start);
  }

  /**
   * Read dictionary into memory
   */
  private static Set<String> readDict(final String arg)
  {
    Set<String> ret = new HashSet<>();
    try (Stream<String> stream = Files.lines(Paths.get(arg)))
    {
      stream
          .map(String::trim)
          .map(String::toLowerCase)
          .forEach(ret::add);

      return ret;
    }
    catch (IOException e)
    {
      log.error("Exception while reading [{}]", arg, e);
      throw new RuntimeException(e);
    }
  }
}
