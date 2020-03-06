/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package display.name.generator;

import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.DisplayNameGeneration;
import org.junit.jupiter.api.Test;

import display.name.generator.util.ReplaceCamelCase;

@DisplayNameGeneration(ReplaceCamelCase.class)
class LibraryTest {
    @Test
    void thisNameShouldBeReplacedByTheGenerator() {
        Library classUnderTest = new Library();
        assertTrue(classUnderTest.someLibraryMethod(), "someLibraryMethod should return 'true'");
    }

    @Test
    @DisplayName("This works ✅")
    void blah() {
        Library classUnderTest = new Library();
        assertTrue(classUnderTest.someLibraryMethod(), "someLibraryMethod should return 'true'");
    }
}