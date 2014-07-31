using System;
using System.Collections.Generic;
using System.Linq;
using EasyGame;

interface ICmdline
{
	void SendMail(string receiver, string mail);
}

class MailData
{
	public string receiver;
	public string mail;
}

class MailService : ServiceBase, IMailServerImpl, ICmdline
{
	Dictionary<string, Session> _onlineUsers = new Dictionary<string, Session>();
	List<MailData> _mailbox = new List<MailData>();
	ServiceHolder _holder;

	public void Launch(int port)
	{
		_holder = new ServiceHolder(this);
		_holder.Start(new ServiceStartInfo() { Port = port });
	}

	void IMailServerImpl.Subscribe(Session session, string userId)
	{
		_onlineUsers.Add(userId, session);
	}

	void IMailServerImpl.Unsubscribe(Session session, string userId)
	{
		_onlineUsers.Remove(userId);
	}

	void ICmdline.SendMail(string receiver, string mail)
	{
		Session agent;
		if(_onlineUsers.TryGetValue(receiver, out agent))
		{
			IMailServerCallbackProxy.OnMessage(agent.Connection, receiver, mail);
		}
		_mailbox.Add(new MailData() { receiver = receiver, mail = mail });
	}

	void IMailServerImpl.GetMails(Session session, string userId, IMailServer_GetMailsCallback cb)
	{
		var mails = _mailbox.Where(m => m.receiver == userId).Select(m => m.mail).ToArray();
		cb.Reply(mails);
	}
}

class Program
{
	static void Main(string[] args)
	{
		var ctx = new ServiceContext();
		var service = new MailService();
		ctx.LaunchCmdline(typeof(ICmdline), service);
		service.Launch(8090);
		ctx.Run();
	}
}