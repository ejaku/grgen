/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;

/**
 * A factory, that makes a C backend.
 */
public class MySQLBackendFactory implements BackendFactory {

  /**
   * The default constructor.
   */
  public MySQLBackendFactory() {
  }

  /**
   * @see de.unika.ipd.grgen.be.BackendCreator#getBackend()
   */
  public Backend getBackend() {
    return new MySQLBackend();
  }

}
