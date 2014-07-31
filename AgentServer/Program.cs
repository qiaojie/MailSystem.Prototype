using System;
using System.Collections.Generic;
using EasyGame;

class AgentService : ServiceBase, IAgentServerImpl, IMailServerCallback
{
	Dictionary<string, Session> _users = new Dictionary<string, Session>();
	IMailServerProxy _mailServer;
	ServiceHolder _holder;

	public void Launch(int port)
	{
		_holder = new ServiceHolder(this);
		_holder.Start(new ServiceStartInfo() { Port = port });

		_mailServer = new IMailServerProxy();
		_mailServer.RegisterCallback(this);
		_mailServer.Connect("127.0.0.1", 8090, Context);
	}

	public override void OnDisconnection(Session client)
	{
		var userId = (string)client.UserData;
		if(userId != null)
		{
			Log.Info("user[{0}] disconnect.", userId);
			_users.Remove(userId);
			_mailServer.Call.Unsubscribe(userId);
		}
	}

	void IAgentServerImpl.Login(Session session, string userId)
	{
		session.UserData = userId;
		if(_users.ContainsKey(userId))
		{
			_users[userId].Close();
			_users.Remove(userId);
		}
		_users.Add(userId, session);
		_mailServer.Call.Subscribe(userId);
	}

	void IMailServerCallback.OnMessage(string userId, string mail)
	{
		Session user;
		if(_users.TryGetValue(userId, out user))
		{
			IAgentServerCallbackProxy.OnNewMail(user.Connection, mail);			
		}
	}

	void IAgentServerImpl.GetMails(Session session, IAgentServer_GetMailsCallback cb)
	{
		var userId = (string)session.UserData;
		_mailServer.Call.GetMails(userId, mails => cb.Reply(mails));
	}
}

class Program
{
	static void Main(string[] args)
	{
		var ctx = new ServiceContext();
		var service = new AgentService();
		service.Launch(12000);
		ctx.Run();
	}
}