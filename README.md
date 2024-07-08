## Caching

Caching means storing frequently used data in a temporary storage. Its main purpose is to speed up data delivery to users by avoiding repeated fetching from the original source.

Caching reduces the time and resources needed to fetch data, making systems faster and improving user experience.

## Server-side Caching in .NET

There are different ways to implement server-side caching in a .NET project. 3 commonly used techniques include:

### 1. Response Caching

Response caching is specific to web applications. It means caching the entire HTTP response generated by an action method.

.NET Core provides `[ResponseCache]` attribute to enable response caching for an action method.

ResponseCache attribute has several properties, including `NoStore`, `Duration` and `Location`.

You can also add global response caching using `Caching Middleware` in Program.cs file.

### 2. In-Memory Caching

In this technique, the application stores temporary data in the main memory (RAM). It's lightweight and suitable when caching is needed within a single instance of an application.

.NET Core provides `IMemoryCache` interface for managing in-memory caching. It's useful for caching small amounts of data.

### 3. Distributed Caching

Distributed caching involves using a shared cache across multiple instances of an application. This avoids the performance bottlenecks of having a large cache on a single server. Data consistency is maintained across servers, even if one server restarts.

Distributed caching is suitable for applications with a microservices architecture.

In .NET Core, popular options for distributed caching include:

- Redis Cache
- SQL Server Cache
- NCache
  
In this repository I implemented `Redis Cache` useing `StackExchange.Redis` NuGet package.


#### Run Redis with Docker

Make sure [Docker](https://docs.docker.com/get-docker/) is installed on your machine.

Open a terminal window and execute the following command to run Redis in a Docker container:

```
docker run --name my-redis -p 6379:6379 -d redis:latest
```



---
These caching techniques help optimize application performance, reduce latency, and enhance user experience by delivering data faster and more efficiently.

You can see the implementation of these 3 caching techniques in the sample app in this repository.


Feel free to create an issue or submit a pull request.

