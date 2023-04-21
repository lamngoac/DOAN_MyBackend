using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.State
{
    public class SessionWrapper
    {
        public HttpSessionStateBase Session
        {
            get;
            set;
        }
        //public static string objectList = "objectList";
        #region Get / Set to session
        /// <summary>
        /// Gets typed values from the session
        /// </summary>
        /// <param name="key">The key name of the session value</param>
        public T GetItem<T>(string key)
        {
            return (T)Session[key];

        }

        /// <summary>
        /// Gets typed values from the session
        /// </summary>
        /// <param name="key">The key name of the session value</param>
        /// <param name="create">Determines if a new instance of the type T should be created in case it does not exist in session for that key</param>
        public T GetItem<T>(string key, bool create) where T : new()
        {

            if (Session[key] == null)
            {
                if (create)
                {
                    T value = new T();
                    Session[key] = value;
                    return value;
                }
                else
                {
                    return (T)Session[key];
                }
            }
            else
            {
                return (T)Session[key];
            }
        }

        public void SetItem<T>(string key, T value)
        {
            Session[key] = value;
        }

        public SessionWrapper(HttpSessionStateBase session)
        {
            Session = session;
        }

        public SessionWrapper(HttpContextBase context)
            : this(context.Session)
        {

        }
        #endregion

        #region Props


        public UserState UserState
        {
            get
            {
                return GetItem<UserState>("UserState");
            }
            set
            {
                SetItem<UserState>("UserState", value);
            }
        }
        /// <summary>
		/// Gets a unique private token for this session. This token is not related to session id.
		/// </summary>
        public string SessionToken
        {
            get
            {
                const string sessionTokenKey = "SessionToken";
                if (GetItem<string>(sessionTokenKey) == null)
                {
                    SetItem<string>(sessionTokenKey, Guid.NewGuid().ToString("N"));
                }
                return GetItem<string>(sessionTokenKey);
            }
        }

        public string SessionId
        {
            get
            {
                return GetItem<string>("SessionId");
            }
            set
            {
                SetItem<string>("SessionId", value);
            }
        }
        #endregion
    }
}