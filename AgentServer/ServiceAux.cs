﻿/* This file is automatically generated from AgentProtocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null. Don't modify it. */
#if !COMPILE_PROTOCOL
using System;
using System.Collections.Generic;
using EasyGame;


	/// <exclude/>
	public class IAgentServer_GetMailsCallback : AsyncRequestCallback
	{
		public IAgentServer_GetMailsCallback(int reqId, Connection conn)
		{
			_reqId = reqId;
			_connection = conn;
		}

		public void Error(int error, string msg)
		{
			var stream = new BinaryStreamWriter();
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("GetMails Failed. reqId: {0}, errCode:{1} errMsg:{2}", _reqId, error, msg);
		#endif
			stream.Write(2);
			stream.Write(_reqId);
			stream.Write(error);
			stream.Write(msg);
			_connection.Write(stream.Buffer, stream.Position);
		}
		public void Reply(string[] p0)
		{
			var stream = new BinaryStreamWriter();
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("GetMails Reply. reqId: {0}, result: {1}", _reqId, Log.ObjToString(p0));
		#endif
			stream.Write(2);
			stream.Write(_reqId);
			stream.Write(0);
			stream.Write(p0);
			_connection.Write(stream.Buffer, stream.Position);
		}
	}
	/// <exclude/>
	[ServiceAuxiliary(Type = typeof(IAgentServer), Stub = typeof(IAgentServerStub))]
	public interface IAgentServerImpl
	{
		void Login(Session session, string userId);
		void GetMails(Session session, IAgentServer_GetMailsCallback cb);
	}
	/// <exclude/>
	public class IAgentServerStub : StubBase
	{
		public IAgentServerStub()
		{
			AddMethodDispatcher(2, GetMails_2);
			AddMethodDispatcher(1, Login_1);
		}

		static BinaryStreamWriter GetMails_2(object __serviceObj, Session __client, BinaryStreamReader __reader)
		{
			var __timer = TimeCounter.BeginNew();
			IAgentServerImpl __service = (IAgentServerImpl)__serviceObj;
			int __reqId;
			__reader.Read(out __reqId);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("GetMails  reqId: {0}",  __reqId);
		#endif
			var reply = new IAgentServer_GetMailsCallback(__reqId, __client.Connection);
			try
			{
				__service.GetMails(__client, reply);
			}
			catch(ServiceException e)
			{
				reply.Error(e.ErrCode, e.Message);
			}
			catch(Exception e)
			{
				Log.Error("Generic Service Invoke Failed, clientId:{0} error message:{1}\nCall Stack: {2}", __client.ID, e.Message, e.StackTrace);
				reply.Error((int)ServiceErrorCode.Generic, "generic service error.");
			}
			PerfStatistic.AddItem("IAgentServer.GetMails", (int)__timer.End());
			return null;
		}
		static BinaryStreamWriter Login_1(object __serviceObj, Session __client, BinaryStreamReader __reader)
		{
			var __timer = TimeCounter.BeginNew();
			IAgentServerImpl __service = (IAgentServerImpl)__serviceObj;
			string userId;
			__reader.Read(out userId);
		#if (DEBUG || LOG_PROTOCOL)
			Log.Debug("Login userId: {0}", userId);
		#endif
			__service.Login(__client, userId);
			PerfStatistic.AddItem("IAgentServer.Login", (int)__timer.End());
			return null;
		}
	}
#endif
