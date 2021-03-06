/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package no.test.report.details;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

class LibraryTest {

  private Library library;
  private ObjectMapper objectMapper;

  @BeforeEach
  void beforeEach() {
    objectMapper = mock(ObjectMapper.class);
    library = new Library(objectMapper);
  }

  @Test
  void onExceptionItThrowsATerminalException() throws JsonProcessingException {
    // arrange
    var myModel = mock(MyModel.class);
    when(objectMapper.writeValueAsBytes(myModel)).thenThrow(mock(JsonProcessingException.class));

    // act & assert
    assertThrows(JsonProcessingException.class, () -> {
      library.toBytes(myModel);
    });
  }
}
