/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package miked.javatestsissue;

public class Library {
    public boolean someLibraryMethod() {
        var id = "hello world";
        return true;
    }

    // before format document ALT + SHIFT F
    public String formatSwitchStatementBefore() {
        switch ("hello") {
            case "world":
                return "ok";
            default:
                return "sure";
        }
    }

    // after format document ALT + SHIFT F
    public String formatSwitchStatementAfter() {
        switch ("hello") {
            case "world":
                return "ok";
            default:
                return "sure";
        }
    }
}
