using System;
using EasyGame;

class Program
{
	class Client : IAgentServerCallback
	{
		void IAgentServerCallback.OnNewMail(string mail)
		{
			Log.Info("Get new mail: {0}", mail);
		}

		void IServiceCallback.OnError(int errCode, string errMsg)
		{
			Log.Error(errMsg);
		}
	}
	static void Main(string[] args)
	{
		if(args.Length != 1)
		{
			Log.Error("please input user name.");
			return;
		}
		var client = new Client();
		var ctx = new ServiceContext();
		var proxy = new IAgentServerProxy();
		proxy.RegisterCallback(client);
		proxy.Connect("127.0.0.1", 12000, ctx);
		proxy.Call.Login(args[0]);
		ctx.Run();
	}
}