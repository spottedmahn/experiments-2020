package miked.autoformat;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.stream.Collectors;

class AutoFormatIssue {
    // @formatter:off
    // before auto format
    // https://stackoverflow.com/a/30026710/185123
    static<T> CompletableFuture<List<T>> sequence(List<CompletableFuture<T>> com) {
        return CompletableFuture.allOf(com.toArray(new CompletableFuture<?>[0]))
                .thenApply(v -> com.stream()
                    .map(CompletableFuture::join)
                    .collect(Collectors.toList())
                );
    }
    // @formatter:on

    // after auto format
    // https://stackoverflow.com/a/30026710/185123
    static <T> CompletableFuture<List<T>> sequence2(List<CompletableFuture<T>> com) {
        return CompletableFuture.allOf(com.toArray(new CompletableFuture<?>[0]))
                .thenApply(v -> com.stream()
                        .map(CompletableFuture::join)
                        .collect(Collectors.toList()));
    }
}