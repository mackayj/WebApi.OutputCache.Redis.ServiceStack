WebApi.OutputCache.Redis.ServiceStack
=====================================

Redis Caching Provider for WebApi.OutputCache using ServiceStack.Redis v3

For use with https://github.com/filipw/AspNetWebApi-OutputCache

Note: You need to manually change the `_webApiCache.Get(X) as Y` calls to `_webApiCache.Get<Y>(X)` until that project updates (https://github.com/filipw/AspNetWebApi-OutputCache/pull/103)

Want to use StackExchange.Redis instead? Check here: https://github.com/mackayj/WebApi.OutputCache.Redis.StackExchange (currently broken due to casts)

