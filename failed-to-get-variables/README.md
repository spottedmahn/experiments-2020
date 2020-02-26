
>Failed to get variables. Reason: com.sun.jdi.InvalidStackFrameException

![](readme-resources/2020-02-26-09-51-31.png)

![](readme-resources/2020-02-26-09-52-03.png)

![](readme-resources/2020-02-26-09-52-28.png)

### Repro Steps
1. Debug the test
1. Put a breakpoint on `when(restHighLevelClient.indexAsync(any(), any(), any()))`
1. After the break point is hit, enable Caught Exceptions
1. F5
1. Wait for the exception, inspect the debugger window

![](readme-resources/failed-to-get-variables-repro-steps.gif)

### Exception from Java Server Log

>!ENTRY java-debug 4 0 2020-02-26 10:06:40.711
!MESSAGE [error response][variables]: Failed to get variables. Reason: com.sun.jdi.InvalidStackFrameException
!STACK 0
com.microsoft.java.debug.core.DebugException: Failed to get variables. Reason: com.sun.jdi.InvalidStackFrameException
	at com.microsoft.java.debug.core.adapter.AdapterUtils.createCompletionException(AdapterUtils.java:255)
	at com.microsoft.java.debug.core.adapter.handler.VariablesRequestHandler.handle(VariablesRequestHandler.java:118)
	at com.microsoft.java.debug.core.adapter.DebugAdapter.lambda$dispatchRequest$0(DebugAdapter.java:87)
	at java.base/java.util.concurrent.CompletableFuture.uniComposeStage(CompletableFuture.java:1106)
	at java.base/java.util.concurrent.CompletableFuture.thenCompose(CompletableFuture.java:2235)
	at com.microsoft.java.debug.core.adapter.DebugAdapter.dispatchRequest(DebugAdapter.java:86)
	at com.microsoft.java.debug.core.adapter.ProtocolServer.dispatchRequest(ProtocolServer.java:118)
	at com.microsoft.java.debug.core.protocol.AbstractProtocolServer.lambda$new$0(AbstractProtocolServer.java:78)
	at io.reactivex.internal.observers.LambdaObserver.onNext(LambdaObserver.java:60)
	at io.reactivex.internal.operators.observable.ObservableObserveOn$ObserveOnObserver.drainNormal(ObservableObserveOn.java:200)
	at io.reactivex.internal.operators.observable.ObservableObserveOn$ObserveOnObserver.run(ObservableObserveOn.java:252)
	at io.reactivex.internal.schedulers.ScheduledRunnable.run(ScheduledRunnable.java:61)
	at io.reactivex.internal.schedulers.ScheduledRunnable.call(ScheduledRunnable.java:52)
	at java.base/java.util.concurrent.FutureTask.run(FutureTask.java:264)
	at java.base/java.util.concurrent.ScheduledThreadPoolExecutor$ScheduledFutureTask.run(ScheduledThreadPoolExecutor.java:304)
	at java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)
	at java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)
	at java.base/java.lang.Thread.run(Thread.java:834)
Caused by: com.sun.jdi.InvalidStackFrameException
	at org.eclipse.jdi.internal.MirrorImpl.defaultReplyErrorHandler(MirrorImpl.java:294)
	at org.eclipse.jdi.internal.StackFrameImpl.getValues(StackFrameImpl.java:142)
	at org.eclipse.jdi.internal.StackFrameImpl.getValue(StackFrameImpl.java:78)
	at com.microsoft.java.debug.core.adapter.variables.VariableUtils.listLocalVariables(VariableUtils.java:153)
	at com.microsoft.java.debug.core.adapter.handler.VariablesRequestHandler.handle(VariablesRequestHandler.java:109)
	... 16 more
