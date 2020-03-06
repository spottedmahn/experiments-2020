[mget producing: Unable to parse response body for Response](https://stackoverflow.com/questions/60537642/mget-producing-unable-to-parse-response-body-for-response)

![](readme-resources/2020-03-06-10-14-04.png)

![](readme-resources/2020-03-06-10-14-46.png)

## the real problem
![](readme-resources/2020-03-06-10-14-59.png)

## interesting

a NPE shows itself 🤷‍♂️

![](readme-resources/2020-03-06-10-15-05.png)

## log / debug console

>10:22:25.477 [main] INFO  mget.cant.parse.body.Library - start of doIt()  
10:22:25.884 [I/O dispatcher 1] INFO  mget.cant.parse.body.Library - getting source bytes to convert to a POJO  
10:22:25.885 [I/O dispatcher 1] INFO  mget.cant.parse.body.Library - converting source to POJO  
10:22:25.890 [I/O dispatcher 1] ERROR mget.cant.parse.body.Library - error with mget  
java.io.IOException: Unable to parse response body for Response{requestLine=POST /_mget HTTP/1.1, host=http://localhost:9200, response=HTTP/1.1 200 OK}  
	at org.elasticsearch.client.RestHighLevelClient$1.onSuccess(RestHighLevelClient.java:1665)  
	at org.elasticsearch.client.RestClient$FailureTrackingResponseListener.onSuccess(RestClient.java:590)  
	at org.elasticsearch.client.RestClient$1.completed(RestClient.java:333)  
	at org.elasticsearch.client.RestClient$1.completed(RestClient.java:327)  
	at org.apache.http.concurrent.BasicFuture.completed(BasicFuture.java:122)  
	at org.apache.http.impl.nio.client.DefaultClientExchangeHandlerImpl.responseCompleted (DefaultClientExchangeHandlerImpl.java:181)  
	at org.apache.http.nio.protocol.HttpAsyncRequestExecutor.processResponse(HttpAsyncRequestExecutor.java:448)  
	at org.apache.http.nio.protocol.HttpAsyncRequestExecutor.inputReady(HttpAsyncRequestExecutor.java:338)  
	at org.apache.http.impl.nio.DefaultNHttpClientConnection.consumeInput(DefaultNHttpClientConnection.java:265)  
	at org.apache.http.impl.nio.client.InternalIODispatch.onInputReady(InternalIODispatch.java:81)  
	at org.apache.http.impl.nio.client.InternalIODispatch.onInputReady(InternalIODispatch.java:39)  
	at org.apache.http.impl.nio.reactor.AbstractIODispatch.inputReady(AbstractIODispatch.java:114)  
	at org.apache.http.impl.nio.reactor.BaseIOReactor.readable(BaseIOReactor.java:162)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.processEvent(AbstractIOReactor.java:337)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.processEvents(AbstractIOReactor.java:315)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.execute(AbstractIOReactor.java:276)  
	at org.apache.http.impl.nio.reactor.BaseIOReactor.execute(BaseIOReactor.java:104)  
	at org.apache.http.impl.nio.reactor.AbstractMultiworkerIOReactor$Worker.run(AbstractMultiworkerIOReactor.java:591)  
	at java.base/java.lang.Thread.run(Thread.java:834)  
Caused by: java.lang.IllegalArgumentException: argument "src" is null  
	at com.fasterxml.jackson.databind.ObjectMapper._assertNotNull(ObjectMapper.java:4429)  
	at com.fasterxml.jackson.databind.ObjectMapper.readValue(ObjectMapper.java:3274)  
	at mget.cant.parse.body.Library$1.onResponse(Library.java:51)  
	at mget.cant.parse.body.Library$1.onResponse(Library.java:1)  
	at org.elasticsearch.client.RestHighLevelClient$1.onSuccess(RestHighLevelClient.java:1663)  
	... 18 common frames omitted  
10:22:25.924 [main] INFO  mget.cant.parse.body.Library - start of doIt2()  
10:22:25.934 [I/O dispatcher 9] INFO  mget.cant.parse.body.Library - getting source bytes to convert to a POJO  
10:22:25.935 [I/O dispatcher 9] ERROR mget.cant.parse.body.Library - error converting getting source bytes  
java.lang.NullPointerException: null  
	at mget.cant.parse.body.Library$2.onResponse(Library.java:89)  
	at mget.cant.parse.body.Library$2.onResponse(Library.java:1)  
	at org.elasticsearch.client.RestHighLevelClient$1.onSuccess(RestHighLevelClient.java:1663)  
	at org.elasticsearch.client.RestClient$FailureTrackingResponseListener.onSuccess(RestClient.java:590)  
	at org.elasticsearch.client.RestClient$1.completed(RestClient.java:333)  
	at org.elasticsearch.client.RestClient$1.completed(RestClient.java:327)  
	at org.apache.http.concurrent.BasicFuture.completed(BasicFuture.java:122)  
	at org.apache.http.impl.nio.client.DefaultClientExchangeHandlerImpl.responseCompleted(DefaultClientExchangeHandlerImpl.java:181)  
	at org.apache.http.nio.protocol.HttpAsyncRequestExecutor.processResponse(HttpAsyncRequestExecutor.java:448)  
	at org.apache.http.nio.protocol.HttpAsyncRequestExecutor.inputReady(HttpAsyncRequestExecutor.java:338)  
	at org.apache.http.impl.nio.DefaultNHttpClientConnection.consumeInput(DefaultNHttpClientConnection.java:265)  
	at org.apache.http.impl.nio.client.InternalIODispatch.onInputReady(InternalIODispatch.java:81)  
	at org.apache.http.impl.nio.client.InternalIODispatch.onInputReady(InternalIODispatch.java:39)  
	at org.apache.http.impl.nio.reactor.AbstractIODispatch.inputReady(AbstractIODispatch.java:114)  
	at org.apache.http.impl.nio.reactor.BaseIOReactor.readable(BaseIOReactor.java:162)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.processEvent(AbstractIOReactor.java:337)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.processEvents(AbstractIOReactor.java:315)  
	at org.apache.http.impl.nio.reactor.AbstractIOReactor.execute(AbstractIOReactor.java:276)  
	at org.apache.http.impl.nio.reactor.BaseIOReactor.execute(BaseIOReactor.java:104)  
	at org.apache.http.impl.nio.reactor.AbstractMultiworkerIOReactor$Worker.run(AbstractMultiworkerIOReactor.java:591)  
	at java.base/java.lang.Thread.run(Thread.java:834)  
