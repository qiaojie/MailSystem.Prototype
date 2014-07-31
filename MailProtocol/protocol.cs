using System;
using System.Runtime.InteropServices;
using EasyGame;

public interface IMailServerCallback : IServiceCallback
{
	[DispId(1)]	
	void OnMessage(string userId, string mail);
}

[Protocol(ID = 1), Callback(typeof(IMailServerCallback))]
public interface IMailServer
{
	[DispId(1)]
	void Subscribe(string userId);
	[DispId(2)]
	void Unsubscribe(string userId);
	[DispId(3), Async]
	void GetMails(string userId, Action<string[]> OnResults);
}
