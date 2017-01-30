package ankel;

import static org.junit.Assert.*;

import lombok.extern.slf4j.Slf4j;

import org.apache.commons.lang3.ArrayUtils;
import org.junit.Test;

/**
 * @author Binh Tran
 */
@Slf4j
public class ActionTest
{
  // Just for my own education to understand ArrayUtils.add behavior.
  @Test
  public void addAt() throws Exception
  {
    char[] original = {'a', 'b', 'c'};

    assertEquals("zabc", new String(ArrayUtils.add(original, 0, 'z')));
    assertEquals("azbc", new String(ArrayUtils.add(original, 1, 'z')));
    assertEquals("abzc", new String(ArrayUtils.add(original, 2, 'z')));
    assertEquals("abcz", new String(ArrayUtils.add(original, 3, 'z')));

    try
    {
      ArrayUtils.add(original, 4, 'z');
      fail();
    }
    catch (final Exception e)
    {
      log.error("Exception", e);
    }
  }

}
