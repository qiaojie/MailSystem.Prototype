using System;
using System.Runtime.InteropServices;
using EasyGame;

public interface IAgentServerCallback : IServiceCallback
{
	[DispId(1)]
	void OnNewMail(string mail);
}

[Protocol(ID = 2), Callback(typeof(IAgentServerCallback))]
public interface IAgentServer
{
	[DispId(1)]
	void Login(string userId);

	[DispId(2), Async]
	void GetMails(Action<string[]> OnResults);
}
