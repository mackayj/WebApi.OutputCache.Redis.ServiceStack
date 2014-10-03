using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using WebApi.OutputCache.Core.Cache;

namespace WebApi.OutputCache.Redis.ServiceStack
{
	public class ServiceStackRedisCacheProvider : IApiOutputCache
	{
		private readonly IRedisClient _client;

		public ServiceStackRedisCacheProvider(string host)
		{
			_client = new RedisClient(host);
		}

		public ServiceStackRedisCacheProvider(string host, int port, string password = null, long db = 0L)
		{
			_client = new RedisClient(host, port, password, db);
		}

		public ServiceStackRedisCacheProvider(IRedisClient client)
		{
			_client = client;
		}

		public void RemoveStartsWith(string key)
		{
			var items = _client.GetAllItemsFromList(key);
			foreach (var item in items)
			{
				Remove(item);
			}
			Remove(key);
		}

		public T Get<T>(string key) where T : class
		{
			return _client.Get<T>(key);
		}

		public object Get(string key)
		{
			return Get<object>(key);
		}

		public byte[] GetData(string key)
		{
			return Get<byte[]>(key);
		}

		public void Remove(string key)
		{
			_client.Remove(key);
		}

		public bool Contains(string key)
		{
			return _client.ContainsKey(key);
		}

		public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
		{
			// Don't store the dependsOnKey because we store it as a list later!
			if (Equals(o, "")) return;

			var primaryAdded = _client.Add(key, o, expiration.LocalDateTime);

			if (dependsOnKey != null && primaryAdded)
			{
				_client.AddItemToList(dependsOnKey, key);
			}
		}

		public IEnumerable<string> AllKeys { get; private set; }
	}
}
