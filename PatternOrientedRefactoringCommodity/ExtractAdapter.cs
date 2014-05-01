using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    #region 例外
    public class QueryException : Exception
    {
        public QueryException()
        {
        }

        public QueryException(string message)
            : base(message)
        {
        }

        public QueryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class SDLoginFailedException : Exception
    {
        public SDLoginFailedException()
        {
        }

        public SDLoginFailedException(string message)
            : base(message)
        {
        }

        public SDLoginFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class SDSocketInitFailedException : Exception
    {
        public SDSocketInitFailedException()
        {
        }

        public SDSocketInitFailedException(string message)
            : base(message)
        {
        }

        public SDSocketInitFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class SDNotFoundException : Exception
    {
        public SDNotFoundException()
        {
        }

        public SDNotFoundException(string message)
            : base(message)
        {
        }

        public SDNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion

    #region ビルド用に暫定的に定義
    public class SDLogin
    {
        internal SDSession loginSession(string server, string user, string password)
        {
            throw new NotImplementedException();
        }
    }
    public class SDSession
    {
        internal SDQuery createQuery(string p)
        {
            throw new NotImplementedException();
        }
    }
    public class SDLoginSession
    {
        private string sdConfigFileName;
        private bool p;

        public SDLoginSession(string sdConfigFileName, bool p)
        {
            // TODO: Complete member initialization
            this.sdConfigFileName = sdConfigFileName;
            this.p = p;
        }

        internal void loginSession(string server, string user, string password)
        {
            throw new NotImplementedException();
        }

        internal SDQuery createQuery(string p)
        {
            throw new NotImplementedException();
        }
    }
    public class SDQuery
    {
        public static string OPEN_FOR_QUERY = "Open for query";
        internal void clearResultSet()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region リファクタリング対象コード
    public class Query
    {

        protected SDQuery sdQuery; // this is needed for SD versions 5.1 & 5.2 

        // this is a login for SD 5.1
        // NOTE: remove this when we convert all aplications to 5.2 
        public virtual void login(String server, String user, String password)
        {
            // QuerySD51に責務を移動
        }

        // 5.2 login
        public virtual void login(String server, String user, String password, String sdConfigFileName)
        {
            // QuerySD52に責務を移動
        }

        protected void executeQuery()
        {
            throw new NotImplementedException();
        }
    }

    public class QuerySD52 : Query
    {
        private SDLoginSession sdLoginSession; // needed for SD version 5.2 
        public QuerySD52() : base() { }
        public void doQuery()
        {
            if (sdQuery != null)
                sdQuery.clearResultSet();
            sdQuery = sdLoginSession.createQuery(SDQuery.OPEN_FOR_QUERY);
            executeQuery();
        }

        // 5.2 login
        public override void login(String server, String user, String password, String sdConfigFileName)
        {
            sdLoginSession = new SDLoginSession(sdConfigFileName, false);
            try
            {
                sdLoginSession.loginSession(server, user, password);
            }
            catch (SDLoginFailedException login_fail_exception)
            {
                throw new QueryException("Login failure\n" + login_fail_exception, login_fail_exception);
            }
            catch (SDSocketInitFailedException init_fail_exception)
            {
                throw new QueryException("Socket fail\n" + init_fail_exception, init_fail_exception);
            }
            catch (SDNotFoundException not_found_exception)
            {
                throw new QueryException("Not found exception\n" + not_found_exception, not_found_exception);
            }
        }
    }

    public class QuerySD51 : Query
    {
        private SDLogin sdLogin; // needed for SD version 5.1 
        private SDSession sdSession; // needed for SD version 5.1 
        public QuerySD51() : base() { }

        public override void login(String server, String user, String password)
        {
            try
            {
                sdSession = sdLogin.loginSession(server, user, password);
            }
            catch (SDLoginFailedException login_fail_exception)
            {
                throw new QueryException("Login failure\n" + login_fail_exception, login_fail_exception);
            }
            catch (SDSocketInitFailedException init_fail_exception)
            {
                throw new QueryException("Socket fail\n" + init_fail_exception, init_fail_exception);
            }
        }

        public void doQuery()
        {
            if (sdQuery != null)
                sdQuery.clearResultSet();
            sdQuery = sdSession.createQuery(SDQuery.OPEN_FOR_QUERY);
            executeQuery();
        }
    }
    #endregion
}
