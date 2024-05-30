﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTest.Cache
{
    public interface ICacheConnector
    {
        /// <summary>
        /// Return the redis cache database based on connection string
        /// </summary>
        /// <param name="cacheConnectionString">redis cache connection string</param>
        /// <returns></returns>
        IDatabase getDbCache(string cacheConnectionString);
    }

    public class CacheConnector : ICacheConnector
    {
        /// <summary>
        /// Static dictionary to hold all redis connections.  Dictionary key is the 
        /// redis cache connection string
        /// </summary>
        private static Dictionary<string, ConnectionMultiplexer> redisConnections;

        /// <summary>
        /// Returns the instance of the redis cache based on the cache 
        /// connection string passed in.
        /// </summary>
        /// <param name="cacheConnectionString"></param>
        /// <returns>Instance of the redis cache</returns>
        public IDatabase getDbCache(string cacheConnectionString)
        {
            //instantiate dictionary if null
            if (redisConnections == null)
            {
                redisConnections = new Dictionary<string, ConnectionMultiplexer>();
            }

            //Retrieve the connection from the dictionary.  If it doesn't exist
            //add the connection to the dictionary
            ConnectionMultiplexer connection;
            if (!redisConnections.TryGetValue(cacheConnectionString, out connection))
            {
                connection = ConnectionMultiplexer.Connect(cacheConnectionString);
                redisConnections.Add(cacheConnectionString, connection);
            }

            IDatabase dbCache = connection.GetDatabase();
            return dbCache;
        }
    }
}