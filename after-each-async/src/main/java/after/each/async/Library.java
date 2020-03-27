/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package after.each.async;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.CompletionStage;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

import lombok.extern.slf4j.Slf4j;

@Slf4j
public class Library {
    public boolean someLibraryMethod() {
        log.info("logging works");
        return true;
    }

    public Future<String> calculateAsync() {
        var completableFuture = new CompletableFuture<String>();

        Executors.newCachedThreadPool()
                .submit(() -> {
                    Thread.sleep(1000);
                    completableFuture.complete("Hello");
                    log.info("calc async completed");
                    return null;
                });

        return completableFuture;
    }

    public CompletionStage<String> calculateAsync2() {
        var completableFuture = new CompletableFuture<String>();

        Executors.newCachedThreadPool()
                .submit(() -> {
                    Thread.sleep(1000);
                    completableFuture.complete("Hello");
                    log.info("calc async completed");
                    return null;
                });

        return completableFuture;
    }
}
