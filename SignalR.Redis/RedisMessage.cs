﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SignalR.Redis
{
    [Serializable]
    public class RedisMessage
    {
        private static readonly BinaryFormatter formatter = new BinaryFormatter();

        public RedisMessage()
        {
        }

        public RedisMessage(long id, Message[] message)
        {
            Id = id;
            Messages = message;
        }

        public long Id { get; set; }

        public Message[] Messages { get; set; }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static RedisMessage Deserialize(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return (RedisMessage)formatter.Deserialize(ms);
            }
        }
    }
}