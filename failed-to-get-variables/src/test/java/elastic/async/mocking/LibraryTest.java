/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package elastic.async.mocking;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

import org.elasticsearch.client.RestHighLevelClient;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;

@ExtendWith(MockitoExtension.class)
class LibraryTest {
    @Test void testSomeLibraryMethod() {
        var restHighLevelClient = mock(RestHighLevelClient.class);
        when(restHighLevelClient.indexAsync(any(), any(), any()))
            .then(answer -> {
                var arguments = answer.getArguments();
                return null;
            });
        var classUnderTest = new Library(restHighLevelClient);
        classUnderTest.doIt(null);
    }
}
