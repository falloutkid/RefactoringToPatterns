using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCode
{
    public interface MessageStrategy
    {
        void sendMessage();
    }

    public abstract class AbstractStrategyFactory
    {
        public abstract MessageStrategy createStrategy(MessageBody mb);
    }

    public class MessageBody
    {
        Object payload;
        public Object getPayload()
        {
            return payload;
        }
        public void configure(Object obj)
        {
            payload = obj;
        }
        public void send(MessageStrategy ms)
        {
            ms.sendMessage();
        }
    }
    class ConcreteMessageStrategy : MessageStrategy
    {
        private MessageBody message_body;
        public ConcreteMessageStrategy(MessageBody mb)
        {
            message_body = mb;
        }
        public void sendMessage()
        {
            object out_ouject = message_body.getPayload();
            System.Diagnostics.Debug.WriteLine(out_ouject.ToString());
        }
    }

    public class DefaultFactory : AbstractStrategyFactory
    {
        private DefaultFactory() { ;}
        static DefaultFactory instance;
        public static AbstractStrategyFactory getInstance()
        {
            if (instance == null)
                instance = new DefaultFactory();
            return instance;
        }

        public override MessageStrategy createStrategy(MessageBody mb)
        {
            return new ConcreteMessageStrategy(mb);
        }
    }

    public class HelloWorld
    {
        public void writeHelloWorld()
        {
            MessageBody mb = new MessageBody();
            mb.configure("Hello World!");
            AbstractStrategyFactory asf = DefaultFactory.getInstance();
            MessageStrategy strategy = asf.createStrategy(mb);
            mb.send(strategy);
        }
    }
}
