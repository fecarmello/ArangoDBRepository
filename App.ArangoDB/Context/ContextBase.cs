using System;

namespace App.ArangoDB.Context
{
    public abstract class ContextBase
    {
        public abstract string CollectionName { get; }
        public abstract string DatabaseName { get; }
        public string ConnectionString { get; }

        public string Url { get { return ConnectionString.Split('#')[00]; } }
        public string User { get { return ConnectionString.Split('#')[01]; } }
        public string Password { get { return ConnectionString.Split('#')[02]; } }

        public ContextBase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString", "Connection String está nula ou vazia");

            ConnectionString = connectionString;
        }
    }
}